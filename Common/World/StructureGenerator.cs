﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Linq;
using Terraria.ID;
using Egoteric.Common.World.ChestHelper;

namespace Egoteric.Common.World
{
    //This is all copied from Structure Helper sadly, but it only because we didn't want to have a need to install Structure Helper just to use our mod.
    public static class StructureGenerator
    {
        internal static Dictionary<string, TagCompound> StructureDataCache = new Dictionary<string, TagCompound>();

        /// <summary>
        /// This method allows you to load a structure from a specific structure file within this mod.
        /// </summary>
        /// <param name="path">The path to the file within the mod.</param>
        /// <param name="pos">The position in which the structure is generated.</param>
        /// <param name="mod">The instance of the mod to grab the file from.</param>
        /// <param name="fullPath">Do you want to use a specific path instead of a path in your mod.</param>
        /// <param name="ignoreNull">If the structure should respect the normal behavior of null tiles or not.</param>
        public static bool GenerateStructure(string path, Point16 pos, Mod mod, bool fullPath = false, bool ignoreNull = false)
        {
            TagCompound tag = GetTag(path, mod, fullPath);

            if (!tag.ContainsKey("Version") || tag.GetString("Version")[0] <= 1)
                throw new Exception("Legacy structures from 1.3 versions of Structure Helper are not supported.");

            return Generate(tag, pos, ignoreNull);
        }

        /// <summary>
        /// This method generates a structure selected randomly from a multistructure file within the files.
        /// </summary>
        /// <param name="path">The path to the file within the mod.</param>
        /// <param name="pos">The position in which the structure is generated.</param>
        /// <param name="mod">The instance of your mod to grab the file from.</param>
        ///<param name="fullPath">Do you want to use a specific path instead of a path in your mod.</param>
        ///<param name="ignoreNull">If the structure should respect the normal behavior of null tiles or not.</param>
        public static bool GenerateMultistructureRandom(string path, Point16 pos, Mod mod, bool fullPath = false, bool ignoreNull = false)
        {
            TagCompound tag = GetTag(path, mod, fullPath);

            if (!tag.ContainsKey("Version") || tag.GetString("Version")[0] <= 1)
                throw new Exception("Legacy structures from 1.3 versions of Structure Helper are not supported.");

            var structures = (List<TagCompound>)tag.GetList<TagCompound>("Structures");
            int index = WorldGen.genRand.Next(structures.Count);
            TagCompound targetStructure = structures[index];

            return Generate(targetStructure, pos, ignoreNull);
        }

        /// <summary>
        /// This method generates a structure you select from a multistructure file within the files.
        /// </summary>
        /// <param name="path">The path to your multistructure file within the files.</param>
        /// <param name="pos">The position in which the structure is generated.</param>
        /// <param name="mod">The instance of the mod to grab the file from.</param>
        /// <param name="index">The index of the specific structure to generate from the multistructure file.</param>
        ///<param name="fullPath">Do you want to use a specific path instead of a path in your mod.</param>
        ///<param name="ignoreNull">If the structure should respect the normal behavior of null tiles or not.</param>
        public static bool GenerateMultistructureSpecific(string path, Point16 pos, Mod mod, int index, bool fullPath = false, bool ignoreNull = false)
        {
            TagCompound tag = GetTag(path, mod, fullPath);

            if (!tag.ContainsKey("Version") || tag.GetString("Version")[0] <= 1)
                throw new Exception("Legacy structures from 1.3 versions of Structure Helper are not supported.");

            var structures = (List<TagCompound>)tag.GetList<TagCompound>("Structures");

            if (index >= structures.Count || index < 0)
            {
                Egoteric.Instance.Logger.Warn($"Attempted to generate structure {index} in mutistructure containing {structures.Count - 1} structures.");
                return false;
            }

            TagCompound targetStructure = structures[index];

            return Generate(targetStructure, pos, ignoreNull);
        }

        /// <summary>
        /// Gets the dimensions of a structure from a structure file within your mod.
        /// </summary>
        /// <param name="path">The path to your multistructure file within the files.</param>
        /// <param name="mod">The instance of your mod to grab the file from.</param>
        /// <param name="dims">The Point16 variable which you want to be set to the dimensions of the structure.</param>
        /// <param name="fullPath">Do you want to use a specific path instead of a path in your mod.</param>
        /// <returns></returns>
        public static bool GetDimensions(string path, Mod mod, ref Point16 dims, bool fullPath = false)
        {
            TagCompound tag = GetTag(path, mod, fullPath);

            dims = new Point16(tag.GetInt("Width"), tag.GetInt("Height"));
            return true;
        }

        /// <summary>
        /// Gets the dimensions of a structure from a structure file within your mod.
        /// </summary>
        /// <param name="path">The path to your multistructure file within the files.</param>
        /// <param name="mod">The instance of your mod to grab the file from.</param>
        /// <param name="index">The index of the specific structure to generate dimensions from in the multistructure file.</param>
        /// <param name="dims">The Point16 variable which you want to be set to the dimensions of the structure.</param>
        /// <param name="fullPath">Do you want to use a specific path instead of a path in your mod.</param>
        /// <returns></returns>
        public static bool GetMultistructureDimensions(string path, Mod mod, int index, ref Point16 dims, bool fullPath = false)
        {
            TagCompound tag = GetTag(path, mod, fullPath);

            var structures = (List<TagCompound>)tag.GetList<TagCompound>("Structures");

            if (index >= structures.Count || index < 0)
            {
                dims = new Point16(0, 0);
                Egoteric.Instance.Logger.Warn($"Attempted to get dimensions of structure {index} in mutistructure containing {structures.Count - 1} structures.");
                return false;
            }

            TagCompound targetStructure = structures[index];

            dims = new Point16(targetStructure.GetInt("Width"), targetStructure.GetInt("Height"));
            return true;
        }

