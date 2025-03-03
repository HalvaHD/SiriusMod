using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiriusMod.Common.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SiriusMod.Content.Tiles.LaboratoryTiles
{
	public class YellowSmallCrate : ModTile
	{
		public int FrameCounter = 0;
		public override void SetStaticDefaults() {
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = false;
			Main.tileLavaDeath[Type] = false;
			Main.tileWaterDeath[Type] = false;
			Main.tileSolidTop[Type] = true;
			Main.tileTable[Type] = true;
			
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.CoordinateHeights = [16, 16, 16];
			TileObjectData.newTile.Origin = new Point16(1, 2);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			
			TileObjectData.newTile.HookPostPlaceMyPlayer =
				new PlacementHook(ModContent.GetInstance<YellowSmallCrateTileEntity>().Hook_AfterPlacement, -1, 0, true);
			
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide | AnchorType.Table, 2, 0);
			TileObjectData.addAlternate(0);
			
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide | AnchorType.Table, 2, 1);
			TileObjectData.addAlternate(0);
			
			TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
			TileObjectData.newAlternate.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide | AnchorType.Table, 2, 2);
			TileObjectData.addAlternate(0);
			
			
			TileObjectData.addTile(Type);
			AddMapEntry(new Color(88,94,107));
			
			AnimationFrameHeight = 54;
		}
		

		public override void AnimateTile(ref int frame, ref int frameCounter) {
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			ModContent.GetInstance<YellowSmallCrateTileEntity>().Kill(i, j);
		}

		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
		{
			Tile tile = Main.tile[i, j];
			YellowSmallCrateTileEntity tileEntity = TileUtils.FindTileEntity<YellowSmallCrateTileEntity>(i, j, 4, 3, 16);
			
		}


		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			YellowSmallCrateTileEntity tileEntity = TileUtils.FindTileEntity<YellowSmallCrateTileEntity>(i, j, 4, 3, 16);
			Tile tile = Main.tile[i, j];

			if (tileEntity != null && tileEntity.IsOpened != true)
			{
				Main.NewText("ОТКРЫТОЙ НАХУЙ");
				tileEntity.IsOpened = true;
				tileEntity.AnimationCounter = 240;
			}

			return true;
		}

		
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			int xFrameOffset = Main.tile[i, j].TileFrameX;
			int yFrameOffset = Main.tile[i, j].TileFrameY;
			Texture2D glowmask = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
			Vector2 drawOffest = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
			Vector2 drawPosition = new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) + drawOffest;
			Color drawColour = Color.White;
			Tile trackTile = Main.tile[i, j];
			if (!trackTile.IsHalfBlock && trackTile.Slope == 0)
				spriteBatch.Draw(glowmask, drawPosition, new Rectangle(xFrameOffset, yFrameOffset, 16, 16), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
			else if (trackTile.IsHalfBlock)
				spriteBatch.Draw(glowmask, drawPosition + new Vector2(0f, 8f), new Rectangle(xFrameOffset, yFrameOffset, 18, 8), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
		}
		
	}
	public class YellowSmallCrateTileEntity : ModTileEntity
	{
		public bool IsOpened = false;
		public int AnimationCounter = 0;
		public int FrameCounter = 0;
		public override bool IsTileValidForEntity(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile.HasTile && (int) tile.TileType == ModContent.TileType<YellowSmallCrate>() && tile.TileFrameX == (short) 0 && tile.TileFrameY == (short) 0;
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
			if (AnimationCounter > 0)
			{
				AnimationCounter--;
			}
			int i = Position.X;
			int j = Position.Y;
			Tile tile = Main.tile[i, j];
			if (tile.HasTile && IsOpened == true)
			{
				if (++FrameCounter % 10 == 0)
				{
					if (tile.TileFrameY != 54)
					{
						
						int topX = i - tile.TileFrameX % 74 / 16;
						int topY = j - tile.TileFrameY % 54 / 16;
            
						for (int x = topX; x < topX + 4; x++) {
							for (int y = topY; y < topY + 3; y++) {
								Main.tile[x, y].TileFrameY += 54;
								Main.tileSolidTop[Type] = true; 
            							
							}
						}
					}
					if (AnimationCounter == 0)
					{
						int topX = i - tile.TileFrameX % 74 / 16;
						int topY = j - tile.TileFrameY % 54 / 16;
            
						for (int x = topX; x < topX + 4; x++) {
							for (int y = topY; y < topY + 3; y++) {
								Main.tile[x, y].TileFrameY -= 54;
            							
							}
						}

						if (tile.TileFrameY == 0)
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
