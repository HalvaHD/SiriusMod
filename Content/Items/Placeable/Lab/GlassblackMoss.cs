using ProtoMod.Content.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Placeable.Lab
{
    public class GlassblackMoss : ModItem
    {
        public override void SetStaticDefaults() {
            Item.ResearchUnlockCount = 100;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;
        }

        public override void SetDefaults() {
            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.Lab.GlassblackMoss>());
            Item.width = 8;
            Item.height = 8;
            Item.value = 10;
        }
    }
}