using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;
using Overthrown.Content.Rarities;

namespace Overthrown.Content.Tiles
{
    class NullBlock : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            TileID.Sets.DrawsWalls[Type] = true;
        }
    }
    class NullBlockItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Null Block");
            Tooltip.SetDefault("A block to place in a structure to prevent the generator from touching already placed tiles"
                + "\nIf you are looking to prevent messing with walls, use the Null Wall instead"
                + "\nDEV ITEM - Used for Structure Generation");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 2;
            Item.useTime = 2;
            Item.useStyle = 1;
            Item.rare = ItemRarityID.Red;
            Item.createTile = ModContent.TileType<NullBlock>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
    class NullWall : ModWall { }

    class NullWallItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Null Wall");
            Tooltip.SetDefault("A wall to place in a structure to prevent the generator from touching already placed walls"
            + "\nIf you are looking to prevent messing with blocks, use the Null Block instead"
            + "\nDEV ITEM - Used for Structure Generation");
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 2;
            Item.useTime = 2;
            Item.useStyle = 1;
            Item.rare = ItemRarityID.Red;
            Item.createWall = ModContent.WallType<NullWall>();
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtWall, 4)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
