﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader.IO;

namespace Egoteric.Common.World.ChestHelper
{
    //This is all copied from Structure Helper sadly, but it only because we didn't want to have a need to install Structure Helper just to use our mod.
    class ChestRulePool : ChestRule
    {
        /// <summary>
        /// How many items from the pool, picked at random, should be placed in the chest.
        /// </summary>
        public int itemsToGenerate;

        public override bool UsesWeight => true;

        public override string Name => "Pool Rule";

        public override string Tooltip => "Generates a configurable amount of items \nrandomly selected from the rule.\nCan make use of weight.";

        public override void PlaceItems(Chest chest, ref int nextIndex)
        {
            if (nextIndex >= 40) return;

            List<Loot> toLoot = pool;

            for (int k = 0; k < itemsToGenerate; k++)
            {
                if (nextIndex >= 40) return;

                int maxWeight = 1;

                foreach (Loot loot in toLoot)
                    maxWeight += loot.weight;

                int selection = Main.rand.Next(maxWeight);
                int weightTotal = 0;
                Loot selectedLoot = null;

                for (int i = 0; i < toLoot.Count; i++)
                {
                    weightTotal += toLoot[i].weight;

                    if (selection < weightTotal + 1)
                    {
                        selectedLoot = toLoot[i];
                        toLoot.Remove(selectedLoot);
                        break;
                    }
                }

                chest.item[nextIndex] = selectedLoot?.GetLoot();
                nextIndex++;
            }
        }

        public override TagCompound Serizlize()
        {
            var tag = new TagCompound()
            {
                {"Type", "Pool"},
                {"ToGenerate", itemsToGenerate},
                {"Pool", SerializePool()}
            };

            return tag;
        }

        public static ChestRule Deserialize(TagCompound tag)
        {
            var rule = new ChestRulePool();
            rule.itemsToGenerate = tag.GetInt("ToGenerate");
            rule.pool = DeserializePool(tag.GetCompound("Pool"));

            return rule;
        }

        public override ChestRule Clone()
        {
            var clone = new ChestRulePool();

            for (int k = 0; k < pool.Count; k++)
                clone.pool.Add(pool[k].Clone());

            clone.itemsToGenerate = itemsToGenerate;

            return clone;
        }
    }
}