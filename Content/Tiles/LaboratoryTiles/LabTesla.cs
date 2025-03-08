using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiriusMod.Content.Dusts;
using SiriusMod.Helpers;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SiriusMod.Content.Tiles.LaboratoryTiles
{
	public class LabTesla : ModTile
	{
		public override void SetStaticDefaults()
		{

			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
			Main.tileWaterDeath[Type] = false;
			
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Width = 2;
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

		public override void NumDust(int i, int j, bool fail, ref int num)
			=> num = fail ? 1 : 3;

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Utilities.SimpleGlowmask(i, j, spriteBatch, Texture);
		}
	}
}
