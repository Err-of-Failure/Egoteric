using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Overthrown.Content.DamageClasses;

namespace Overthrown.Content.Items.Weapons
{
    internal class VoidSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Sword");
            Tooltip.SetDefault("Beta Item");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.autoReuse = true;

            Item.DamageType = ModContent.GetInstance<Void>();
            Item.damage = 150;
            Item.knockBack = 4.75f;
            Item.crit = 55;

            Item.value = Item.buyPrice(platinum: 10);
            Item.rare = ItemRarityID.Expert;

            Item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<VoidCrystal>(), 10)
                .AddRecipeGroup(RecipeGroupID.IronBar, 4)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
