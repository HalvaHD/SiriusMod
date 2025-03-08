using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiriusMod.Common.Utilities;
using SiriusMod.Helpers;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SiriusMod.Content.Tiles.LaboratoryTiles
{
	public class LabBigDoorHorizontal : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileID.Sets.DrawsWalls[Type] = true;
			
			Main.tileLavaDeath[Type] = false;
			Main.tileWaterDeath[Type] = false;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<LabBigDoorHorizontalTileEntity>().Hook_AfterPlacement, -1, 0, true);
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Width = 17;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16];
			TileObjectData.newTile.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide | AnchorType.Table, 2, 0);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.EmptyTile, TileObjectData.newTile.Width, 0);

			// TileObjectData.newTile.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide | AnchorType.Table, 3, 0);

			
			TileObjectData.newTile.LavaDeath = false;
			
			TileObjectData.addTile(Type);
			
			AnimationFrameHeight = TileObjectData.newTile.CoordinateFullHeight;
			
			AddMapEntry(new Color(123, 134, 145));
		}
		public override void AnimateTile(ref int frame, ref int frameCounter) {
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			ModContent.GetInstance<LabBigDoorHorizontalTileEntity>().Kill(i, j);
		}

		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
		{
			Tile tile = Main.tile[i, j];
			LabBigDoorHorizontalTileEntity tileEntity = TileUtils.FindTileEntity<LabBigDoorHorizontalTileEntity>(i, j, 17, 4, 16);
			if (tileEntity != null && tileEntity.AnimationCounter > 120)
			{
				tile.IsActuated = true;
			}
			else
			{
				tile.IsActuated = false;
			}
			
		}
		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;


		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			LabBigDoorHorizontalTileEntity tileEntity = TileUtils.FindTileEntity<LabBigDoorHorizontalTileEntity>(i, j, 17, 4, 16);
			
			Tile tile = Main.tile[i, j];

			if (tileEntity != null && tileEntity.IsOpened != true)
			{
				Main.NewText("ОТКРЫТО");
				tileEntity.IsOpened = true;
				tileEntity.AnimationCounter = 300;
			}

			return true;
		}
		
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Utilities.SimpleGlowmask(i, j, spriteBatch, Texture);
		}
	}
	
	public class LabBigDoorHorizontalTileEntity : ModTileEntity
	{
		public bool IsOpened = false;
		public int AnimationCounter = 0;
		public int FrameCounter = 0;
		public bool FullyOpened = false;

		public override bool IsTileValidForEntity(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile.HasTile && (int) tile.TileType == ModContent.TileType<LabBigDoorHorizontal>() && tile.TileFrameX == (short) 0 && tile.TileFrameY == (short) 0;
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
			if (AnimationCounter <= 0 && tile.TileFrameX != 0)
			{	
				FullyOpened = true;
			}
			if (tile.HasTile && IsOpened == true)
			{
				FrameCounter++;
				if (FrameCounter % 6 == 0)
				{
					if (tile.TileFrameY != 650 && FullyOpened == false)
					{
						for (int k = 0; k < 1; k++)
						{
							int topX = i - tile.TileFrameX % 306 / 16;
							int topY = j - tile.TileFrameY % 66/ 16;
							
							for (int x = topX; x < topX + 4; x++) {
								for (int y = topY; y < topY + 17; y++) {
									Main.tile[x, y].TileFrameY += 66;
            					
								}
							}
						
						}
					}

					if (tile.TileFrameY >= 650)
					{
						Main.tileSolid[Main.tile[i,j].TileType] = false;

					}
					
					if (FrameCounter % 6 == 0 && FullyOpened == true) {
						if (tile.TileFrameY != 0)
						{
							for (int k = 0; k < 1; k++)
							{
								int topX = i - tile.TileFrameX % 306 / 16;
								int topY = j - tile.TileFrameY % 66/ 16;

								for (int x = topX; x < topX + 4; x++)
								{
									for (int y = topY; y < topY + 17; y++)
									{
										Main.tile[x, y].TileFrameY -= 66;
									}
								}

							}
						}

						Main.tileSolid[Main.tile[i,j].TileType] = true;
						if (tile.TileFrameY == 0)
						{
							IsOpened = false;
							FrameCounter = 0;
							FullyOpened = false;
						}
					}
					
				}
			}
		}
		
	}
}
