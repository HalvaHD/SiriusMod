using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Dusts
{
	public class LabMossDust : ModDust
	{
		public override void SetStaticDefaults()
			=> UpdateType = DustID.Grass;
	}
}