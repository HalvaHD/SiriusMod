using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Content;
using SiriusMod.Common;
using SiriusMod.Common.Players;
using SiriusMod.Helpers;
using SiriusMod.Mechanics;

namespace SiriusMod.UI.Mechanics
{
    // Как выяснилось все гораздо проще делеать через ModSystem
    public class OverheatUI : ModSystem
    {
        // На будущее
        private const float MouseDragEpsilon = 0.05f; // 0.05%
        
        // Дефолтная позиция под игроком
        internal const float DefaultPosX = 50.104603f;
        internal const float DefaultPosY = 55.765408f;
        
        // На будущее
        private static Vector2? dragOffset = null;
        private static Texture2D frameTexture, barTexture;
        public override void OnModLoad()
        {
            frameTexture = ModContent.Request<Texture2D>("SiriusMod/UI/Mechanics/Kitkat", AssetRequestMode.ImmediateLoad).Value;
            barTexture = ModContent.Request<Texture2D>("SiriusMod/UI/Mechanics/Kitkat_Bar", AssetRequestMode.ImmediateLoad).Value;
            dragOffset = null;
        }

        public override void Unload()
        {
            dragOffset = null;
            frameTexture = barTexture = null;
        }
        
        // Сам метод активируется в файлике UIManageSystem.cs
        public static void Draw(SpriteBatch spriteBatch, Player player)
        {
            if (Main.LocalPlayer.HeldItem.ModItem is not Overheat overheatItem)
            {
                return;
            }
            Vector2 screenRatioPosition = new Vector2(SiriusConfig.Instance.OverheatUIPosX, SiriusConfig.Instance.OverheatUIPosY);
            if (screenRatioPosition.X < 0f || screenRatioPosition.X > 100f)
                screenRatioPosition.X = DefaultPosX;
            if (screenRatioPosition.Y < 0f || screenRatioPosition.Y > 100f)
                screenRatioPosition.Y = DefaultPosY;
            
            float uiScale = Main.UIScale;
            Vector2 screenPos = screenRatioPosition;
            screenPos.X = (int)(screenPos.X * 0.01f * Main.screenWidth);
            screenPos.Y = (int)(screenPos.Y * 0.01f * Main.screenHeight);

            if (overheatItem.OverheatLevel > 0 || overheatItem.CooldownLevel > 0)
            {
                DrawOverheatBar(spriteBatch, player, overheatItem, screenPos);
            }
            else
            {
                bool IsPosChanged = false;
                if (SiriusConfig.Instance.OverheatUIPosX != screenRatioPosition.X)
                {
                    SiriusConfig.Instance.OverheatUIPosX = screenRatioPosition.X;
                    IsPosChanged = true;
                }
                if (SiriusConfig.Instance.OverheatUIPosY != screenRatioPosition.Y)
                {
                    SiriusConfig.Instance.OverheatUIPosY = screenRatioPosition.Y;
                    IsPosChanged = true;
                }
                if (IsPosChanged)
                {
                    SiriusMod.SaveConfig(SiriusConfig.Instance);
                }
                    
            }
            // Здесь отрисовываем шкалу
            Rectangle mouseHitbox = new Rectangle((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y, 8, 8);
            Rectangle OverheatBarArea = Utils.CenteredRectangle(screenPos, frameTexture.Size() * uiScale);

            MouseState ms = Mouse.GetState();
            Vector2 mousePos = Main.MouseScreen;

            // При пересечении
            if (OverheatBarArea.Intersects(mouseHitbox))
            {
                // if (!SiriusConfig.Instance.LockedOverheatUI)
                //     Main.LocalPlayer.mouseInterface = true;

                // If the mouse is on top of the meter, show the player's accumulated sulphuric poisoning.
                if (overheatItem.OverheatLevel > 0)
                {
                    string overheatText = (overheatItem.OverheatLevel / 600f * 100f).ToString("n2");
                    Main.instance.MouseText(overheatText, 0, 0, -1, -1, -1, -1);
                }

                Vector2 newScreenRatioPosition = screenRatioPosition;
                
               
                if (!SiriusConfig.Instance.BarsPosLock && ms.RightButton == ButtonState.Pressed)
                {
                    
                    if (!dragOffset.HasValue)
                        dragOffset = mousePos - screenPos;

                    Vector2 newCorner = mousePos - dragOffset.GetValueOrDefault(Vector2.Zero);
                    
                    newScreenRatioPosition.X = (100f * newCorner.X) / Main.screenWidth;
                    newScreenRatioPosition.Y = (100f * newCorner.Y) / Main.screenHeight;
                }

                // Compute the change in position. If it is large enough, actually move the meter
                Vector2 delta = newScreenRatioPosition - screenRatioPosition;
                if (Math.Abs(delta.X) >= MouseDragEpsilon || Math.Abs(delta.Y) >= MouseDragEpsilon)
                {
                    SiriusConfig.Instance.OverheatUIPosX = newScreenRatioPosition.X;
                    SiriusConfig.Instance.OverheatUIPosY = newScreenRatioPosition.Y;
                }
                
                if (ms.RightButton == ButtonState.Released)
                {
                    dragOffset = null;
                    SiriusMod.SaveConfig(SiriusConfig.Instance);
                }
            }
        }
        
        private static void DrawOverheatBar(SpriteBatch spriteBatch, Player player, Overheat OverheatItem, Vector2 screenPos)
        {
            if (OverheatItem.OverheatLevel > 0)
            {
                float uiScale = Main.UIScale;
                float offset = (frameTexture.Width - barTexture.Width) * 0.5f;
                float completionRatio = MathHelper.Clamp(OverheatItem.OverheatLevel / player.GetModPlayer<SiriusModPlayer>().MaxOverheat, 0f, 1f);
                float colorRatio = MathHelper.Clamp(OverheatItem.OverheatLevel / player.GetModPlayer<SiriusModPlayer>().MaxOverheat, 0.2f, 1f);
                Rectangle barRectangle = new Rectangle(0, 0, (int)(barTexture.Width * completionRatio), barTexture.Width);
                spriteBatch.Draw(frameTexture, screenPos, new Rectangle(0, 0, (frameTexture.Width), frameTexture.Height), Color.Lerp(Color.White, new Color(255, 67, 20), colorRatio), 0f, frameTexture.Size() * 0.5f, uiScale * 1.75f, SpriteEffects.None, 0);
                spriteBatch.Draw(barTexture, screenPos + new Vector2(offset * uiScale, 0) + new Vector2(4, 0), barRectangle, Color.White, 0f, frameTexture.Size() * 0.5f, uiScale * 1.75f, SpriteEffects.None, 0);
            }

            if (OverheatItem.CooldownLevel > 0 && OverheatItem.CooldownLevel <= 200)
            {
                float uiScale = Main.UIScale;
                float offset = (frameTexture.Width - barTexture.Width) * 0.5f;
                float completionRatio = MathHelper.Clamp(OverheatItem.CooldownLevel / 300f, 0f, 1f);
                //float colorRatio = MathHelper.Clamp(OverheatItem.CooldownLevel / 300f, 0.2f, 1f);
                Rectangle barRectangle = new Rectangle(0, 0, (int)(barTexture.Width * completionRatio), barTexture.Width);
                spriteBatch.Draw(frameTexture, screenPos, new Rectangle(0, 0, (frameTexture.Width), frameTexture.Height), Utilities.ColorSwap(new Color(255, 67, 20), Color.White, 0.5f), 0f, frameTexture.Size() * 0.5f, uiScale * 1.75f, SpriteEffects.None, 0);
                spriteBatch.Draw(barTexture, screenPos + new Vector2(offset * uiScale, 0) + new Vector2(4, 0), barRectangle, Color.White, 0f, frameTexture.Size() * 0.5f, uiScale * 1.75f, SpriteEffects.None, 0);
                
            }

            if (OverheatItem.CooldownLevel > 200)
            {
                float uiScale = Main.UIScale;
                float offset = (frameTexture.Width - barTexture.Width) * 0.5f;
                spriteBatch.Draw(frameTexture, screenPos, new Rectangle(0, 0, (frameTexture.Width), frameTexture.Height), new Color(255, 51, 0), 0f, frameTexture.Size() * 0.5f, uiScale * 1.75f, SpriteEffects.None, 0);
                spriteBatch.Draw(barTexture, screenPos + new Vector2(offset * uiScale, 0) + new Vector2(4, 0), new Rectangle(0,0, (barTexture.Width), barTexture.Height), Color.White, 0f, frameTexture.Size() * 0.5f, uiScale * 1.75f, SpriteEffects.None, 0);
            }
        }
    }
}
