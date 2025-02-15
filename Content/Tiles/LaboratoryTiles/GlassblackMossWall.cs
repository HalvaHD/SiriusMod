using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SiriusMod.Content.Tiles.LaboratoryTiles
{
    public class GlassblackMossWall : ModWall
    {
        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = true;

            AddMapEntry(new Color(123, 134, 145));
        }
    }
}