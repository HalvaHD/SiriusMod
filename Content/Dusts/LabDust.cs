using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Dusts
{
	public class LabDust : ModDust
	{
		public override void SetStaticDefaults()
			=> UpdateType = DustID.Stone;
	}
}