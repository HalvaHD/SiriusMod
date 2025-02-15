using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SiriusMod.Content.Tiles.LaboratoryTiles
{
    public class LabScreensBig : ModTile
    {
        public override string Texture => "SiriusMod/Content/Tiles/LaboratoryTiles/LabScreenAtlas";
        
        public override void SetStaticDefaults()
        {
	        Main.tileFrameImportant[Type] = true;
	        Main.tileNoAttach[Type] = true;
	        Main.tileLavaDeath[Type] = true;
	        Main.tileWaterDeath[Type] = false;

	        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
	        TileObjectData.newTile.Height = 5;
	        TileObjectData.newTile.Width = 9;
	        TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16, 16 };
	        TileObjectData.newTile.CoordinateWidth = 16;
	        TileObjectData.newTile.CoordinatePadding = 2;
	        TileObjectData.newTile.Origin = new Point16(1, 4);

	        TileObjectData.newTile.StyleWrapLimit = 2;
	        TileObjectData.newTile.StyleHorizontal = true;
	        TileObjectData.newTile.LavaDeath = true;
	        
	        TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
	        TileObjectData.newSubTile.LinkedAlternates = true;
	        TileObjectData.addSubTile(1);
	        
	        TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
	        TileObjectData.newSubTile.LinkedAlternates = true;
	        TileObjectData.addSubTile(2);
	        
	        TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
	        TileObjectData.newSubTile.LinkedAlternates = true;
	        TileObjectData.addSubTile(3);
	        
	        TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
	        TileObjectData.newSubTile.LinkedAlternates = true;
	        TileObjectData.addSubTile(4);
	        
	        TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
	        TileObjectData.newSubTile.LinkedAlternates = true;
	        TileObjectData.addSubTile(5);
			
	        TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
	        TileObjectData.newSubTile.LinkedAlternates = true;
	        TileObjectData.addSubTile(6);
	        
	        TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
	        TileObjectData.newSubTile.LinkedAlternates = true;
	        TileObjectData.addSubTile(7);
			
	        TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
	        TileObjectData.newSubTile.LinkedAlternates = true;
	        TileObjectData.addSubTile(8);
	        
	        TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
	        TileObjectData.newSubTile.LinkedAlternates = true;
	        TileObjectData.addSubTile(9);
	        
	        TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
	        TileObjectData.newSubTile.LinkedAlternates = true;
	        TileObjectData.newSubTile.Width = 11;
	        TileObjectData.addSubTile(10);


	        TileObjectData.addTile(Type);
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
				spriteBatch.Draw(glowmask, drawPosition + new Vector2(0f, 8f), new Rectangle(xFrameOffset, yFrameOffset, 16, 8), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
		}
    }
}
