using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Egoteric.Content.Rarities;

namespace Egoteric.Content.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Body)]
    public class PyxlShirt : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pyxl's Shirt");
			Tooltip.SetDefault("It's actually a little baggy");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
        {
			Item.width = 22;
			Item.height = 20;

			Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Purple;
            Item.vanity = true;
			Item.maxStack = 1;
        }
	}
}
