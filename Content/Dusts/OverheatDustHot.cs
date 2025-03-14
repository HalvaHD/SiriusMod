using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Dusts
{
    public class OverheatDustHot : ModDust
    {
        public override void SetStaticDefaults()
            => UpdateType = DustID.Granite;
    }
}