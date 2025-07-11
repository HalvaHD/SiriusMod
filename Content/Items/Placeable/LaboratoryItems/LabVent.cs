using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items.Placeable.LaboratoryItems
{
    public class LabVent : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.LaboratoryTiles.LabVents>(), 0);
            Item.width = 32;
            Item.height = 32;
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