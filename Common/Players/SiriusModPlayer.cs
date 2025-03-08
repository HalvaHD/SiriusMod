using Terraria;
using Terraria.ModLoader;

namespace SiriusMod.Mechanics
{
    public class SiriusModPlayer : ModPlayer
    {
        public int MaxOverheat = 600;

        public override void ResetEffects()
        {
            MaxOverheat = 600;
        }
    }
}
