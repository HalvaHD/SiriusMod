using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Placeable.LaboratoryItems
{
    public class GlassblackMossWall : ModItem
    {
        public override void SetStaticDefaults()
            => Item.ResearchUnlockCount = 400;

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.maxStack = Terraria.Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createWall = Mod.Find<ModWall>(GetType().Name).Type;
        }

        public override void AddRecipes()
        {
            CreateRecipe(4)
                .AddIngredient<GlassblackMoss>()
                .AddTile(TileID.WorkBenches)
            .Register();
        }
    }
}