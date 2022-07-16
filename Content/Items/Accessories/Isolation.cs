﻿using Overthrown.Content.Items.Accessories;
using Overthrown.Content.DamageClasses;
using Overthrown.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;

namespace Overthrown.Content.Items.Accessories
{
    public class Isolation : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Isolation");
			Tooltip.SetDefault("Beta Item"
				+ "\nIncreases your ranged and throwing capabilities substantially"
				+ "\nExtremely limits the use of other classes"
				+ "\n''I don't need anyone but myself...''");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
		{
			Item.width = 38;
			Item.height = 44;
			Item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			//Base Damage (Percentage)
			player.GetDamage(DamageClass.Ranged) += 1f;
			player.GetDamage(DamageClass.Throwing) += 1f;
			player.GetDamage(DamageClass.Melee) -= 0.5f;
			player.GetDamage(DamageClass.Magic) -= 0.5f;
			player.GetDamage(DamageClass.Summon) -= 0.5f;

			//Crit Chance (Int)
			player.GetCritChance(DamageClass.Throwing) += 20f;
			player.GetCritChance(DamageClass.Ranged) += 20f;
			player.GetCritChance(DamageClass.Summon) -= 10f;
			player.GetCritChance(DamageClass.Melee) -= 10f;
			player.GetCritChance(DamageClass.Magic) -= 10f;

			//Attack Speed (Percentage)
			player.GetAttackSpeed(DamageClass.Magic) -= 0.25f;
			player.GetAttackSpeed(DamageClass.Summon) -= 0.25f;
			player.GetAttackSpeed(DamageClass.Melee) -= 0.25f;
			player.GetAttackSpeed(DamageClass.Ranged) += 0.5f;
			player.GetAttackSpeed(DamageClass.Throwing) += 0.5f;

			//Armor Penetration (Int)
			player.GetArmorPenetration(DamageClass.Summon) -= 10f;
			player.GetArmorPenetration(DamageClass.Throwing) += 20f;
			player.GetArmorPenetration(DamageClass.Melee) -= 10f;
			player.GetArmorPenetration(DamageClass.Magic) -= 10f;
			player.GetArmorPenetration(DamageClass.Ranged) += 20f;

			//Knockback (Percentage)
			player.GetKnockback(DamageClass.Summon) -= 0.5f;
			player.GetKnockback(DamageClass.Throwing) += 1f;
			player.GetKnockback(DamageClass.Melee) -= 0.5f;
			player.GetKnockback(DamageClass.Magic) -= 0.5f;
			player.GetKnockback(DamageClass.Ranged) += 1f;
		}

		public override bool CanEquipAccessory(Player player, int slot, bool modded)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && (player.armor[i].type == ModContent.ItemType<Abandonment>() || player.armor[i].type == ModContent.ItemType<Death>() || player.armor[i].type == ModContent.ItemType<Betrayal>()))
					{
						return false;
					}
				}
			}
			return true;
		}
	}
}