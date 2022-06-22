using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Overthrown.Content.Rarities
{
    public class VidiaHigherRarity : ModRarity
    {
        public override Color RarityColor => new Color((byte)(Main.DiscoR), 0, 0);

        public override int GetPrefixedRarity(int offset, float valueMulti)
        {
            if (offset < 0)
            {
                return ModContent.RarityType<VidiaBaseRarity>();
            }

            return Type;
        }
    }
}
