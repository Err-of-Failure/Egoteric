using Egoteric.Content.Items;
using Egoteric.Content.Items.Armor;
using Egoteric.Content.Items.Armor.Vanity;
//using Egoteric.Content.Items.Weapons.Magic;
using Egoteric.Content.Items.Weapons.Throwables;
//using Egoteric.Content.Items.Weapons.Ranged;
//using Egoteric.Content.Items.Weapons.Summon;
//using Egoteric.Content.Items.Weapons.Melee;
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
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
		public int curEXP = 0;
		/// <summary>
		/// The player's current level
		/// </summary>
		public int curLevel = 1;
		/// <summary>
		/// How many skill points the player has availible
		/// </summary>
		public int skillPoints = 0;
		/// <summary>
		/// Just used to check for certain algorithms.
		/// </summary>
		public int spentSkillPoints = 0;
		/// <summary>
		/// Total EXP needed to get to next level
		/// </summary>
		public int maxEXP = 100;

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
		public int[] UpgradePaths = new int[5];

		/// <summary>
		/// More Minions
		/// </summary>
		public int MoreMinions = 0;
		/// <summary>
		/// More Flat Damage
		/// </summary>
		public float IncreasedDamage = 0f;
		/// <summary>
		/// More Flat Defense
		/// </summary>
		public int IncreasedDefense = 0;
		/// <summary>
		/// More Max Health for the player;
		/// </summary>
		public int MaxLife = 0;

		public override void SaveData(TagCompound tag)
        {
			tag["curEXP"] = curEXP;
			tag["maxEXP"] = maxEXP;
			tag["curLevel"] = curLevel;
			tag["skillPoints"] = skillPoints;
			tag["UpgradePaths"] = UpgradePaths;
			tag["MoreMinions"] = MoreMinions;
			tag["IncreasedDamage"] = IncreasedDamage;
			tag["IncreasedDefense"] = IncreasedDefense;
			tag["MaxLife"] = MaxLife;
			base.SaveData(tag);
        }

        public override void LoadData(TagCompound tag)
        {
			curEXP = tag.GetInt("curEXP");
			maxEXP = tag.GetInt("maxEXP");
			curLevel = tag.GetInt("curLevel");
			skillPoints = tag.GetInt("skillPoints");
			UpgradePaths = tag.GetIntArray("UpgradePaths");
			MoreMinions = tag.GetInt("MoreMinions");
			IncreasedDamage = tag.GetFloat("IncreasedDamage");
			IncreasedDefense = tag.GetInt("IncreasedDefense");
			MaxLife = tag.GetInt("MaxLife");
			base.LoadData(tag);

			if (curEXP > maxEXP)
			{
				LevelUp();
			}
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
					new Item(ModContent.ItemType<RetroTorchHead>()),
					new Item(ModContent.ItemType<RetroTorchHoodie>()),
					new Item(ModContent.ItemType<RetroTorchShorts>())
				};
            }

			return Enumerable.Empty<Item>();
		}

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (EgotericKeybinds.checkCurrentStats.JustPressed)
            {
				Main.NewText(Player.name + "'s Stats:\n" + "Current XP: " + curEXP + "\nTotal XP needed for next level: " + maxEXP + "\nCurrent Level: " + curLevel + "\nSkill Points Availible: " + skillPoints);
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
				curEXP = 0;
				skillPoints = 0;
				for (int i = 0; i < UpgradePaths.Length; i++)
                {
					UpgradePaths[i] = 0;
                }
				MoreMinions = 0;
				IncreasedDamage = 0f;
				IncreasedDefense = 0;
			}
			if (EgotericKeybinds.openUI.JustPressed)
            {
				if (LevelsScreen.Added == false)
				{
					LevelsScreen.AddCharacterImage(Player);
				}
				LevelsScreen.Visible = true;
            }
			if (EgotericKeybinds.hideUI.JustPressed)
			{
				LevelsScreen.RemoveCharacterImage(LevelsScreen.playerImage);
				LevelsScreen.Visible = false;
			}
        }

        public override void PreUpdate()
        {
			if (maxEXP != (int)(300 * Math.Pow((0.75 * curLevel), 2)))
            {
				maxEXP = (int)(300 * Math.Pow((0.75 * curLevel), 2));
			}
			if (curEXP >= maxEXP)
				LevelUp();
			if (curEXP < 0)
				curEXP = 0;

			if (LevelsScreen.Added == true)
			{
				if (Player.velocity.X > 0 || Player.velocity.X < 0 || Player.velocity.Y > 0 || Player.velocity.Y < 0)
				{
					LevelsScreen.playerImage.SetAnimated(true);
				}
				else
				{
					LevelsScreen.playerImage.SetAnimated(false);
				}
			}

			base.PreUpdate();
        }

		public void LevelUp()
        {
			curEXP -= maxEXP;
			curLevel++;
			Main.NewText("You've leveled up! You are now level " + curLevel + "!", Color.Yellow);
			skillPoints++;
			Main.NewText("You currently have " + skillPoints + " skillpoints available!", Color.Blue);
			if (curEXP < 0)
				curEXP = 0;
			if (curEXP >= maxEXP)
				LevelUp();
		}

		public void AddXP(int XPToEarn)
        {
			curEXP += XPToEarn;
			if (curEXP >= maxEXP)
				LevelUp();
		}

		public void SetXP(float XPToSet)
        {
			curEXP = (int)XPToSet;
		}

		/// <summary>
		/// <para>The function called upon to spend the skill points a player has</para>
		/// <para>The class upgrades are listen as follows</para>
		/// <list type="bullet">
		///		<item>"Melee" - Melee Class Upgrades</item>
		///		<item>"Ranged" - Ranged Class Upgrades</item>
		///		<item>"Magic" - Magic Class Upgrades</item>
		///		<item>"Summon" - Summoner Class Upgrades</item>
		///		<item>"Throwing" - Throwing Class Upgrades, more than likely useless for now</item>
		/// </list>
		/// <para>Extra, more expensive upgrades (put at least 2 in the <paramref name="skillPointsRequired"/> value</para>
		/// <list type="bullet">
		///		<item>"MoreMinions" - Adding 1 more minion to the players minion count</item>
		///		<item>"IncreasedDamage" - Increases flat damage value</item>
		///		<item>"IncreasedDefense" - Increases flat defense value</item>
		///		<item>"MaxLife" - Increases max life value (WILL have a cap at 20 upgrades, 1 per heart)</item>
		/// </list>
		/// </summary>
		/// <param name="UpgradeID">The ID of the Upgrade you need</param>
		/// <param name="skillPointsRequired">The amount of skillpoints required to spend</param>
		public void SpendSkillPoint(string UpgradeID, int skillPointsRequired = 1)
        {
			if ((skillPoints - skillPointsRequired) >= 0)
			{
				skillPoints -= skillPointsRequired;
				spentSkillPoints += skillPointsRequired;
			}

			switch (UpgradeID)
            {
				case "Melee":
					UpgradePaths[0]++;
					break;
				case "Ranged":
					UpgradePaths[1]++;
					break;
				case "Magic":
					UpgradePaths[2]++;
					break;
				case "Summon":
					UpgradePaths[3]++;
					break;
				case "Throwing":
					UpgradePaths[4]++;
					break;
				case "MoreMinions":
					MoreMinions++;
					break;
				case "IncreasedDamage":
					IncreasedDamage++;
					break;
				case "IncreasedDefense":
					IncreasedDefense++;
					break;
				case "MaxLife":
					MaxLife++;
					break;
				default:
					break;
            }
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
			ModPacket packet = Mod.GetPacket();
			packet.Write((byte)Egoteric.MessageType.SyncPlayer);
			packet.Write((byte)Player.whoAmI);
			packet.Write(curEXP);
			packet.Write(curLevel);
			packet.Write(skillPoints);
			packet.Send(toWho, fromWho);
		}
	}
}
