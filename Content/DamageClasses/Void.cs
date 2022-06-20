using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Overthrown.Content.DamageClasses
{
    internal class Void : DamageClass
    {
        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Generic)
                return StatInheritanceData.Full;

            return new StatInheritanceData(
                damageInheritance: 0f,
                critChanceInheritance: 0f,
                attackSpeedInheritance: 0f,
                armorPenInheritance: 0f,
                knockbackInheritance: 0f
            );
        }

        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Melee)
                return true;
            if (damageClass == DamageClass.Magic)
                return true;

            return false;
        }
        
        public override void SetDefaultStats(Player player)
        {
            player.GetCritChance<Void>() += 5f;
            player.GetArmorPenetration<Void>() += 1.5f;
            player.GetDamage<Void>() += 0.5f;
        }

        public override bool UseStandardCritCalcs => true;

        public override bool ShowStatTooltipLine(Player player, string lineName)
        {
            if (lineName == "Speed")
                return false;

            return true;
        }
    }
}
