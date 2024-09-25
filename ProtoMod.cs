using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;
using Twig.Common.Players;
using Twig.Content.Currencies;
using Twig.Content.Items;
using Twig.Content.Projectiles;

namespace Twig
{
	public class Twig : Mod
	{
		// public static int StarCryCurrencyID;
		// public static int StarCryCurrencyID2;
		// public static int StarCryCurrencyID5;
		public static int CosmicCryCurrencyID;
		public static int CosmicCryCurrencyID2;
		public static int CosmicCryCurrencyID5;
		public static List<int> CheckKilledBosses = new ();

		public override void Load()
		{
			ProjectileLoader.GetProjectile(ModContent.ProjectileType<TeleporterInstance>());
			// Костыльно решение, потому что магазины не предполаагют использование склонений в валютах
			// 2 - от двух до пяти единиц стоимости. 5 - от пяти и выше
			// StarCryCurrencyID = CustomCurrencyManager.RegisterCurrency(new StarCryPrice(ModContent.ItemType<StarCry>(), 9999, this.GetLocalization("Currency.StarCry").ToString()));
			// StarCryCurrencyID2 = CustomCurrencyManager.RegisterCurrency(new StarCryPrice(ModContent.ItemType<StarCry>(), 9999, this.GetLocalization("Currency.StarCry2").ToString()));
			// StarCryCurrencyID5 = CustomCurrencyManager.RegisterCurrency(new StarCryPrice(ModContent.ItemType<StarCry>(), 9999, this.GetLocalization("Currency.StarCry5").ToString()));
			CosmicCryCurrencyID = CustomCurrencyManager.RegisterCurrency(new CosmicCryPrice(
				ModContent.ItemType<CosmicCry>(),
				9999, this.GetLocalization("Currency.CosmicCry").ToString()));
			CosmicCryCurrencyID2 = CustomCurrencyManager.RegisterCurrency(new CosmicCryPrice(
				ModContent.ItemType<CosmicCry>(),
				9999, this.GetLocalization("Currency.CosmicCry2").ToString()));
			CosmicCryCurrencyID5 = CustomCurrencyManager.RegisterCurrency(new CosmicCryPrice(
				ModContent.ItemType<CosmicCry>(),
				9999, this.GetLocalization("Currency.CosmicCry5").ToString()));
			;
			
			
		}

		public override void PostSetupContent()
		{
			base.PostSetupContent();
		}

		public override void Unload()
		{
			base.Unload();
		}
	}
}