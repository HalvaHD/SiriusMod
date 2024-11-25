using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Common.Utilities;
using ProtoMod.Content.Items;
using ProtoMod.Content.NPC.Bosses.Protector;
using ProtoMod.Systems;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ProtoMod.Content.Tiles.LaboratoryTiles
{
	public class TechTrashTile : ModTile
	{
		public int FrameCounter = 0;
		public override void SetStaticDefaults() {
			Main.tileFrameImportant[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
			TileObjectData.newTile.UsesCustomCanPlace    = true;
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<TechTrashTileEntity>().Hook_AfterPlacement, -1, 0, true);
			


			
			
			
			
			
			
			
			TileObjectData.addTile(Type);
			
			AddMapEntry(new Color(88,94,107));
			
			AnimationFrameHeight = 63;
		}
		

		public override void AnimateTile(ref int frame, ref int frameCounter) {
		}

		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			ModContent.GetInstance<TechTrashTileEntity>().Kill(i, j);
		}

		public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
		{
			Tile tile = Main.tile[i, j];
			TechTrashTileEntity tileEntity = TileUtils.FindTileEntity<TechTrashTileEntity>(i, j, 3, 3, 16);
			
			
			
		}
		public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;


		public override bool RightClick(int i, int j)
		{
			Player player = Main.LocalPlayer;
			TechTrashTileEntity tileEntity = TileUtils.FindTileEntity<TechTrashTileEntity>(i, j, 3, 3, 16);
			Tile tile = Main.tile[i, j];

			if (tileEntity != null && tileEntity.IsOpened != true)
			{
				Main.NewText(Vector2.Distance(player.position, new Vector2(tileEntity.Position.X * 16, tileEntity.Position.Y * 16) ) / 16);
				tileEntity.IsOpened = true;
				tileEntity.AnimationCounter = 240;
				foreach (var npc in Main.ActiveNPCs)
				{
					if (npc.type == ModContent.NPCType<Protector>() && npc.active)
					{
						DialogueSystem.WriteDialogue("HALVADefault", "TrashCan0", Color.Aqua, 1, "TextSound");
					}
				}



			}

			return true;
		}

		

		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) {
			Tile tile = Main.tile[i, j];
			Texture2D texture = ModContent.Request<Texture2D>("ProtoMod/Content/Tiles/TechTrashTile").Value;
			Texture2D glowTexture = ModContent.Request<Texture2D>("ProtoMod/Content/Tiles/TechTrashTile_Glow").Value;

			// If you are using ModTile.SpecialDraw or PostDraw or PreDraw, use this snippet and add zero to all calls to spriteBatch.Draw
			// The reason for this is to accommodate the shift in drawing coordinates that occurs when using the different Lighting mode
			// Press Shift+F9 to change lighting modes quickly to verify your code works for all lighting modes
			Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);

			// Because height of third tile is different we change it
			int height = 27;

			// Offset along the Y axis depending on the current frame
			int frameYOffset = Main.tileFrame[Type] * AnimationFrameHeight;

			// Firstly we draw the original texture and then glow mask texture
			spriteBatch.Draw(
				texture,
				new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero,
				new Rectangle(tile.TileFrameX, tile.TileFrameY + frameYOffset, 16, height),
				Lighting.GetColor(i, j), 0f, default, 1f, SpriteEffects.None, 0f);
			// Make sure to draw with Color.White or at least a color that is fully opaque
			// Achieve opaqueness by increasing the alpha channel closer to 255. (lowering closer to 0 will achieve transparency)
			spriteBatch.Draw(
				glowTexture,
				new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero,
				new Rectangle(tile.TileFrameX, tile.TileFrameY + frameYOffset, 16, height),
				Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

			// Return false to stop vanilla draw
			return false;
		}
	}
	public class TechTrashTileEntity : ModTileEntity
	{
		public bool IsOpened = false;
		public int AnimationCounter = 0;
		public int FrameCounter = 0;
		public override bool IsTileValidForEntity(int x, int y)
		{
			Tile tile = Main.tile[x, y];
			return tile.HasTile && (int) tile.TileType == ModContent.TileType<TechTrashTile>() && tile.TileFrameX == (short) 0 && tile.TileFrameY == (short) 0;
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
					if (tile.TileFrameY != 126)
					{
						for (int k = 0; k < 2; k++)
						{
							int topX = i - tile.TileFrameX % 52 / 16;
							int topY = j - tile.TileFrameY % 63 / 16;
            
							short frameAdjustment = (short)(k * 63);
            
							for (int x = topX; x < topX + 3; x++) {
								for (int y = topY; y < topY + 3; y++) {
									Main.tile[x, y].TileFrameY += frameAdjustment;
            							
								}
							}
						}
					}

					if (AnimationCounter == 0)
					{
						for (int k = 0; k < 2; k++)
						{
							int topX = i - tile.TileFrameX % 52 / 16;
							int topY = j - tile.TileFrameY % 63 / 16;
						

							for (int x = topX; x < topX + 3; x++)
							{
								for (int y = topY; y < topY + 3; y++)
								{
									Main.tile[x, y].TileFrameY -= 63;

								}
							}
						
						}

						if (tile.TileFrameY == 0)
						{
							IsOpened = false;
							FrameCounter = 0;
							foreach (var npc in Main.ActiveNPCs)
							{
								if (npc.type == ModContent.NPCType<Protector>() && npc.active)
								{
									Terraria.Item.NewItem(new EntitySource_Misc("TrashLoot"), new Vector2(Position.X * 16 + 50, Position.Y * 16 + 50),
										ModContent.ItemType<YellowCrystalShard>());
								}
							}
						
						}
					
					}
				}
				
			}
			
		}

		// public override void Update() => this.UpdateTime();
	}
	
	
}
