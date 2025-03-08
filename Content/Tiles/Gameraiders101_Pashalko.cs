using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiriusMod.Content.Items;
using SiriusMod.Helpers;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SiriusMod.Content.Tiles
{
	public class Gameraiders101_Pashalko: ModTile
	{

		public static bool ReadSign = false;
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = false;
			Main.tileLavaDeath[Type] = false;
			Main.tileWaterDeath[Type] = false;
			
			TileObjectData.newTile.Height = 11;
			TileObjectData.newTile.Width = 9;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16];
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.Origin = new Point16(2, 8);
			
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			
			TileObjectData.newTile.LavaDeath = false;

			TileObjectData.addTile(Type);
			AddMapEntry(new Color(123, 134, 145));
			
		}

		public override bool RightClick(int i, int j)
		{
			Tile tile = Main.tile[i, j];

			if (tile.TileFrameY >= 144 && tile.TileFrameX >= 32 && tile.TileFrameX <= 80 && ReadSign == false)
			{
				ReadSign = true;
				return true;
			}

			return false;
		}

		public override void MouseOverFar(int i, int j)
		{
			Tile tile = Main.tile[i, j];
			Player player = Main.LocalPlayer;
			if (tile.TileFrameY >= 144 && tile.TileFrameX >= 32 && tile.TileFrameX <= 80)
			{
				player.cursorItemIconEnabled = true;
				player.cursorItemIconID = ModContent.ItemType<LabTemplateSign>();

			}
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Utilities.SimpleGlowmask(i, j, spriteBatch, Texture);
		}
	}
}
