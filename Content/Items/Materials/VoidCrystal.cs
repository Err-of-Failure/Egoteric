using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Egoteric.Content.Rarities;

namespace Egoteric.Content.Items.Materials
{
    internal class VoidCrystal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Crystal");
            Tooltip.SetDefault("Beta Resource Item");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;

            Item.buyPrice(gold: 1, silver: 20);
            Item.maxStack = 999;

            Item.rare = ModContent.RarityType<PyxlBaseRarity>();
        }
    }
}