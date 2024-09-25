using Microsoft.Xna.Framework;
using Terraria.GameContent.UI;

namespace Twig.Content.Currencies
{
	public class StarCryPrice : CustomCurrencySingleCoin
	{
		public StarCryPrice(int coinItemID, long currencyCap, string CurrencyTextKey) : base(coinItemID, currencyCap)
		{
			this.CurrencyTextKey = CurrencyTextKey;
			CurrencyTextColor = new Color(112, 214, 255);
		}
	}
	public class CosmicCryPrice : CustomCurrencySingleCoin
	{
		public CosmicCryPrice(int coinItemID, long currencyCap, string CurrencyTextKey) : base(coinItemID, currencyCap)
		{
			this.CurrencyTextKey = CurrencyTextKey;
			CurrencyTextColor = new Color(123,51,125);
		}
	}

}