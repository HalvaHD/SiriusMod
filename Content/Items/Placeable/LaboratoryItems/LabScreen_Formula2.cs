using ProtoMod.Content.Tiles.LaboratoryTiles;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace ProtoMod.Content.Items.Placeable.LaboratoryItems
{
    public class LabScreen_Formula2 : ModItem
    {
        public override void SetStaticDefaults()
            => Item.ResearchUnlockCount = 100;

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<LabScreenAtlas>(), 6);
            Item.width = 62;
            Item.height = 40;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
        }

        public override void AddRecipes()
        {
        }
    }
}