        internal static unsafe bool Generate(TagCompound tag, Point16 pos, bool ignoreNull = false)
        {
            List<TileSaveData> data = (List<TileSaveData>)tag.GetList<TileSaveData>("TileData");

            if (data is null)
            {
                Egoteric.Instance.Logger.Warn("Corrupt or Invalid structure data.");
                return false;
            }

            int width = tag.GetInt("Width");
            int height = tag.GetInt("Height");

            for (int x = 0; x <= width; x++)
            {
                for (int y = 0; y <= height; y++)
                {
                    bool isNullTile = false;
                    bool isNullWall = false;
                    int index = y + x * (height + 1);

                    TileSaveData d = data[index];
                    Tile tile = Framing.GetTileSafely(pos.X + x, pos.Y + y);

                    if (!int.TryParse(d.Tile, out int type))
                    {
                        string[] parts = d.Tile.Split();
                        if (parts[0] == "StructureHelper" && parts[1] == "NullBlock" && !ignoreNull || parts[0] == "Egoteric" && parts[1] == "NullBlock" && !ignoreNull) isNullTile = true;

                        else if (parts.Length > 1 && ModLoader.GetMod(parts[0]) != null && ModLoader.GetMod(parts[0]).TryFind<ModTile>(parts[1], out ModTile modTileType))
                            type = modTileType.Type;

                        else type = 0;
                    }

                    if (!int.TryParse(d.Wall, out int wallType))
                    {
                        string[] parts = d.Wall.Split();
                        if (parts[0] == "StructureHelper" && parts[1] == "NullWall" && !ignoreNull || parts[0] == "Egoteric" && parts[1] == "NullWall" && !ignoreNull) isNullWall = true;

                        else if (parts.Length > 1 && ModLoader.GetMod(parts[0]) != null && ModLoader.GetMod(parts[0]).TryFind<ModWall>(parts[1], out ModWall modWallType))
                            wallType = modWallType.Type;

                        else wallType = 0;
                    }

                    if (!d.Active) isNullTile = false;

                    if (!isNullTile || ignoreNull) //leave everything else about the tile alone if its a null block
                    {
                        tile.ClearEverything();
                        tile.TileType = (ushort)type;
                        tile.TileFrameX = d.FrameX;
                        tile.TileFrameY = d.FrameY;

                        fixed (void* ptr = &tile.Get<TileWallWireStateData>())
                        {
                            var intPtr = (int*)(ptr);
                            intPtr++;

                            *intPtr = d.WallWireData;
                        }

                        fixed (void* ptr = &tile.Get<LiquidData>())
                        {
                            var shortPtr = (short*)ptr;

                            *shortPtr = d.PackedLiquidData;
                        }

                        if (!d.Active) tile.HasTile = false;

                        if (d.TEType != "") //place and load a tile entity
                        {
                            if (d.TEType != "")
                            {
                                if (d.TEType == "StructureHelper ChestEntity" && !ignoreNull)
                                    GenerateChest(new Point16(pos.X + x, pos.Y + y), d.TEData);

                                else
                                {
                                    int typ;

                                    if (!int.TryParse(d.TEType, out typ))
                                    {
                                        string[] parts = d.TEType.Split();
                                        typ = ModLoader.GetMod(parts[0]).Find<ModTileEntity>(parts[1]).Type;
                                    }

                                    TileEntity.PlaceEntityNet(pos.X + x, pos.Y + y, typ);

                                    if (d.TEData != null && typ > 2)
                                        (TileEntity.ByPosition[new Point16(pos.X + x, pos.Y + y)] as ModTileEntity).LoadData(d.TEData);
                                }
                            }
                        }
                        else if ((type == TileID.Containers || TileID.Sets.BasicChest[tile.TileType]) && d.FrameX % 36 == 0 && d.FrameY % 36 == 0) //generate an empty chest if there is no chest data
                            Chest.CreateChest(pos.X + x, pos.Y + y);
                    }

                    if (!isNullWall || ignoreNull) //leave the wall alone if its a null wall
                        tile.WallType = (ushort)wallType;
                }
            }

            return true;
        }

        public static void GenerateChest(Point16 pos, TagCompound rules)
        {
            int i = Chest.CreateChest(pos.X, pos.Y);
            if (i == -1)
                return;

            Item item = new Item();
            item.SetDefaults(1);

            Chest chest = Main.chest[i];
            ChestEntity.SetChest(chest, ChestEntity.LoadChestRules(rules));
        }

        internal static bool LoadFile(string path, Mod mod, bool fullPath = false)
        {
            TagCompound tag;

            if (!fullPath)
            {
                var stream = mod.GetFileStream(path);
                tag = TagIO.FromStream(stream);
                stream.Close();
            }

            else tag = TagIO.FromFile(path);

            if (tag is null)
            {
                Egoteric.Instance.Logger.Warn("Structure was unable to be found. Are you passing the correct path?");
                return false;
            }

            StructureDataCache.Add(path, tag);
            return true;
        }

        internal static TagCompound GetTag(string path, Mod mod, bool fullPath = false)
        {
            TagCompound tag;

            if (!StructureDataCache.ContainsKey(path))
            {
                if (!LoadFile(path, mod, fullPath))
                    return null;

                tag = StructureDataCache[path];
            }

            else
                tag = StructureDataCache[path];

            return tag;
        }
    }
}