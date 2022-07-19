using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace Egoteric.Content.Currencies
{
    internal class VoidCurrency : CustomCurrencySingleCoin
    {
        public VoidCurrency(int coinItemID, long currencyCap, string CurrencyTextKey) : base(coinItemID, currencyCap)
        {
            this.CurrencyTextKey = CurrencyTextKey;
            CurrencyTextColor = Color.BlueViolet;
        }
    }
}
