using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace Overthrown
{
	public class Overthrown : Mod
	{
		public const string ASSET_PATH = "Overthrown/Assets";
		public const string STRUCTURE_PATH = "Overthrown/World/Structures";

		public static int VoidCurrencyId;

		public Overthrown() { Instance = this; }

		public static Overthrown Instance { get; set; }

		public override void Load()
		{
			VoidCurrencyId = CustomCurrencyManager.RegisterCurrency(new Content.Currencies.VoidCurrency(ModContent.ItemType<Content.Items.VoidCrystal>(), 999L, "Mods.Overthrown.Currencies.VoidCurrency"));
		}

        public override void Unload()
		{ 
        }
    }
}