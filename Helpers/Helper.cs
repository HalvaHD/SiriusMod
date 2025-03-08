using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
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

        public static void SimpleGlowmask(int i, int j, SpriteBatch spriteBatch, string Texture)
        {
            int xFrameOffset = Main.tile[i, j].TileFrameX;
            int yFrameOffset = Main.tile[i, j].TileFrameY;
            Texture2D glowmask = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Vector2 drawOffest = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
            Vector2 drawPosition = new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) + drawOffest;
            Color drawColour = Color.White;
            Tile trackTile = Main.tile[i, j];
            if (!trackTile.IsHalfBlock && trackTile.Slope == 0)
                spriteBatch.Draw(glowmask, drawPosition, new Rectangle(xFrameOffset, yFrameOffset, 16, 16), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
            else if (trackTile.IsHalfBlock)
                spriteBatch.Draw(glowmask, drawPosition + new Vector2(0f, 8f), new Rectangle(xFrameOffset, yFrameOffset, 18, 8), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);

        }
    }
    
    
}

