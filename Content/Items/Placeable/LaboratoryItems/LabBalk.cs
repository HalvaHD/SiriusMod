using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Placeable.LaboratoryItems
{
    public class LabBalk : ModItem
    {
        public override void SetStaticDefaults() {
            Item.ResearchUnlockCount = 100;
            ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;
        }

        public override void SetDefaults() {
            Item.DefaultToPlaceableTile(ModContent.WallType<Tiles.LaboratoryTiles.LabBalk>());
            Item.maxStack = 9999;
            Item.width = 8;
            Item.height = 8;
            Item.value = 10;
        }
    }
}