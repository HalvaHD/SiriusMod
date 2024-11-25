using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Placeable.LaboratoryItems
{
	public class LabPlatingMossy : ModItem
	{
		public override void SetStaticDefaults()
			=> Item.ResearchUnlockCount = 100;

		public override void SetDefaults()
		{
			Item.width = 12;
			Item.height = 12;
			Item.maxStack = Terraria.Item.CommonMaxStack;
			Item.useTurn = true;
			Item.autoReuse = true;
			Item.useAnimation = 15;
			Item.useTime = 10;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.consumable = true;
			Item.createTile = Mod.Find<ModTile>(GetType().Name).Type;
		}

		public override void AddRecipes()
		{
			CreateRecipe(10)
				//.AddIngredient<GlassblackMoss>() TODO
				.AddIngredient<LabPlating>(10)
				.AddTile(TileID.WorkBenches)
			.Register();

			CreateRecipe()
				.AddIngredient<LabPlatingWallMossy>(4)
				.AddTile(TileID.WorkBenches)
			.Register();
		}
	}
}