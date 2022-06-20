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
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.autoReuse = true;

            Item.DamageType = ModContent.GetInstance<Void>();
            Item.damage = 20;
            Item.knockBack = 3.5f;
            Item.crit = 25;

            Item.value = Item.buyPrice(platinum: 20);
            Item.rare = ItemRarityID.Expert;

            Item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
        {
            
        }
    }
}
