using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SiriusMod.Content.Tiles.LaboratoryTiles
{
    public class LabTv : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileWaterDeath[Type] = false;
            
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Width = 4;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16];
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.Origin = new Point16(1, 2);
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.AnchorWall = true;
            TileObjectData.newTile.LavaDeath = false;

            TileObjectData.newTile.StyleWrapLimit = 3;
            TileObjectData.newTile.StyleHorizontal = true;
            
            
            TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
            TileObjectData.newSubTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.addSubTile(1);
            
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newSubTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
            TileObjectData.addAlternate(1);
            
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(71, 95, 114));
        }
    }
}
