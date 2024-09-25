using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Twig.Common.Players;
using Twig.Content.Items.Placeable;

namespace Twig.Content.Items.Accessories
{
	public class RainyBracelet : ModItem, ILocalizedModType
	{
		public new string LocalizationCategory => "Items.Accessories";
		// By declaring these here, changing the values will alter the effect, and the tooltip

		// Insert the modifier values into the tooltip localization. More info on this approach can be found on the wiki: https://github.com/tModLoader/tModLoader/wiki/Localization#binding-values-to-localizations
		public override void SetDefaults() {
			Item.width = 32;
			Item.height = 32;
			Item.accessory = true;
			Item.rare = ItemRarityID.Green;
		}

		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.GetModPlayer<RainyBraceletPlayer>().RainyBracelet = true;
			player.moveSpeed += 0.1f;
			TwigModPlayer.RainyB = true;
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient<StarBar>(12)
				.AddIngredient(ItemID.AnkletoftheWind)
				.AddTile(TileID.Anvils)
				.Register();
		}
	}
	

	// Some movement effects are not suitable to be modified in ModItem.UpdateAccessory due to how the math is done.
	// ModPlayer.PostUpdateRunSpeeds is suitable for these modifications.
	public class RainyBraceletPlayer : ModPlayer {
		public bool RainyBracelet = false;

		public override void ResetEffects() {
			RainyBracelet = false;
		}
	}
}