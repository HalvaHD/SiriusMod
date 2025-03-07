using System;
using SiriusMod.Mechanics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using CalamityMod;
using CalamityMod.Cooldowns;
using Microsoft.Xna.Framework.Input;
using ReLogic.Content;
using SiriusMod.Helpers;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Animations;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.Social.Steam;
using Terraria.UI;

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
            //Main.NewText("Отрисовка идет");
            Vector2 screenRatioPosition = new Vector2(DefaultPosX, DefaultPosY); // TODO: Add config pos
            
            float uiScale = Main.UIScale;
            Vector2 screenPos = screenRatioPosition;
            screenPos.X = (int)(screenPos.X * 0.01f * Main.screenWidth);
            screenPos.Y = (int)(screenPos.Y * 0.01f * Main.screenHeight);
            
            // Здесь отрисовываем шкалу
            DrawOverheatBar(spriteBatch, overheatItem, screenPos);

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
                    string overheatText = (overheatItem.OverheatLevel / 420f * 100f).ToString("n2");
                    Main.instance.MouseText(overheatText, 0, 0, -1, -1, -1, -1);
                }

                Vector2 newScreenRatioPosition = screenRatioPosition;

                // TODO: Add config moveability
                // if (!SiriusConfig.Instance.MeterPosLock && ms.LeftButton == ButtonState.Pressed)
                // {
                //     // If the drag offset doesn't exist yet, create it.
                //     if (!dragOffset.HasValue)
                //         dragOffset = mousePos - screenPos;
                //
                //     // Given the mouse's absolute current position, compute where the corner of the water bar should be based on the original drag offset.
                //     Vector2 newCorner = mousePos - dragOffset.GetValueOrDefault(Vector2.Zero);
                //
                //     // Convert the new corner position into a screen ratio position.
                //     newScreenRatioPosition.X = (100f * newCorner.X) / Main.screenWidth;
                //     newScreenRatioPosition.Y = (100f * newCorner.Y) / Main.screenHeight;
                // }

                // Compute the change in position. If it is large enough, actually move the meter
                Vector2 delta = newScreenRatioPosition - screenRatioPosition;
                if (Math.Abs(delta.X) >= MouseDragEpsilon || Math.Abs(delta.Y) >= MouseDragEpsilon)
                {
                    // newScreenRatioPosition.X
                    // newScreenRatioPosition.Y
                }

                // When the mouse is released, save the config and destroy the drag offset.
                if (ms.LeftButton == ButtonState.Released)
                {
                    dragOffset = null;
                }
            }
        }
        
        private static void DrawOverheatBar(SpriteBatch spriteBatch, Overheat OverheatItem, Vector2 screenPos)
        {
            if (OverheatItem.OverheatLevel > 0)
            {
                float uiScale = Main.UIScale;
                float offset = (frameTexture.Width - barTexture.Width) * 0.5f;
                float completionRatio = MathHelper.Clamp(OverheatItem.OverheatLevel / 420f, 0f, 1f);
                float colorRatio = MathHelper.Clamp(OverheatItem.OverheatLevel / 420f, 0.2f, 1f);
                Rectangle barRectangle = new Rectangle(0, 0, (int)(barTexture.Width * completionRatio), barTexture.Width);
                spriteBatch.Draw(frameTexture, screenPos, new Rectangle(0, 0, (frameTexture.Width), frameTexture.Height), Color.Lerp(Color.White, new Color(255, 67, 20), colorRatio), 0f, frameTexture.Size() * 0.5f, uiScale * 2f, SpriteEffects.None, 0);
                spriteBatch.Draw(barTexture, screenPos + new Vector2(offset * uiScale, 0) + new Vector2(4, 0), barRectangle, Color.White, 0f, frameTexture.Size() * 0.5f, uiScale * 2f, SpriteEffects.None, 0);
            }

            if (OverheatItem.CooldownLevel > 0 && OverheatItem.CooldownLevel <= 420)
            {
                float uiScale = Main.UIScale;
                float offset = (frameTexture.Width - barTexture.Width) * 0.5f;
                float completionRatio = MathHelper.Clamp(OverheatItem.CooldownLevel / 420f, 0f, 1f);
                float colorRatio = MathHelper.Clamp(OverheatItem.CooldownLevel / 420f, 0.2f, 1f);
                Rectangle barRectangle = new Rectangle(0, 0, (int)(barTexture.Width * completionRatio), barTexture.Width);
                spriteBatch.Draw(frameTexture, screenPos, new Rectangle(0, 0, (frameTexture.Width), frameTexture.Height), Utilities.ColorSwap(new Color(255, 67, 20), Color.White, 0.5f), 0f, frameTexture.Size() * 0.5f, uiScale * 2f, SpriteEffects.None, 0);
                spriteBatch.Draw(barTexture, screenPos + new Vector2(offset * uiScale, 0) + new Vector2(4, 0), barRectangle, Color.White, 0f, frameTexture.Size() * 0.5f, uiScale * 2f, SpriteEffects.None, 0);
                
            }

            if (OverheatItem.CooldownLevel > 420)
            {
                float uiScale = Main.UIScale;
                float offset = (frameTexture.Width - barTexture.Width) * 0.5f;
                spriteBatch.Draw(frameTexture, screenPos, new Rectangle(0, 0, (frameTexture.Width), frameTexture.Height), new Color(255, 51, 0), 0f, frameTexture.Size() * 0.5f, uiScale * 2f, SpriteEffects.None, 0);
                spriteBatch.Draw(barTexture, screenPos + new Vector2(offset * uiScale, 0) + new Vector2(4, 0), new Rectangle(0,0, (barTexture.Width), barTexture.Height), Color.White, 0f, frameTexture.Size() * 0.5f, uiScale * 2f, SpriteEffects.None, 0);
            }
        }
    }
}
