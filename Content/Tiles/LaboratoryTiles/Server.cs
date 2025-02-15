// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using SiriusMod.Content.Dusts;
// using Terraria;
// using Terraria.DataStructures;
// using Terraria.Enums;
// using Terraria.GameContent.Drawing;
// using Terraria.ModLoader;
// using Terraria.ObjectData;
//
// namespace SiriusMod.Content.Tiles.Lab
// {
// 	public class Server : ModTile
// 	{
// 		public override void SetStaticDefaults()
// 		{
// 			Main.tileLighted[Type] = true;
// 			Main.tileFrameImportant[Type] = true;
// 			Main.tileNoAttach[Type] = true;
// 			Main.tileLavaDeath[Type] = false;
// 			Main.tileWaterDeath[Type] = false;
// 			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
// 			TileObjectData.newTile.Height = 4;
// 			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16 };
// 			TileObjectData.newTile.LavaDeath = false;
// 			AnimationFrameHeight = 64;
//
// 			TileObjectData.addTile(Type);
// 			AddMapEntry(new Color(123, 134, 145));
// 			
// 			
// 			base.SetStaticDefaults();
// 		}
//
// 		public override void AnimateTile(ref int frame, ref int frameCounter)
// 		{
// 			// Cycle 5 frames of animation spending 8 ticks on each
// 			if (++frameCounter >= 30) {
// 				frameCounter = 0;
// 				if (++frame >= 2) {
// 					frame = 0;
// 				}
// 			}
// 		}
//
// 		// public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
// 		// {
// 		// 	int xFrameOffset = Main.tile[i, j].TileFrameX;
// 		// 	int yFrameOffset = Main.tile[i, j].TileFrameY;
// 		// 	Texture2D glowmask = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
// 		// 	Vector2 drawOffest = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
// 		// 	Vector2 drawPosition = new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) + drawOffest;
// 		// 	Color drawColour = Color.White;
// 		// 	Tile trackTile = Main.tile[i, j];
// 		// 	if (!trackTile.IsHalfBlock && trackTile.Slope == 0)
// 		// 		spriteBatch.Draw(glowmask, drawPosition, new Rectangle(xFrameOffset, yFrameOffset, 16, 16), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
// 		// 	else if (trackTile.IsHalfBlock)
// 		// 		spriteBatch.Draw(glowmask, drawPosition + new Vector2(0f, 8f), new Rectangle(xFrameOffset, yFrameOffset, 16, 8), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
// 		// }
// 	}
// }
