using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiriusMod.Helpers;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Enums;

namespace SiriusMod.Content.Tiles.LaboratoryTiles
{
	public class LabBookshelf : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = false;
			Main.tileLavaDeath[Type] = false;
			Main.tileWaterDeath[Type] = false;
			Main.tileSolidTop[Type] = true;
			Main.tileTable[Type] = true;
			
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16];
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.Origin = new Point16(1, 3);
			
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			
			TileObjectData.newTile.LavaDeath = false;

			TileObjectData.addTile(Type);
			AddMapEntry(new Color(123, 134, 145));
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Utilities.SimpleGlowmask(i, j, spriteBatch, Texture);
		}
	}
}
