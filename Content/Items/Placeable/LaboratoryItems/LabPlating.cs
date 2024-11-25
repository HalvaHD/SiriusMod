using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Placeable.LaboratoryItems
{
	public class LabPlating : ModItem
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
			CreateRecipe()
				.AddIngredient<LabPlatingWall>(4)
				.AddTile(TileID.WorkBenches)
			.Register();

			CreateRecipe()
				.AddIngredient<LabPlatform>(2)
			.Register();
		}
	}
}