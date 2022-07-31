using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Egoteric.Content.Rarities;

namespace Egoteric.Content.Items
{
    public class FunniJokeItem : ModItem
    {
        private string FunniLink = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
        private string OpenLinkCommand = "/C Start ";
        private void OpenLink(string Link)
        {
            Process.Start("cmd.exe", OpenLinkCommand + Link);
        }

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
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.autoReuse = true;

            Item.noMelee = true;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)
        {
            OpenLink(FunniLink);
            return base.UseItem(player);
        }
    }
}
