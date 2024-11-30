using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Content.Dusts;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ProtoMod.Content.Tiles.LaboratoryTiles
{
    public class BrokenServerChest : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            Main.tileWaterDeath[Type] = false;
			
            TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
            TileObjectData.newTile.Height = 18;
            TileObjectData.newTile.Width = 6;
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16, 16];
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.LavaDeath = false;

            TileObjectData.addTile(Type);
            AddMapEntry(new Color(123, 134, 145));
            
        }
    }
}