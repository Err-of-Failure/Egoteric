using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Overthrown.Content.Projectiles;
using Overthrown.Content.Rarities;

namespace Overthrown.Content.Items.Weapons.Throwables
{
    public class PuzzleCube : ModItem
    {
        public static int Sequence;
        public static string CustomTooltip = "Solved, Ichor Effect";
        public static dynamic Cube = ModContent.ProjectileType<Solved>();
        public static string Path = "Overthrown/Content/Items/Weapons/Throwables/PuzzleCube";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Puzzle Cube");
            Tooltip.SetDefault("Right Click to switch between different states\n" 
                + CustomTooltip 
                + "\nCurrently Effects do nothing, this is only here as a placeholder");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }
        public override void SetDefaults()
        {
            Item.rare = ModContent.RarityType<PyxlHigherRarity>();
            Item.value = Item.sellPrice(gold: 25);

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 12;
            Item.useTime = 18;
            Item.UseSound = SoundID.Item71;
            Item.autoReuse = true;

            Item.damage = 50;
            Item.knockBack = 6.5f;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;

            Item.shootSpeed = 3.7f;
            Item.shoot = Cube;
        }
        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Sequence++;
                if (Sequence >= 4)
                    Sequence = 0;

                if (Sequence == 0)
                {
                    CustomTooltip = "Solved, Ichor Effect";
                    Cube = ModContent.ProjectileType<Solved>();
                    Path = "Overthrown/Content/Items/Weapons/Throwables/PuzzleCube";
                }
                else if (Sequence == 1)
                {
                    CustomTooltip = "Checkerboard, Poison Effect";
                    Cube = ModContent.ProjectileType<Checkerboard>();
                    Path = "Overthrown/Content/Items/Weapons/Throwables/PuzzleCube_Checkerboard";
                }
                else if (Sequence == 2)
                {
                    CustomTooltip = "Dots, Cursed Effect";
                    Cube = ModContent.ProjectileType<Dots>();
                    Path = "Overthrown/Content/Items/Weapons/Throwables/Dots";
                }
                else if (Sequence == 3)
                {
                    CustomTooltip = "Superflip, Fire Effect";
                    Cube = ModContent.ProjectileType<Superflip>();
                    Path = "Overthrown/Content/Items/Weapons/Throwables/Superflip";
                }
                return true;
            }
            return true;
        }
        public override string Texture => Path;
    }
}
