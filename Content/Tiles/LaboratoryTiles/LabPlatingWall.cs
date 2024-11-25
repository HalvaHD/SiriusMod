using Microsoft.Xna.Framework;
using ProtoMod.Content.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace ProtoMod.Content.Tiles.LaboratoryTiles
{
	public class LabPlatingWall : ModWall
	{
		public override void SetStaticDefaults()
		{
			Main.wallHouse[Type] = true;
			AddMapEntry(new Color(40, 25, 47));
			DustType = ModContent.DustType<LabDust>();
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
	}
}