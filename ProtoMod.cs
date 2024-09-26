using System.Collections.Generic;
using ProtoMod.Content.Currencies;
using ProtoMod.Content.Items;
using ProtoMod.Content.Projectiles;
using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace ProtoMod
{
	public class ProtoMod : Mod
	{
		public static int CosmicCryCurrencyID;
		public static int CosmicCryCurrencyID2;
		public static int CosmicCryCurrencyID5;
		public static List<int> CheckKilledBosses = new ();

		public override void Load()
		{
			ProjectileLoader.GetProjectile(ModContent.ProjectileType<TeleporterInstance>());
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