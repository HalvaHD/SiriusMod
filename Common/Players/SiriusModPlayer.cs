using Terraria;
using Terraria.ModLoader;

namespace SiriusMod.Mechanics
{
    public class SiriusModPlayer : ModPlayer
    {
        public float  MaxOverheat = 600f;
        public float pickSDP = 20f;

        public override void ResetEffects()
        {
            MaxOverheat = 600f;
            pickSDP = 20f;
        }
    }
}
