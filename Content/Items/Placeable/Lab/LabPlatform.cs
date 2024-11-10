using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Placeable.Lab
{
	public class LabPlatform : ModItem
	{
		public override void SetStaticDefaults()
			=> Item.ResearchUnlockCount = 200;

		public override void SetDefaults()
		{
			Item.width = 8;
			Item.height = 10;
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
			CreateRecipe(2)
				.AddIngredient<LabPlating>()
			.Register();
		}
	}
}