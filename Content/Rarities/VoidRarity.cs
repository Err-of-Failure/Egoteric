using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Overthrown.Content.Rarities
{
    public class VoidRarity : ModRarity
    {
        public override Color RarityColor => new Color(91, 0, 140);

        public override int GetPrefixedRarity(int offset, float valueMulti)
        {
            if (offset > 0)
            {
                return ModContent.RarityType<VoidHigherRarity>();
            }

            return Type;
        }
    }
}
