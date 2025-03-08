using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiriusMod.Helpers;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SiriusMod.Content.Tiles.LaboratoryTiles
{
    public class LabBigScreens : ModTile
    {
        public override void SetStaticDefaults()
        {
	        Main.tileFrameImportant[Type] = true;
	        Main.tileNoAttach[Type] = true;
	        Main.tileLavaDeath[Type] = true;
	        Main.tileWaterDeath[Type] = false;
	        
	        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
	        TileObjectData.newTile.Height = 5;
	        TileObjectData.newTile.Width = 9;
	        TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 16];
	        TileObjectData.newTile.CoordinateWidth = 16;
	        TileObjectData.newTile.CoordinatePadding = 2;
	        TileObjectData.newTile.Origin = new Point16(1, 4);
	        TileObjectData.newTile.UsesCustomCanPlace = true;
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
	        Utilities.SimpleGlowmask(i, j, spriteBatch, Texture);
        }
    }
}
