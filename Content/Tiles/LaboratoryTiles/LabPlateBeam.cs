using Microsoft.Xna.Framework;
using ProtoMod.Content.Dusts;
using Terraria;
using Terraria.ModLoader;

namespace ProtoMod.Content.Tiles.LaboratoryTiles
{
	public class LabPlateBeam : ModWall
	{
		public override void SetStaticDefaults()
		{
			Main.wallHouse[Type] = false;
			AddMapEntry(new Color(40, 25, 47));
			DustType = ModContent.DustType<LabDust>();
		}

		
	}
}