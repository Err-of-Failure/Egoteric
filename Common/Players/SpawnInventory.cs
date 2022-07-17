using Overthrown.Content.Items;
using Overthrown.Content.Items.Armor;
using Overthrown.Content.Items.Armor.Vanity;
//using Overthrown.Content.Items.Weapons.Magic;
using Overthrown.Content.Items.Weapons.Throwables;
//using Overthrown.Content.Items.Weapons.Ranged;
//using Overthrown.Content.Items.Weapons.Summon;
using Overthrown.Content.Items.Weapons.Melee;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;

namespace Overthrown.Common.Players
{
    public class SpawnInventory : ModPlayer
    {
		public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
		{
			if (Player.name == "Pyxl")
			{
				return new[] 
				{
					new Item(ModContent.ItemType<PyxlShirt>()),
					new Item(ModContent.ItemType<PyxlHead>()),
					new Item(ModContent.ItemType<PyxlLegs>())
				};
			}
			if (Player.name == "Torch")
            {
				return new[]
				{
					new Item(ModContent.ItemType<TorchHead>()),
					new Item(ModContent.ItemType<TorchHoodie>()),
					new Item(ModContent.ItemType<TorchShorts>())
				};
            }

			return Enumerable.Empty<Item>();
		}

	}
}
