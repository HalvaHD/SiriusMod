using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiriusMod.Common.Utilities;
using SiriusMod.Helpers;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SiriusMod.Content.Tiles.LaboratoryTiles
{
	public class YellowCrate : ModTile
	{
		public int FrameCounter = 0;
		public override void SetStaticDefaults() {
			Main.tileFrameImportant[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileTable[Type] = true; 
			
			TileObjectData.newTile.Height = 6;
			TileObjectData.newTile.Width = 7;
			TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 16, 16];
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.Origin = new Point16(1, 5);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide | AnchorType.Table, 3, 0);

			
			TileObjectData.newTile.HookPostPlaceMyPlayer =
				new PlacementHook(ModContent.GetInstance<YellowCrateTileEntity>().Hook_AfterPlacement, -1, 0, true); 

			
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide | AnchorType.Table, 3, 1);
			TileObjectData.addAlternate(0);
			
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide | AnchorType.Table, 3, 2);
			TileObjectData.addAlternate(0);
			
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide | AnchorType.Table, 3, 3);
			TileObjectData.addAlternate(0);
			
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide | AnchorType.Table, 3, 4);
			TileObjectData.addAlternate(0);
			
			TileObjectData.addTile(Type);
			
			AddMapEntry(new Color(88,94,107));
			
			AnimationFrameHeight = 108;
		}
		

		public override void AnimateTile(ref int frame, ref int frameCounter) {
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			ModContent.GetInstance<YellowCrateTileEntity>().Kill(i, j);
		}

		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
		{
			Tile tile = Main.tile[i, j];
			YellowCrateTileEntity tileEntity = TileUtils.FindTileEntity<YellowCrateTileEntity>(i, j, 7, 6, 16);
			
		}

		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			YellowCrateTileEntity tileEntity = TileUtils.FindTileEntity<YellowCrateTileEntity>(i, j, 7, 6, 16);
			Tile tile = Main.tile[i, j];
			
			if (tileEntity != null && tileEntity.IsOpened != true)
			{
				Main.NewText("ОТКРЫТО");
				tileEntity.IsOpened = true;
				tileEntity.AnimationCounter = 240;
			}
			return true;
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Utilities.SimpleGlowmask(i, j, spriteBatch, Texture);
		}
	}
	public class YellowCrateTileEntity : ModTileEntity
	{
		public bool IsOpened = false;
		public int AnimationCounter = 0;
		public int FrameCounter = 0;
		public override bool IsTileValidForEntity(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile.HasTile && (int) tile.TileType == ModContent.TileType<YellowCrate>() && tile.TileFrameX == (short) 0 && tile.TileFrameY == (short) 0;
		}
		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				return Place(i, j);
			}
			return -1;
		}

		public override void Update()
		{
			int i = Position.X;
			int j = Position.Y;
			Tile tile = Main.tile[i, j];
			
			if (AnimationCounter > 0)
			{
				AnimationCounter--;
				
			}

			if (tile.HasTile && IsOpened == true)
			{
				if (++FrameCounter % 10 == 0)
				{
					if (tile.TileFrameX != 126)
					{
						int topX = i - tile.TileFrameX % 126 / 16;
						int topY = j - tile.TileFrameY % 108 / 16;
						for (int x = topX; x < topX + 7; x++) {
							for (int y = topY; y < topY + 6; y++) {
								Main.tile[x, y].TileFrameX += 126;
															
							}
						}
						
					}
					
					if (AnimationCounter == 0)
					{
						int topX = i - tile.TileFrameX % 126 / 16;
						int topY = j - tile.TileFrameY % 108 / 16;
            
						for (int x = topX; x < topX + 7; x++) {
							for (int y = topY; y < topY + 6; y++) {
								Main.tile[x, y].TileFrameX -= 126;
            							
							}
						}

						if (tile.TileFrameX == 0)
						{
							IsOpened = false;
							FrameCounter = 0;
						}
						
					}
				}
				
			}
			
		}
	}
	
	
}
