using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SiriusMod.Content.Tiles.LaboratoryTiles
{
	public class LabBigDoorClosed : ModTile
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
			TileObjectData.newTile.Origin = new Point16(1, 16);
			
			TileObjectData.newTile.LavaDeath = false;
			
			TileObjectData.addTile(Type);
			
			AnimationFrameHeight = 306;
			
			AddMapEntry(new Color(123, 134, 145));
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
}
