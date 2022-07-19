using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Egoteric.Content.Rarities
{
    public class VidiaBaseRarity : ModRarity
    {
        public override Color RarityColor => new Color(190, 0, 0);

        public override int GetPrefixedRarity(int offset, float valueMulti)
        {
            if (offset > 0)
            {
                return ModContent.RarityType<VidiaHigherRarity>();
            }

            return Type;
        }
    }
}
