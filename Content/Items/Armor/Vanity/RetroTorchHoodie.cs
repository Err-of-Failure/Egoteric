using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Egoteric.Content.Rarities;

namespace Egoteric.Content.Items.Armor.Vanity
{
	[AutoloadEquip(EquipType.Body)]
	public class RetroTorchHoodie : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Retro Torch's Hoodie");
			Tooltip.SetDefault("Hood not included");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 22;

			Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Red;
            Item.vanity = true;
			Item.maxStack = 1;
		}
	}
}
