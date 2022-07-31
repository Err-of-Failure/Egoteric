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

namespace Egoteric.Content.Items.Weapons.Summon
{
    public class SummonStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Test Summon Staff");
            Tooltip.SetDefault("Just a test, will not stay as an item");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true;
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 100;
            Item.width = 32;
            Item.height = 32;
            Item.mana = 10;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(gold: 20, silver: 50);
            Item.rare = ModContent.RarityType<VidiaHigherRarity>();
            Item.DamageType = DamageClass.Summon;
            Item.UseSound = SoundID.Item44;

            Item.shoot = ModContent.ProjectileType<TestMinion>();

            Item.buffType = ModContent.BuffType<TestMinionBuff>();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
            projectile.originalDamage = Item.damage;
            return false;
        }
        public override bool? UseItem(Player player)
        {
            return base.UseItem(player);
        }
    }
}
