using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Dusts
{
    public class OverheatDustCold : ModDust
    {
        public override void SetStaticDefaults()
            => UpdateType = DustID.Stone;
    }
}