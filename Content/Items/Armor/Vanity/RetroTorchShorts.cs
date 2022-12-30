using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Egoteric.Content.Rarities;

namespace Egoteric.Content.Items.Armor.Vanity
{
	[AutoloadEquip(EquipType.Legs)]
	public class RetroTorchShorts : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Retro Torch's Jorts");
			Tooltip.SetDefault("Jorts are totally in style!");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 16;

			Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Red;
            Item.vanity = true;
			Item.maxStack = 1;
		}
	}
}
