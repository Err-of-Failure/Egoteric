using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using System.Reflection;
using Terraria.Utilities;
using System.Runtime.Serialization.Formatters.Binary;
using Terraria.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures; 
using Egoteric.Common.World;

namespace Egoteric.Common.World
{
    public class WorldStructures : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int GenIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Sunflowers"));
            if (GenIndex == -1)
            {
                return;
            }
            tasks.Insert(GenIndex + 1, new TestHouseGen("TestStructure", 100f));
        }

        /// <summary>
        /// Checks if the given area is more or less flattish.
        /// Returns false if the average tile height variation is greater than the threshold.
        /// Expects that the first tile is solid, and traverses from there.
        /// Use the weight parameters to change the importance of up/down checks.
        /// </summary>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="width"></param>
        /// <param name="threshold"></param>
        /// <param name="goingDownWeight"></param>
        /// <param name="goingUpWeight"></param>
        /// <returns></returns>
        public static bool CheckFlat(int startX, int startY, int width, float threshold, int goingDownWeight = 0, int goingUpWeight = 0)
        {
            // Fail if the tile at the other end of the check plane isn't also solid
            if (!WorldGen.SolidTile(startX + width, startY)) return false;

            float totalVariance = 0;
            for (int i = 0; i < width; i++)
            {
                if (startX + i >= Main.maxTilesX) return false;

                // Fail if there is a tile very closely above the check area
                for (int k = startY - 1; k > startY - 100; k--)
                {
                    if (WorldGen.SolidTile(startX + i, k)) return false;
                }

                // If the tile is solid, go up until we find air
                // If the tile is not, go down until we find a floor
                int offset = 0;
                bool goingUp = WorldGen.SolidTile(startX + i, startY);
                offset += goingUp ? goingUpWeight : goingDownWeight;
                while ((goingUp && WorldGen.SolidTile(startX + i, startY - offset))
                    || (!goingUp && !WorldGen.SolidTile(startX + i, startY + offset)))
                {
                    offset++;
                }
                if (goingUp) offset--; // account for going up counting the first tile
                totalVariance += offset;
            }
            return totalVariance / width <= threshold;
        }
    }
    public class TestHouseGen : GenPass
    {
        public TestHouseGen(string name, float loadWeight) : base(name, loadWeight) {
        }
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Generating Test Structure Build";

            bool placed = false;
            int attempts = 0;

            while (!placed && attempts++ < 1000)
            {
                int x = WorldGen.genRand.Next(300, Main.maxTilesX / 4);

                if (WorldGen.genRand.NextBool())
                {
                    x = Main.maxTilesX - x;
                }

                int y = (int)Main.worldSurface - 200;

                while (!WorldGen.SolidTile(x, y) && y <= Main.worldSurface)
                {
                    y++;
                }

                if (y > Main.worldSurface)
                {
                    continue;
                }

                Point16 dimensions = new Point16(0, 0);

                StructureGenerator.GetDimensions("Common/World/Structures/BasicStarterHouse.nbt", Egoteric.Instance, ref dimensions, false);

                if (!WorldStructures.CheckFlat(x, y, dimensions.X, 3))
                    continue;

                int type = Main.tile[x, y].TileType;

                if (!(type == TileID.Dirt
                    || type == TileID.Grass
                    || type == TileID.Stone))
                {
                    continue;
                }

                //Point16 location = new Point16(x - 29, y - 26); //Test
                Point16 location = new Point16(x, y - dimensions.Y + 5); //Test2
                //Point16 location = new Point16(x, y);

                StructureGenerator.GenerateStructure("Common/World/Structures/BasicStarterHouse.nbt", location, Egoteric.Instance, false, false);

                placed = true;
            }
        }
    }
}
