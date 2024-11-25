using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ProtoMod.Content.Tiles.LaboratoryTiles
{
    public class LabBalk : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            

            AddMapEntry(new Color(123, 134, 145));
        }
    }
}