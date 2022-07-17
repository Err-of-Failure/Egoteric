using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Overthrown.Content.Rarities;

namespace Overthrown.Content.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class PyxlHead :ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pyxl's Hair");
			Tooltip.SetDefault("Beta Item");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 20;

			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ModContent.RarityType<PyxlHigherRarity>();
			Item.vanity = true;
			Item.maxStack = 1;
		}
	}
}
