using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Content.Dusts;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ProtoMod.Content.Tiles.LaboratoryTiles
{
    public class BrokenServers : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = false;
            Main.tileLavaDeath[Type] = false;
            Main.tileWaterDeath[Type] = false;
			
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 16];
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.LavaDeath = false;

            TileObjectData.addTile(Type);
            AddMapEntry(new Color(123, 134, 145));

            base.SetStaticDefaults();
        }
    }
}