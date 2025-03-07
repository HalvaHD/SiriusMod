using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SiriusMod.Helpers
{
    public class SiriusModHelper : ModSystem
    {
    
    }

    public static class Utilities
    {
        public static Color ColorSwap(Color firstColor, Color secondColor, float seconds)
        {
            float frameDuration = seconds * 60;
            float frameCount = Main.GlobalTimeWrappedHourly * 60;
            float lerpFactor = frameCount % (frameDuration * 2) / frameDuration;

            if (lerpFactor > 1f)
                lerpFactor = 2f - lerpFactor;

            return Color.Lerp(firstColor, secondColor, lerpFactor);
        }
    }
    
    
}

