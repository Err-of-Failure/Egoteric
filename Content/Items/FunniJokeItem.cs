using System.Diagnostics;
using System.Runtime.InteropServices;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Egoteric.Content.Rarities;
using Egoteric;

namespace Egoteric.Content.Items
{
    public class RickRollItem : ModItem
    {
        private string FunniLink = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

        public override string Texture => "Egoteric/Content/Items/DevItem";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Free Bobux");
            Tooltip.SetDefault("Totaly Lejit, no gimix");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Expert;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 1;
            Item.useAnimation = 0;
            Item.autoReuse = true;

            Item.noMelee = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)
        {
            Egoteric.OpenLink(FunniLink);
            return false;
        }
    }
    public class NotepadItem : ModItem
    {
        public string NotePadText = "Test";
        public string NotePadTitle = "Weh";
        public override string Texture => "Egoteric/Content/Items/DevItem";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Random Message Popup");
            Tooltip.SetDefault("Just Testing out the notepad");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 1;

            Item.rare = ItemRarityID.Expert;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 1;
            Item.useAnimation = 0;
            Item.autoReuse = true;

            Item.noMelee = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)
        {
            Process notepad = Egoteric.OpenProgram("notepad.exe");
            if (notepad != null)
            {
                notepad.WaitForInputIdle();
                Egoteric.SetWindowText(notepad.MainWindowHandle, NotePadTitle);
                IntPtr child = Egoteric.FindWindowEx(notepad.MainWindowHandle, new IntPtr(0), "Edit", null);
                Egoteric.SendMessage(child, 0x000C, 0, NotePadText);
            }
            return false;
        }
    }
}
