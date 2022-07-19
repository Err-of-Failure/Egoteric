using Egoteric.Content.Items;
using Egoteric.Content.Items.Armor;
using Egoteric.Content.Items.Armor.Vanity;
//using Egoteric.Content.Items.Weapons.Magic;
using Egoteric.Content.Items.Weapons.Throwables;
//using Egoteric.Content.Items.Weapons.Ranged;
//using Egoteric.Content.Items.Weapons.Summon;
using Egoteric.Content.Items.Weapons.Melee;
using Egoteric.Content.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;

namespace Egoteric.Common.Players
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
