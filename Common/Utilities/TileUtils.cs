using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SiriusMod.Common.Utilities
{
	public static class TileUtils
	{
		/// <summary>
		/// Atttempts to find the top-left corner of a multitile at location (<paramref name="x"/>, <paramref name="y"/>)
		/// </summary>
		/// <param name="x">The tile X-coordinate</param>
		/// <param name="y">The tile Y-coordinate</param>
		/// <returns>The tile location of the multitile's top-left corner, or the input location if no tile is present or the tile is not part of a multitile</returns>
		public static Point16 GetTopLeftTileInMultitile(int x, int y)
		{
			Tile tile = Main.tile[x, y];

			int frameX = 0;
			int frameY = 0;

			if (tile.HasTile)
			{
				int style = 0, alt = 0;
				TileObjectData.GetTileInfo(tile, ref style, ref alt);
				TileObjectData data = TileObjectData.GetTileData(tile.TileType, style, alt);

				if (data != null)
				{
					int size = 16 + data.CoordinatePadding;

					frameX = tile.TileFrameX % (size * data.Width) / size;
					frameY = tile.TileFrameY % (size * data.Height) / size;
				}
			}

			return new Point16(x - frameX, y - frameY);
		}

		/// <summary>
		/// Uses <seealso cref="GetTopLeftTileInMultitile(int, int)"/> to try to get the entity bound to the multitile at (<paramref name="i"/>, <paramref name="j"/>).
		/// </summary>
		/// <typeparam name="T">The type to get the entity as</typeparam>
		/// <param name="i">The tile X-coordinate</param>
		/// <param name="j">The tile Y-coordinate</param>
		/// <param name="entity">The found <typeparamref name="T"/> instance, if there was one.</param>
		/// <returns><see langword="true"/> if there was a <typeparamref name="T"/> instance, or <see langword="false"/> if there was no entity present OR the entity was not a <typeparamref name="T"/> instance.</returns>
		public static T FindTileEntity<T>(int i, int j, int width, int height, int sheetSquare = 16) where T : ModTileEntity
		{
			Tile tile = Main.tile[i, j];
			int X = i - (int) tile.TileFrameX % (width * sheetSquare) / sheetSquare;
			int Y = j - (int) tile.TileFrameY % (height * sheetSquare) / sheetSquare;
			int type = ModContent.GetInstance<T>().Type;
			TileEntity tileEntity;
			return !TileEntity.ByPosition.TryGetValue(new Point16(X, Y), out tileEntity) || (int) tileEntity.type != type ? default (T) : (T) tileEntity;
		}
	}
}