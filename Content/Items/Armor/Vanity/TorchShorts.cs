using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Overthrown.Content.Rarities;

namespace Overthrown.Content.Items.Armor.Vanity
{
	[AutoloadEquip(EquipType.Legs)]
	public class TorchShorts : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Torch's Shorts");
			Tooltip.SetDefault("Jorts are totally in style!");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 16;

			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ModContent.RarityType<VidiaHigherRarity>();
			Item.vanity = true;
			Item.maxStack = 1;
		}
	}
}
