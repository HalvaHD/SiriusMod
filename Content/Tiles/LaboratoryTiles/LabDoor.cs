using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Content.Dusts;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ProtoMod.Content.Tiles.LaboratoryTiles
{
	public class LabDoor : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileNoAttach[Type] = true;
			TileID.Sets.NotReallySolid[Type] = true;
			TileID.Sets.DrawsWalls[Type] = true;
			

			
			Main.tileLavaDeath[Type] = false;
			Main.tileWaterDeath[Type] = false;
			
			TileObjectData.newTile.Height = 17;
			TileObjectData.newTile.Width = 4;
			TileObjectData.newTile.CoordinatePadding = 2;
			TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 16, 16 , 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16];
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.UsesCustomCanPlace = true;
			
			
			TileObjectData.newTile.LavaDeath = false;
			
			TileObjectData.addTile(Type);
			
			AnimationFrameHeight = 306;
			
			AddMapEntry(new Color(123, 134, 145));
		}
		
		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) {
			Tile tile = Main.tile[i, j];
			Texture2D texture = ModContent.Request<Texture2D>("ProtoMod/Content/Tiles/LaboratoryTiles/LabDoor").Value;
			Texture2D glowTexture = ModContent.Request<Texture2D>("ProtoMod/Content/Tiles/LaboratoryTiles/LabDoor_Glow").Value;

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
				new Rectangle(tile.TileFrameX, tile.TileFrameY + frameYOffset, 16, 16),
				Lighting.GetColor(i, j), 0f, default, 1f, SpriteEffects.None, 0f);
			// Make sure to draw with Color.White or at least a color that is fully opaque
			// Achieve opaqueness by increasing the alpha channel closer to 255. (lowering closer to 0 will achieve transparency)
			spriteBatch.Draw(
				glowTexture,
				new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero,
				new Rectangle(tile.TileFrameX, tile.TileFrameY + frameYOffset, 16, 16),
				Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

			// Return false to stop vanilla draw
			return false;
		
		}
	}
}
