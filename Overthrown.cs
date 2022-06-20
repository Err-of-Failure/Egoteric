using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace Overthrown
{
	public class Overthrown : Mod
	{
		public const string ASSET_PATH = "Overthrown/Assets";

		public static int VoidCurrencyId;

		public override void Load()
		{
			VoidCurrencyId = CustomCurrencyManager.RegisterCurrency(new Content.Currencies.VoidCurrency(ModContent.ItemType<Content.Items.VoidCrystal>(), 999L, "Mods.Overthrown.Content.Currencies.VoidCurrency"));
		}

        public override void Unload()
		{ 
        }
    }
}