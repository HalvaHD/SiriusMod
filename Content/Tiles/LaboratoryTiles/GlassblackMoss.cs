using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Tiles.LaboratoryTiles
{
    public class GlassblackMoss : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            

            AddMapEntry(new Color(123, 134, 145));
        }
        
         public override void RandomUpdate(int i, int j)
        {
            Tile tileBelow = Framing.GetTileSafely(i, j + 1);
            Tile tileAbove = Framing.GetTileSafely(i, j - 1);
            Tile tileAbove2 = Framing.GetTileSafely(i + 1, j - 1);
            Tile tile = Framing.GetTileSafely(i, j);
            if (WorldGen.genRand.NextBool(15) && !tileBelow.HasTile && tileBelow.LiquidType != LiquidID.Lava)
            {
                if (tile.Slope != SlopeType.SlopeUpLeft && tile.Slope != SlopeType.SlopeUpRight)
                {
                    tileBelow.TileType = (ushort)ModContent.TileType<GlassblackMossVines>();
                    tileBelow.HasTile = true;
                    tileBelow.TileColor = tile.TileColor;
                    WorldGen.SquareTileFrame(i, j + 1, true);
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendTileSquare(-1, i, j + 1, 3, TileChangeType.None);
                }
            }

            if (Main.rand.NextBool(15) && !tileAbove.HasTile && tileAbove.LiquidAmount == 0)
            {
                int rand = Main.rand.Next(6);
                WorldGen.PlaceObject(i, j - 1, ModContent.TileType<GlassblackMossGrass>(), true, rand);
                NetMessage.SendObjectPlacement(-1, i, j - 1, ModContent.TileType<GlassblackMossGrass>(), rand, 0, -1, -1);
                tileAbove.TileColor = tile.TileColor;
            }
        }
    }
}