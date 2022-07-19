using Egoteric.Content.Items.Accessories;
using Egoteric.Content.DamageClasses;
using Egoteric.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;

namespace Egoteric.Content.Items.Accessories
{
    public class Abandonment : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abandonment");
			Tooltip.SetDefault("Beta Item"
				+ "\nIncreases your summoning capabilities substantially"
				+ "\nExtremely limits the use of other classes"
				+ "\n''If I'm the main character, then no one will leave... right?''");
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

        public override void SetDefaults()
        {
			Item.width = 38;
			Item.height = 44;
			Item.accessory = true;
			Item.master = true;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			//Base Damage (Percentage)
			player.GetDamage(DamageClass.Magic) -= 0.5f;
			player.GetDamage(DamageClass.Melee) -= 0.5f;
			player.GetDamage(DamageClass.Ranged) -= 0.5f;
			player.GetDamage(DamageClass.Summon) += 1f;
			player.GetDamage(DamageClass.Throwing) -= 0.5f;

			//Crit Chance (Int)
			player.GetCritChance(DamageClass.Summon) += 20f;
			player.GetCritChance(DamageClass.Magic) -= 10f;
			player.GetCritChance(DamageClass.Melee) -= 10f;
			player.GetCritChance(DamageClass.Ranged) -= 10f;
			player.GetCritChance(DamageClass.Throwing) -= 10f;

			//Attack Speed (Percentage)
			player.GetAttackSpeed(DamageClass.Ranged) -= 0.25f;
			player.GetAttackSpeed(DamageClass.Magic) -= 0.25f;
			player.GetAttackSpeed(DamageClass.Melee) -= 0.25f;
			player.GetAttackSpeed(DamageClass.Summon) += 0.5f;
			player.GetAttackSpeed(DamageClass.Throwing) -= 0.25f;

			//Armor Penetration (Int)
			player.GetArmorPenetration(DamageClass.Magic) -= 10f;
			player.GetArmorPenetration(DamageClass.Throwing) -= 10f;
			player.GetArmorPenetration(DamageClass.Melee) -= 10f;
			player.GetArmorPenetration(DamageClass.Ranged) -= 10f;
			player.GetArmorPenetration(DamageClass.Summon) += 20f;

			//Knockback (Percentage)
			player.GetKnockback(DamageClass.Magic) -= 0.5f;
			player.GetKnockback(DamageClass.Throwing) -= 0.5f;
			player.GetKnockback(DamageClass.Melee) -= 0.5f;
			player.GetKnockback(DamageClass.Ranged) -= 0.5f;
			player.GetKnockback(DamageClass.Summon) += 1f;

			//Summon Class Exclusive
			player.maxMinions += 10;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {
            if (slot < 10)
            {
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && (player.armor[i].type == ModContent.ItemType<Betrayal>() && player.armor[i].type == ModContent.ItemType<Death>() && player.armor[i].type == ModContent.ItemType<Isolation>()))
					{
						return false;
					}
				}
			}
			return true;
        }
    }
}
