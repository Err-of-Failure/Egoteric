using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Egoteric.Content.Projectiles;
using Egoteric.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace Egoteric.Content.Items.Weapons.Throwables
{
    public class PuzzleCube : ModItem
    {
        public static int Mode;
        public static string CustomTooltip = "Solved, Ichor Effect";
        public static string Path = "Egoteric/Content/Items/Weapons/Throwables/PuzzleCube";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Puzzle Cube");
            Tooltip.SetDefault("Right Click to switch between different states\n"
                + CustomTooltip
                + "\nCurrently Effects do nothing, this is only here as a placeholder");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
        }

        public override void SetDefaults()
        {
            Item.rare = ModContent.RarityType<PyxlHigherRarity>();
            Item.value = Item.sellPrice(gold: 25);

            Item.useStyle = ItemUseStyleID.Swing; //Might provide more of a throwing animations
            Item.useAnimation = 12;
            Item.useTime = 18;
            //Item.UseSound = SoundID.Item1;
            Item.autoReuse = false; //Probably shouldn't give them the ability to hold down the button, its obnoxious

            Item.damage = 50;
            Item.knockBack = 6.5f;
            Item.noUseGraphic = true;
            Item.DamageType = DamageClass.Throwing;
            Item.noMelee = true;

            Item.shootSpeed = 8.75f;
            Item.shoot = ModContent.ProjectileType<Solved>();
            Item.rare = ItemRarityID.Yellow;

            Item.maxStack = 999;
        }

        public string SetTooltip(int mode)
        {
            string Text;

            if (mode == 0)
            {
                CustomTooltip = "Solved, Ichor Effect";
            }
            else if (mode == 1)
            {
                CustomTooltip = "Checkerboard, Poison Effect";
            }
            else if (mode == 2)
            {
                CustomTooltip = "Dots, Cursed Effect";
            }
            else if (mode == 3)
            {
                CustomTooltip = "Superflip, Fire Effect";
            }

            Text = "Right Click to switch between different states\n" 
                + CustomTooltip
                + "\nCurrently Effects do nothing, this is only here as a placeholder";

            return Text;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                if (Mode == 3)
                {
                    Path = "Egoteric/Content/Items/Weapons/Throwables/PuzzleCube";
                    Mode = 0;
                    SetTooltip(Mode);
                    Item.useAnimation = 12;
                    Item.useTime = 18;
                    Item.noMelee = true;
                    Item.noUseGraphic = true;
                    Item.shoot = ModContent.ProjectileType<Solved>();
                    Item.rare = ItemRarityID.Yellow;
                    if (player.whoAmI == Main.myPlayer)
                    {
                        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), Color.Yellow, "Puzzle Cube Solved!", false, false);
                    }
                }
                else if (Mode == 0)
                {
                    Path = "Egoteric/Content/Items/Weapons/Throwables/PuzzleCube_Checkerboard";
                    Mode = 1;
                    SetTooltip(Mode);
                    Item.useAnimation = 12;
                    Item.useTime = 18;
                    Item.noMelee = true;
                    Item.noUseGraphic = true;
                    Item.shoot = ModContent.ProjectileType<Checkerboard>();
                    Item.rare = ItemRarityID.Green;
                    if (player.whoAmI == Main.myPlayer)
                    {
                        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), Color.Green, "Checkerboard Pattern!", false, false);
                    }
                }
                else if (Mode == 1)
                {
                    Path = "Egoteric/Content/Items/Weapons/Throwables/Dots";
                    Mode = 2;
                    SetTooltip(Mode);
                    Item.useAnimation = 12;
                    Item.useTime = 18;
                    Item.noMelee = true;
                    Item.noUseGraphic = true;
                    Item.shoot = ModContent.ProjectileType<Dots>();
                    Item.rare = ModContent.RarityType<PyxlHigherRarity>();
                    if (player.whoAmI == Main.myPlayer)
                    {
                        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), new Color((byte)(Main.DiscoR), 0, (byte)(Main.DiscoR)), "Dots Pattern!", false, false);
                    }
                }
                else if (Mode == 2)
                {
                    Path = "Egoteric/Content/Items/Weapons/Throwables/Superflip";
                    Mode = 3;
                    SetTooltip(Mode);
                    Item.useAnimation = 12;
                    Item.useTime = 18;
                    Item.noMelee = true;
                    Item.noUseGraphic = true;
                    Item.shoot = ModContent.ProjectileType<Superflip>();
                    Item.rare = ModContent.RarityType<VidiaHigherRarity>();
                    if (player.whoAmI == Main.myPlayer)
                    {
                        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y - 20, player.width, player.height), new Color((byte)(Main.DiscoR), 0, 0), "Superflip Pattern!", false, false);
                    }
                }
            }
            return base.CanUseItem(player);
        }

        public override bool? UseItem(Player player) => base.UseItem(player);

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
            SoundEngine.PlaySound(SoundID.Item1, new Vector2?(player.position));
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }

        public override bool AltFunctionUse(Player player) => true;
    }
}
