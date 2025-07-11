using Microsoft.Xna.Framework;
using SiriusMod.Content.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace SiriusMod.Content.Tiles.LaboratoryTiles
{
    public class LabPlatingBetter : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;

            DustType = ModContent.DustType<LabDust>();

            AddMapEntry(new Color(123, 134, 145));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
            => num = fail ? 1 : 3;
    }
}