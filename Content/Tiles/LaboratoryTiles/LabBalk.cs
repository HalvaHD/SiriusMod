using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ProtoMod.Content.Tiles.LaboratoryTiles
{
    public class LabBalk : ModWall
    {


        public override void SetStaticDefaults()
        {
            Main.wallHouse[Type] = false;
            AddMapEntry(new Color(36, 35, 37));
            
        
    }
}