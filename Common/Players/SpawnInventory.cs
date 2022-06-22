using Overthrown.Content.Items;
using Overthrown.Content.Items.Armor;
using Overthrown.Content.Items.Armor.Vanity;
using Overthrown.Content.Items.Weapons;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Overthrown.Common.Players
{
    public class SpawnInventory : ModPlayer
    {
		private Player player;
		public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
		{
			if (player.name == "Pyxl")
			{
				return new[] 
				{
					new Item(ModContent.ItemType<PyxlShirt>())
				};
			}
			if (player.name == "Torch")
            {
				return new[]
				{
					new Item(ModContent.ItemType<TorchHead>()),
					new Item(ModContent.ItemType<TorchHoodie>()),
					new Item(ModContent.ItemType<TorchShorts>())
				};
            }

			return null;
		}

	}
}
