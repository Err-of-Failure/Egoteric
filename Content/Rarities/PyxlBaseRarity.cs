using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Egoteric.Content.Rarities
{
    public class PyxlBaseRarity : ModRarity
    {
        public override Color RarityColor => new Color(91, 0, 140);

        public override int GetPrefixedRarity(int offset, float valueMulti)
        {
            if (offset > 0)
            {
                return ModContent.RarityType<PyxlHigherRarity>();
            }

            return Type;
        }
    }
}
