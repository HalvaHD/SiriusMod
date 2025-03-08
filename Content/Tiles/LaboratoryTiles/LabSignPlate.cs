using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiriusMod.Helpers;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace SiriusMod.Content.Tiles.LaboratoryTiles
{
    public class LabSignPlate : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileSign[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;
            
            
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Width = 5;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = [ 16, 16, 16 ];
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.LavaDeath = false;
            
            AddMapEntry(new Color(71, 95, 114));
            TileObjectData.addTile(Type);
        }
        
        public override bool RightClick(int i, int j)
        {
            Sign.ReadSign(i, j, true);
            return true;
        }
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Utilities.SimpleGlowmask(i, j, spriteBatch, Texture);
        }
    }
}
