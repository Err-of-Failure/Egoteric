using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Overthrown.Content.GUI;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Overthrown.World;

namespace Overthrown.World.ChestHelper.GUI
{
    internal class ChestCustomizer : UIState
    {
        public static bool Visible;

        internal UIList ruleElements = new UIList();
        internal UIScrollbar scrollBar = new UIScrollbar();

        UIImageButton NewGuaranteed = new UIImageButton(ModContent.Request<Texture2D>("Overthrown/Content/GUI/RedPlus"));
        UIImageButton NewChance = new UIImageButton(ModContent.Request<Texture2D>("Overthrown/Content/GUI/GreenPlus"));
        UIImageButton NewPool = new UIImageButton(ModContent.Request<Texture2D>("Overthrown/Content/GUI/PinkPlus"));
        UIImageButton NewPoolChance = new UIImageButton(ModContent.Request<Texture2D>("Overthrown/Content/GUI/BluePlus"));

        public static UIImageButton closeButton = new UIImageButton(ModContent.Request<Texture2D>("Overthrown/Content/GUI/RedX"));

        public override void OnInitialize()
        {
            GeneratorMenu.SetDims(ruleElements, -200, 0.5f, 0, 0.1f, 400, 0, 0, 0.8f);
            GeneratorMenu.SetDims(scrollBar, 232, 0.5f, 0, 0.1f, 32, 0, 0, 0.8f);
            ruleElements.SetScrollbar(scrollBar);
            Append(ruleElements);
            Append(scrollBar);

            GeneratorMenu.SetDims(NewGuaranteed, -200, 0.5f, -50, 0.1f, 32, 0, 32, 0);
            NewGuaranteed.OnClick += (n, m) => ruleElements.Add(new GuarunteedRuleElement());
            Append(NewGuaranteed);

            GeneratorMenu.SetDims(NewChance, -160, 0.5f, -50, 0.1f, 32, 0, 32, 0);
            NewChance.OnClick += (n, m) => ruleElements.Add(new ChanceRuleElement());
            Append(NewChance);

            GeneratorMenu.SetDims(NewPool, -120, 0.5f, -50, 0.1f, 32, 0, 32, 0);
            NewPool.OnClick += (n, m) => ruleElements.Add(new PoolRuleElement());
            Append(NewPool);

            GeneratorMenu.SetDims(NewPoolChance, -80, 0.5f, -50, 0.1f, 32, 0, 32, 0);
            NewPoolChance.OnClick += (n, m) => ruleElements.Add(new PoolChanceRuleElement());
            Append(NewPoolChance);

            GeneratorMenu.SetDims(closeButton, 200 - 32, 0.5f, -50, 0.1f, 32, 0, 32, 0);
            closeButton.OnClick += (n, m) => Visible = false;
            Append(closeButton);
        }

        public bool SetData(ChestEntity entity)
        {
            entity.rules.Clear();

            if (ruleElements.Count == 0)
            {
                entity.Kill(entity.Position.X, entity.Position.Y);
                return false;
            }

            for (int k = 0; k < ruleElements.Count; k++)
            {
                entity.rules.Add((ruleElements._items[k] as ChestRuleElement).rule.Clone());
            }

            return true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Recalculate();

            var color = new Color(49, 84, 141);

            GeneratorMenu.DrawBox(spriteBatch, NewGuaranteed.GetDimensions().ToRectangle(), NewGuaranteed.IsMouseHovering ? color : color * 0.8f);
            GeneratorMenu.DrawBox(spriteBatch, NewChance.GetDimensions().ToRectangle(), NewChance.IsMouseHovering ? color : color * 0.8f);
            GeneratorMenu.DrawBox(spriteBatch, NewPool.GetDimensions().ToRectangle(), NewPool.IsMouseHovering ? color : color * 0.8f);
            GeneratorMenu.DrawBox(spriteBatch, NewPoolChance.GetDimensions().ToRectangle(), NewPoolChance.IsMouseHovering ? color : color * 0.8f);

            GeneratorMenu.DrawBox(spriteBatch, closeButton.GetDimensions().ToRectangle(), closeButton.IsMouseHovering ? color : color * 0.8f);

            var rect = ruleElements.GetDimensions().ToRectangle();
            rect.Inflate(30, 10);
            GeneratorMenu.DrawBox(spriteBatch, rect, new Color(20, 40, 60) * 0.8f);

            if (rect.Contains(Main.MouseScreen.ToPoint()))
                Main.LocalPlayer.mouseInterface = true;

            if (NewGuaranteed.IsMouseHovering)
            {
                Main.hoverItemName = "Add New Guaranteed Rule";
                Main.LocalPlayer.mouseInterface = true;
            }

            if (NewChance.IsMouseHovering)
            {
                Main.hoverItemName = "Add New Chance Rule";
                Main.LocalPlayer.mouseInterface = true;
            }

            if (NewPool.IsMouseHovering)
            {
                Main.hoverItemName = "Add New Pool Rule";
                Main.LocalPlayer.mouseInterface = true;
            }

            if (NewPoolChance.IsMouseHovering)
            {
                Main.hoverItemName = "Add New Pool + Chance Rule";
                Main.LocalPlayer.mouseInterface = true;
            }

            if (closeButton.IsMouseHovering)
            {
                Main.hoverItemName = "Close";
                Main.LocalPlayer.mouseInterface = true;
            }

            base.Draw(spriteBatch);
        }
    }
}
