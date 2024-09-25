using Terraria.ID;
using Terraria.ModLoader;
using Twig.Content.Tiles;

namespace Twig.Content.Items.Placeable
{
	public class StarOre : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 100;
			ItemID.Sets.SortingPriorityMaterials[Item.type] = 58;

			// This ore can spawn in slime bodies like other pre-boss ores. (copper, tin, iron, etch)
			// It will drop in amount from 3 to 13.
		}

		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(ModContent.TileType<StarRainOre>());
			Item.width = 12;
			Item.height = 12;
			Item.value = 1000;
		}
	}
}