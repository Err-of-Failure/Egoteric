using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.IO;
using Terraria.ID;
using Egoteric.Content.Rarities;
using Egoteric.World;

namespace Egoteric.Content.Items.StructureCreation
{
    internal class SaveMultiStructure : ModItem
    {
        public bool point2;
        public Point16 TopLeft;
        public int Width;
        public int Height;
        internal List<TagCompound> StructureCache = new List<TagCompound>();

        public Rectangle target => new Rectangle(TopLeft.X, TopLeft.Y, Width, Height);

        public override bool CanRightClick() => true;

        public override bool AltFunctionUse(Player player) => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Multistructure Saver");
            Tooltip.SetDefault("Just like the Strucure Save, Left click to select the first point and click again to select the second point"
                + "\nRight click to add a structure to the Multistructure list"
                + "\nRight click in the inventory to save the Multistructure to a file"
                + "\nDEV ITEM");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 1;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.rare = ModContent.RarityType<PyxlHigherRarity>();
        }

        public override void RightClick(Player player)
        {
            Item.stack++;
            if (StructureCache.Count > 1)
                StructureSaver.SaveMultistructureToFile(ref StructureCache);
            else
                Main.NewText("Too few structures! If you want to save a single structure, use the Structure Saver instead!", Color.Red);
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2 && !point2 && TopLeft != default)
                StructureCache.Add(StructureSaver.SaveStructure(target));

            else if (!point2)
            {
                TopLeft = (Main.MouseWorld / 16).ToPoint16();
                Width = 0;
                Height = 0;
                Main.NewText("Select the Second Point");
                point2 = true;
            }

            else
            {
                Point16 bottomRight = (Main.MouseWorld / 16).ToPoint16();
                Width = bottomRight.X - TopLeft.X - 1;
                Height = bottomRight.Y - TopLeft.Y - 1;
                Main.NewText("Structure is ready to add to the list!"
                    + "\nRight click to add this structure, Right click in inventory to save all structures to a file");
                point2 = false;
            }

            return true;
        }
    }
}
