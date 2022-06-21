using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Overthrown.Content.Rarities;

namespace Overthrown.Content.Items.Armor.Vanity
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
			Item.width = 18;
			Item.height = 18;

			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ModContent.RarityType<PyxlHigherRarity>();
			Item.vanity = true;
			Item.maxStack = 1;
        }
	}
}
