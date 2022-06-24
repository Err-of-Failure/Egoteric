using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Overthrown.Content.Rarities;

namespace Overthrown.Content.Items.Armor.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class TorchHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Torch's Head");
			Tooltip.SetDefault("Seems like the head of a friendly dragon");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 28;

			Item.value = Item.sellPrice(gold: 1);
			Item.rare = ModContent.RarityType<VidiaHigherRarity>();
			Item.vanity = true;
			Item.maxStack = 1;
		}
	}
}
