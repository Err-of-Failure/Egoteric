﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Egoteric.Content.Rarities;
using Egoteric.Common.World;

namespace Egoteric.Content.Items.StructureCreation
{
    internal class SaveStructure : ModItem
    {
        public bool point2;
        public Point16 TopLeft;
        public int Width;
        public int Height;

        public override bool AltFunctionUse(Player player) => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Structure Saver");
            Tooltip.SetDefault("Left click once to select the Top Left point of the structure"
                + "\nLeft click again to select the Bottom Right point of the structure"
                + "\nRight click saves the structure to a file"
                + "\nIf you left click again after selecting a second point, you have to reselect the two points"
                + "\nDEV ITEM");
        }
        public override void SetDefaults()
        {
            Item.useStyle = 1;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.rare = ItemRarityID.LightPurple;
        }

        public override bool? UseItem(Player player)
        {
            if (player.altFunctionUse == 2 && !point2 && TopLeft != default)
                StructureSaver.SaveToFile(new Rectangle(TopLeft.X, TopLeft.Y, Width, Height));

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
                Main.NewText("The structure is ready to save! Right click to save.");
                point2 = false;
            }

            return true;
        }
    }
}
