using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Linq;
using Terraria.UI;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria.GameContent.UI;
using Overthrown.Content.GUI;
using Overthrown.World.ChestHelper;
using Overthrown.World.ChestHelper.GUI;
using Overthrown.Content.Items.StructureCreation;


namespace Overthrown
{
	public class Overthrown : Mod
	{
		public const string ASSET_PATH = "Overthrown/Assets";
		public const string STRUCTURE_PATH = "Overthrown/World/Structures";

		public static int VoidCurrencyId;

		public Overthrown() { Instance = this; }

		public static Overthrown Instance { get; set; }

		public override void Load()
		{
			VoidCurrencyId = CustomCurrencyManager.RegisterCurrency(new Content.Currencies.VoidCurrency(ModContent.ItemType<Content.Items.VoidCrystal>(), 999L, "Mods.Overthrown.Currencies.VoidCurrency"));
		}

        public override void Unload()
		{ 
        }
    }

    public class UIRenderer : ModSystem
    {
        internal static UserInterface GeneratorMenuUI;
        internal static GeneratorMenu GeneratorMenu;

        internal static UserInterface ChestMenuUI;
        internal static ChestCustomizer ChestCustomizer;

        public override void Load()
        {
            GeneratorMenuUI = new UserInterface();
            GeneratorMenu = new GeneratorMenu();
            GeneratorMenuUI.SetState(GeneratorMenu);

            ChestMenuUI = new UserInterface();
            ChestCustomizer = new ChestCustomizer();
            ChestMenuUI.SetState(ChestCustomizer);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            layers.Insert(layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text")), new LegacyGameInterfaceLayer("GUI Menus",
                delegate
                {
                    if (GeneratorMenu.Visible)
                    {
                        GeneratorMenuUI.Update(Main._drawInterfaceGameTime);
                        GeneratorMenu.Draw(Main.spriteBatch);
                    }

                    if (ChestCustomizer.Visible)
                    {
                        ChestMenuUI.Update(Main._drawInterfaceGameTime);
                        ChestCustomizer.Draw(Main.spriteBatch);
                    }

                    return true;
                }, InterfaceScaleType.UI));
        }

        public override void PostDrawInterface(SpriteBatch spriteBatch)
        {
            if (Main.LocalPlayer.HeldItem.ModItem is SaveStructure)
            {
                spriteBatch.End();
                spriteBatch.Begin(default, default, default, default, default, default, Main.GameViewMatrix.ZoomMatrix);

                Texture2D tex = ModContent.Request<Texture2D>("Overthrown/Assets/Textures/Corner").Value;
                Texture2D tex2 = ModContent.Request<Texture2D>("Overthrown/Assets/Textures/Box").Value;
                Point16 TopLeft = (Main.LocalPlayer.HeldItem.ModItem as SaveStructure).TopLeft;
                int Width = (Main.LocalPlayer.HeldItem.ModItem as SaveStructure).Width;
                int Height = (Main.LocalPlayer.HeldItem.ModItem as SaveStructure).Height;

                float tileScale = 16 * Main.GameViewMatrix.Zoom.Length() * 0.707106688737f;
                Vector2 pos = (Main.MouseWorld / tileScale).ToPoint16().ToVector2() * tileScale - Main.screenPosition;
                pos = Vector2.Transform(pos, Matrix.Invert(Main.GameViewMatrix.ZoomMatrix));
                pos = Vector2.Transform(pos, Main.UIScaleMatrix);

                spriteBatch.Draw(tex, pos, tex.Frame(), Color.White * 0.5f, 0, tex.Frame().Size() / 2, 1, 0, 0);

                if (Width != 0 && TopLeft != default)
                {
                    spriteBatch.Draw(tex2, new Rectangle((int)(TopLeft.X * 16 - Main.screenPosition.X), (int)(TopLeft.Y * 16 - Main.screenPosition.Y), Width * 16 + 16, Height * 16 + 16), tex2.Frame(), Color.White * 0.15f);
                    spriteBatch.Draw(tex, (TopLeft.ToVector2() + new Vector2(Width + 1, Height + 1)) * 16 - Main.screenPosition, tex.Frame(), Color.Red, 0, tex.Frame().Size() / 2, 1, 0, 0);
                }
                if (TopLeft != default) spriteBatch.Draw(tex, TopLeft.ToVector2() * 16 - Main.screenPosition, tex.Frame(), Color.Cyan, 0, tex.Frame().Size() / 2, 1, 0, 0);

                spriteBatch.End();
                spriteBatch.Begin(default, default, default, default, default, default, Main.UIScaleMatrix);
            }

            if (Main.LocalPlayer.HeldItem.ModItem is SaveMultiStructure)
            {
                spriteBatch.End();
                spriteBatch.Begin(default, default, default, default, default, default, Main.GameViewMatrix.ZoomMatrix);

                Texture2D tex = ModContent.Request<Texture2D>("Overthrown/Assets/Textures/Corner").Value;
                Texture2D tex2 = ModContent.Request<Texture2D>("Overthrown/Assets/Textures/Box").Value;
                Point16 TopLeft = (Main.LocalPlayer.HeldItem.ModItem as SaveMultiStructure).TopLeft;
                int Width = (Main.LocalPlayer.HeldItem.ModItem as SaveMultiStructure).Width;
                int Height = (Main.LocalPlayer.HeldItem.ModItem as SaveMultiStructure).Height;
                int count = (Main.LocalPlayer.HeldItem.ModItem as SaveMultiStructure).StructureCache.Count;

                float tileScale = 16 * Main.GameViewMatrix.Zoom.Length() * 0.707106688737f;
                Vector2 pos = (Main.MouseWorld / tileScale).ToPoint16().ToVector2() * tileScale - Main.screenPosition;
                pos = Vector2.Transform(pos, Matrix.Invert(Main.GameViewMatrix.ZoomMatrix));
                pos = Vector2.Transform(pos, Main.UIScaleMatrix);

                spriteBatch.Draw(tex, pos, tex.Frame(), Color.White * 0.5f, 0, tex.Frame().Size() / 2, 1, 0, 0);

                if (Width != 0 && TopLeft != default)
                {
                    spriteBatch.Draw(tex2, new Rectangle((int)(TopLeft.X * 16 - Main.screenPosition.X), (int)(TopLeft.Y * 16 - Main.screenPosition.Y), Width * 16 + 16, Height * 16 + 16), tex2.Frame(), Color.White * 0.15f);
                    spriteBatch.Draw(tex, (TopLeft.ToVector2() + new Vector2(Width + 1, Height + 1)) * 16 - Main.screenPosition, tex.Frame(), Color.Yellow, 0, tex.Frame().Size() / 2, 1, 0, 0);
                }
                if (TopLeft != default) spriteBatch.Draw(tex, TopLeft.ToVector2() * 16 - Main.screenPosition, tex.Frame(), Color.LimeGreen, 0, tex.Frame().Size() / 2, 1, 0, 0);

                spriteBatch.End();
                spriteBatch.Begin();
                Utils.DrawBorderString(spriteBatch, "Structures to save: " + count, Main.MouseScreen + new Vector2(0, 30), Color.White);

                spriteBatch.End();
                spriteBatch.Begin(default, default, default, default, default, default, Main.UIScaleMatrix);
            }
        }
    }
}