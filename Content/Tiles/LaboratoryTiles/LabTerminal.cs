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
    public class LabTerminal : ModTile
    {
	    public override void SetStaticDefaults()
	    {
		    Main.tileFrameImportant[Type] = true;
		    Main.tileNoAttach[Type] = true;
		    Main.tileLavaDeath[Type] = true;
		    Main.tileWaterDeath[Type] = false;

		    TileObjectData.newTile.Height = 3;
		    TileObjectData.newTile.Width = 5;
		    TileObjectData.newTile.CoordinateHeights = [16, 16, 16];
		    TileObjectData.newTile.CoordinateWidth = 16;
		    TileObjectData.newTile.CoordinatePadding = 2;
		    TileObjectData.newTile.AnchorBottom = new AnchorData(
			    AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		    TileObjectData.newTile.Origin = new Point16(1, 2);
		    TileObjectData.newTile.UsesCustomCanPlace = true;

		    TileObjectData.newTile.StyleWrapLimit = 2;
		    TileObjectData.newTile.StyleHorizontal = true;
		    TileObjectData.newTile.LavaDeath = true;
		    TileObjectData.newTile.Direction = TileObjectDirection.PlaceRight;

		    TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		    TileObjectData.newTile.AnchorBottom = new AnchorData(
			    AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		    TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceLeft;
		    TileObjectData.addAlternate(1);
		    
		    TileObjectData.addTile(Type);
		    AddMapEntry(new Color(123, 134, 145));
	    }
	    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	    {
		    Utilities.SimpleGlowmask(i, j, spriteBatch, Texture);
	    }
		
    }
}

