using ProtoMod.Content.Tiles;
using ProtoMod.Content.Tiles.LaboratoryTiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Placeable
{
		public class TrashcanItem : ModItem
	{
		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.VoidMonolith);
			Item.createTile = ModContent.TileType<TechTrashTile>();
		}
	}
}