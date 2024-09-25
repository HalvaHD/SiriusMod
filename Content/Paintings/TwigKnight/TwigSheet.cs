using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria;

namespace Twig.Content.Paintings.TwigKnight
{
    internal class TwigSheet : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileSpelunker[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16];
            TileObjectData.addTile(Type);
            DustType = 7;
            TileID.Sets.DisableSmartCursor[Type] = true;
            DustType = 7;
            TileID.Sets.DisableSmartCursor[Type] = true;
        }
    }
}
