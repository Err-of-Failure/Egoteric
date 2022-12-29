using Egoteric.Content.Items.Accessories;
//using Egoteric.Content.DamageClasses;
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
			player.GetDamage(DamageClass.Magic) *= 0.5f;
			player.GetDamage(DamageClass.Summon) *= 2.0f;
			player.GetDamage(DamageClass.Ranged) *= 0.5f;
			player.GetDamage(DamageClass.Throwing) *= 0.5f;
			player.GetDamage(DamageClass.Melee) *= 0.5f;

			//Crit Chance (Int)
			player.GetCritChance(DamageClass.Magic) *= 0.75f;
			player.GetCritChance(DamageClass.Summon) *= 1.5f;
			player.GetCritChance(DamageClass.Ranged) *= 0.75f;
			player.GetCritChance(DamageClass.Throwing) *= 0.75f;
			player.GetCritChance(DamageClass.Melee) *= 0.75f;

			//Attack Speed (Percentage)
			player.GetAttackSpeed(DamageClass.Magic) *= 0.75f;
			player.GetAttackSpeed(DamageClass.Summon) *= 1.5f;
			player.GetAttackSpeed(DamageClass.Ranged) *= 0.75f;
			player.GetAttackSpeed(DamageClass.Throwing) *= 0.75f;
			player.GetAttackSpeed(DamageClass.Melee) *= 0.75f;

			//Armor Penetration (Int)
			player.GetArmorPenetration(DamageClass.Magic) *= 0.75f;
			player.GetArmorPenetration(DamageClass.Summon) *= 1.5f;
			player.GetArmorPenetration(DamageClass.Ranged) *= 0.75f;
			player.GetArmorPenetration(DamageClass.Throwing) *= 0.75f;
			player.GetArmorPenetration(DamageClass.Melee) *= 0.75f;

			//Knockback (Percentage)
			player.GetKnockback(DamageClass.Magic) *= 0.5f;
			player.GetKnockback(DamageClass.Summon) *= 2.0f;
			player.GetKnockback(DamageClass.Ranged) *= 0.5f;
			player.GetKnockback(DamageClass.Throwing) *= 0.5f;
			player.GetKnockback(DamageClass.Melee) *= 0.5f;

			//Summon Class Exclusive
			player.maxMinions += 10; //Will more than likely be changed to a public int value later on
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
