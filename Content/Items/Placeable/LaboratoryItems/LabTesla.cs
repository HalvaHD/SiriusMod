using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items.Placeable.LaboratoryItems
{
    public class LabTesla : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 64;
            Item.maxStack = Item.CommonMaxStack;
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
                .AddRecipeGroup("IronBar", 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}