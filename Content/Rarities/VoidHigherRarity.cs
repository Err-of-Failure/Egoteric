using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Overthrown.Content.Rarities
{
    public class VoidHigherRarity : ModRarity
    {
        public override Color RarityColor => new Color(91, 0, (byte)(Main.DiscoB / 1.5f));

        public override int GetPrefixedRarity(int offset, float valueMulti)
        {
            if (offset < 0)
            {
                return ModContent.RarityType<VoidRarity>();
            }

            return Type;
        }
    }
}
