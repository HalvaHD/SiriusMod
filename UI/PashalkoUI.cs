using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using ReLogic.Graphics;
using SiriusMod.Content.Tiles;
using SiriusMod.Helpers;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace SiriusMod.UI
{
    public class PashalkoUI : ModSystem
    {
        private const float PosX = 50.104603f;
        private const float PosY = 55.765408f;
        private static string GratitudeText = "";
        private static bool Reading = false;
        private static int i = 0;
        

        private static Texture2D frameTexture;
        public override void OnModLoad()
        {
            frameTexture = ModContent.Request<Texture2D>("SiriusMod/UI/ChatBackground", AssetRequestMode.ImmediateLoad).Value;
        }

        public override void Unload()
        {
            frameTexture = null;
        }

        public static void Draw(SpriteBatch spriteBatch, Player player)
        {
            if (GratitudeText == "")
            {
                GratitudeText = Language.GetTextValue($"Mods.SiriusMod.GratitudeText");
            }
            if (Gameraiders101_Pashalko.ReadSign == true)
            {
                player.mouseInterface = true;
                Vector2 screenRatioPosition = new Vector2(PosX, PosY - 20);
                Vector2 screenPos = screenRatioPosition;
                screenPos.X = (int)(screenPos.X * 0.01f * Main.screenWidth);
                screenPos.Y = (int)(screenPos.Y * 0.01f * Main.screenHeight);
                
                string text = FontAssets.MouseText.Value.CreateWrappedText(GratitudeText.Substring(0, i), frameTexture.Width - 15f);
                if (i < GratitudeText.Length)
                {
                    i++;
                }
                DrawPashalkoFrame(spriteBatch, screenPos, text);
            }
            if (i > 10 && (PlayerInput.Triggers.Current.MouseLeft || PlayerInput.Triggers.Current.MouseRight))
            {
                Reading = false;
                Gameraiders101_Pashalko.ReadSign = false;
                i = 0;
            }
        }

        private static void DrawPashalkoFrame(SpriteBatch spriteBatch, Vector2 screenPos, string text)
        {
            float uiScale = Main.UIScale;
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            spriteBatch.Draw(frameTexture, screenPos, new Rectangle(0, 0, frameTexture.Width, frameTexture.Height), Color.White, 0f, frameTexture.Size() * 0.5f, uiScale, SpriteEffects.None, 0);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            spriteBatch.DrawString(FontAssets.MouseText.Value, text, screenPos + new Vector2(-244 * uiScale, -70 * uiScale) , new Color(0, 92, 91), 0f, new Vector2(0,0), uiScale, SpriteEffects.None, 0);
            spriteBatch.DrawString(FontAssets.MouseText.Value, text, screenPos + new Vector2(-241 * uiScale, -70 * uiScale) , Color.White, 0f, new Vector2(0,0), uiScale, SpriteEffects.None, 0);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

        }
    }
}

