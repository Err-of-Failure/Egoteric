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

            return new StatInheritanceData.None;
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
            player.GetCritChanc<Void>() += 5;
            player.GetArmorPenetration<Void>() += 2.5f;
            player.GetDamage<Void>() += 20;
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
