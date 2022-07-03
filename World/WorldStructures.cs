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
using StructureHelper;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Overthrown.World
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
    }
    public class TestHouseGen : GenPass
    {
        public TestHouseGen(string name, float loadWeight) : base(name, loadWeight) {
        }
        protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        {
            progress.Message = "Applying Test Structure Build";

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

                
                Point16 location = new Point16(x, y);

                Generator.GenerateStructure("World/Structures/BasicStarterHouse", location, Overthrown.Instance, false, false);

                placed = true;
            }
        }
    }
}
