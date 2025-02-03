using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace ProtoMod.Content.Items.Placeable.LaboratoryItems
{
    public class LabTVBroken_Right : ModItem
    {
        public override void SetStaticDefaults()
            => Item.ResearchUnlockCount = 100;

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.LaboratoryTiles.LabTVBroken>(), 2);
            Item.width = 30;
            Item.height = 22;
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