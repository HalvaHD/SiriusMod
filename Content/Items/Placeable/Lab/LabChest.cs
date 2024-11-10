using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Placeable.Lab
{
	public class LabChest : ModItem
	{
		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 22;
			Item.maxStack = 9999;
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
				.AddIngredient<LabPlating>(8)
				.AddRecipeGroup("IronBar", 2)
				.AddTile(TileID.Anvils)
			.Register();
		}
	}
}