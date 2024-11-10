using Microsoft.Xna.Framework;
using ProtoMod.Content.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Tiles.Lab
{
	public class LabPlatingWallMossy : ModWall
	{
		public override void SetStaticDefaults()
		{
			Main.wallHouse[Type] = true;
			AddMapEntry(new Color(40, 25, 47));
		}

		public override bool CreateDust(int i, int j, ref int type)
		{
			Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, Main.rand.NextBool(3) ? ModContent.DustType<LabMossDust>() : ModContent.DustType<LabDust>(), 0f, 0f, 0, default, 1f);
			return false;
		}

		public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
	}
}