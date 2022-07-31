using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.IO;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.DataStructures;
using Egoteric.Content.DamageClasses;
using Egoteric.Content.Rarities;
using Egoteric.Content.Items.Materials;
using Egoteric.Content.Projectiles.Minions;
using Egoteric.Content.Buffs.Minions;

namespace Egoteric.Content.Buffs.Minions
{
    public class TestMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Test Minion");
            Description.SetDefault("This will not be here once we can better figure out summons items.");

            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<TestMinion>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
