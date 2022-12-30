using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Egoteric.Content.GUI;
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
    internal class PlaceStructure : ModItem
    {
        public static bool ignoreNulls = false;
        public static bool UIVisible;

        public override bool AltFunctionUse(Player player) => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Structure Placer");
            Tooltip.SetDefault("Left click to place a specified structure" 
                + "\nRight click to open the structure selection GUI"
                + "\nDEV ITEM");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 1;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.rare = ItemRarityID.LightRed;
        }

        public override bool? UseItem(Player player)
        {
            if(player.altFunctionUse == 2)
            {
                UIVisible = !UIVisible;
                return true;
            }

            if(GeneratorMenu.selected != null)
            {
                var pos = new Point16(Player.tileTargetX, Player.tileTargetY);

                if (GeneratorMenu.multiMode)
                    StructureGenerator.GenerateMultistructureSpecific(GeneratorMenu.selected.Path, pos, Egoteric.Instance, GeneratorMenu.multiIndex, true, GeneratorMenu.ignoreNulls);

                else
                    StructureGenerator.GenerateStructure(GeneratorMenu.selected.Path, pos, Egoteric.Instance, true, GeneratorMenu.ignoreNulls);
            }
            else
                Main.NewText("There was no structure selected, press right click and select a structure from the GUI to generate it.", Color.Red);

            return true;
        }
    }
}
