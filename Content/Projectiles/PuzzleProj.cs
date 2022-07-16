
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Overthrown.Content.Items.Weapons.Throwables;

namespace Overthrown.Content.Projectiles
{
    public class Solved : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solved Cube Projectile");
        }

        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 74;
            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;

            AIType = ProjectileID.ThrowingKnife;
        }
        public override string Texture => "Overthrown/Content/Items/Weapons/Throwables/PuzzleCube";
    }
    public class Checkerboard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Checkerboard Cube Projectile");
        }

        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 74;
            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;

            AIType = ProjectileID.ThrowingKnife;
        }
        public override string Texture => "Overthrown/Content/Items/Weapons/Throwables/PuzzleCube_Checkerboard";
    }
    public class Dots : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dots Cube Projectile");
        }

        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 74;
            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;

            AIType = ProjectileID.ThrowingKnife;
        }
        public override string Texture => "Overthrown/Content/Items/Weapons/Throwables/PuzzleCube_Dots";
    }
    public class Superflip : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Superflip Cube Projectile");
        }

        public override void SetDefaults()
        {
            Projectile.width = 56;
            Projectile.height = 74;
            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Throwing;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;

            AIType = ProjectileID.ThrowingKnife;
        }
        public override string Texture => "Overthrown/Content/Items/Weapons/Throwables/PuzzleCube_Superflip";
    }
}
