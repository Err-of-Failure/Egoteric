using Egoteric.Content.Items;
using Egoteric.Content.Items.Armor;
using Egoteric.Content.Items.Armor.Vanity;
//using Egoteric.Content.Items.Weapons.Magic;
using Egoteric.Content.Items.Weapons.Throwables;
//using Egoteric.Content.Items.Weapons.Ranged;
//using Egoteric.Content.Items.Weapons.Summon;
using Egoteric.Content.Items.Weapons.Melee;
using Egoteric.Content.Tiles;
using Egoteric.Content.GUI;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Terraria.ModLoader.IO;
using Terraria.GameInput;

namespace Egoteric.Common.Players
{
	/// <summary>
	/// This mods Player variables.
	/// </summary>
    public class EgotericPlayer : ModPlayer
    {
		/// <summary>
		/// This tells the current amount of XP a player has stored
		/// </summary>
		public static int curEXP = 0;

		/// <summary>
		/// The player's current level
		/// </summary>
		public static int curLevel = 1;

		/// <summary>
		/// How many skill points the player has availible
		/// </summary>
		public static int skillPoints = 1;

		/// <summary>
		/// Total EXP needed to get to next level
		/// </summary>
		public static int maxEXP = (int)(100 * Math.Pow((0.55 * curLevel), 2));

		/// <summary>
		/// What upgrades the player has chosen.
		/// 
		/// <list type="bullet">
		///		<item>UpgradePaths[0] = Melee Upgrades</item>
		///		<item>UpgradePaths[1] = Ranged Upgrades</item>
		///		<item>UpgradePaths[2] = Magic Upgrades</item>
		///		<item>UpgradePaths[3] = Summon Upgrades</item>
		///		<item>UpgradePaths[4] = Throwing Upgrades</item>
		/// </list>
		/// </summary>
		public static int[] UpgradePaths = new int[5];

		public override void SaveData(TagCompound tag)
        {
			tag["curEXP"] = curEXP;
			tag["maxEXP"] = maxEXP;
			tag["curLevel"] = curLevel;
			tag["skillPoints"] = skillPoints;
			tag["UpgradePaths"] = UpgradePaths;
			base.SaveData(tag);
        }

        public override void LoadData(TagCompound tag)
        {
			curEXP = tag.GetInt("curEXP");
			maxEXP = tag.GetInt("maxEXP");
			curLevel = tag.GetInt("curLevel");
			skillPoints = tag.GetInt("skillPoints");
			UpgradePaths = tag.GetIntArray("UpgradePaths");
            base.LoadData(tag);
        }

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

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (EgotericKeybinds.checkCurrentStats.JustPressed)
            {
				Main.NewText("Current XP: " + curEXP + "\nTotal XP needed for next level: " + maxEXP + "\nCurrent Level: " + curLevel + "\nSkill Points Availible: " + skillPoints);
            }
			if (EgotericKeybinds.addLevel.JustPressed)
            {
				Main.NewText("Old Level: " + curLevel);
				curLevel++;
				Main.NewText("New Level: " + curLevel);
            }
			if (EgotericKeybinds.resetLevel.JustPressed)
            {
				Main.NewText("You've just lost " + (curLevel - 1) + " levels!");
				curLevel = 1;
            }
			if (EgotericKeybinds.openUI.JustPressed)
            {
				LevelUp.Visible = true;
            }
			if (EgotericKeybinds.hideUI.JustPressed)
			{
				LevelUp.Visible = false;
			}
        }

        public override void PreUpdate()
        {
			if (maxEXP != (int)(100 * Math.Pow((0.55 * curLevel), 2)))
            {
				maxEXP = (int)(100 * Math.Pow((0.55 * curLevel), 2));
			}
            base.PreUpdate();
        }
    }
}
