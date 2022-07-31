using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Egoteric.Content.DamageClasses;
using Egoteric.Content.Rarities;
using Egoteric.Content.Items.Materials;

namespace Egoteric.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    internal class VoidHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Helmet");
            Tooltip.SetDefault("Beta Item");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        /*
        public override bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
        {
            if (line.Mod == "Terraria" && line.Name == "ItemName")
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Main.UIScaleMatrix);
                GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(ItemID.NebulaDye), Item, null);
                Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2(line.X, line.Y), Color.White, 1);
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
                return false;
            }
            return true;
        }
        */
        public override void SetDefaults()
        {
            Item.width = 18; 
            Item.height = 18; 
            Item.value = Item.sellPrice(gold: 1); 
            Item.rare = ModContent.RarityType<PyxlHigherRarity>();
            Item.defense = 5; 
        }

        /*
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            
        }
        */

        public override void UpdateArmorSet(Player player)
        {
            
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<VoidCrystal>(), 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
