using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiriusMod.Helpers;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SiriusMod.Content.Tiles.LaboratoryTiles
{
	public class LabBigTableTwo : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileTable[Type] = true;
			Main.tileLavaDeath[Type] = false;
			Main.tileWaterDeath[Type] = false;
			Main.tileSolidTop[Type] = true;
			Main.tileTable[Type] = true;
			
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
			
			AdjTiles = new int[] { TileID.Tables };


			AddMapEntry(new Color(200, 200, 200), Language.GetText("MapObject.Table"));
			
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinateHeights = [16, 16, 16];
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.Origin = new Point16(1, 2);
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
