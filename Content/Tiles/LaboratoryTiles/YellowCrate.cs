using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Common.Utilities;
using ProtoMod.Content.Items;
using ProtoMod.Content.NPC.Bosses.Protector;
using ProtoMod.Systems;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ProtoMod.Content.Tiles.LaboratoryTiles
{
	public class YellowCrate : ModTile
	{
		public int FrameCounter = 0;
		public override void SetStaticDefaults() {
			Main.tileFrameImportant[Type] = true;
			Main.tileSolidTop[Type] = true;
			Main.tileTable[Type] = true; 
			
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			
			TileObjectData.newTile.Height = 6;
			TileObjectData.newTile.Width = 7;
			TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 16, 16];
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.UsesCustomCanPlace = true;
			TileObjectData.newTile.HookPostPlaceMyPlayer =
				new PlacementHook(ModContent.GetInstance<YellowCrateTileEntity>().Hook_AfterPlacement, -1, 0, true); 
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
				Main.NewText("ОТКРЫТОЙ НАХУЙ");
				tileEntity.IsOpened = true;
				tileEntity.AnimationCounter = 240;
			}

			return true;
		}

		

		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) {
			Tile tile = Main.tile[i, j];
			Texture2D texture = ModContent.Request<Texture2D>("ProtoMod/Content/Tiles/LaboratoryTiles/YellowCrate").Value;
			Texture2D glowTexture = ModContent.Request<Texture2D>("ProtoMod/Content/Tiles/LaboratoryTiles/YellowCrate_Glow").Value;

			// If you are using ModTile.SpecialDraw or PostDraw or PreDraw, use this snippet and add zero to all calls to spriteBatch.Draw
			// The reason for this is to accommodate the shift in drawing coordinates that occurs when using the different Lighting mode
			// Press Shift+F9 to change lighting modes quickly to verify your code works for all lighting modes
			Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);

			// Offset along the Y axis depending on the current frame
			int frameYOffset = Main.tileFrame[Type] * AnimationFrameHeight;

			// Firstly we draw the original texture and then glow mask texture
			spriteBatch.Draw(
				texture,
				new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero,
				new Rectangle(tile.TileFrameX, tile.TileFrameY + frameYOffset, 16, 16),
				Lighting.GetColor(i, j), 0f, default, 1f, SpriteEffects.None, 0f);
			spriteBatch.Draw(
				glowTexture,
				new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero,
				new Rectangle(tile.TileFrameX, tile.TileFrameY + frameYOffset, 16, 16),
				Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			
			return false;
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
			if (AnimationCounter > 0)
			{
				AnimationCounter--;
			}
			int i = Position.X;
			int j = Position.Y;
			Tile tile = Main.tile[i, j];
			// Main.tileSolid[tile.TileType] = true;

			if (tile.HasTile && IsOpened == true)
			{
				if (++FrameCounter % 10 == 0)
				{
					if (tile.TileFrameY != 108)
					{
						int topX = i - tile.TileFrameX % 126 / 16;
						int topY = j - tile.TileFrameY % 108 / 16;
            
						for (int x = topX; x < topX + 7; x++) {
							for (int y = topY; y < topY + 6; y++) {
								Main.tile[x, y].TileFrameY += 108;
								
							}
						}
						
					}
					if (AnimationCounter == 0)
					{
						int topX = i - tile.TileFrameX % 126 / 16;
						int topY = j - tile.TileFrameY % 108 / 16;
            
						for (int x = topX; x < topX + 7; x++) {
							for (int y = topY; y < topY + 6; y++) {
								Main.tile[x, y].TileFrameY -= 108;
            							
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
