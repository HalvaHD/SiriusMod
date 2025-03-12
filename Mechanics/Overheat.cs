using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.Items.SummonItems;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;


namespace SiriusMod.Mechanics
{
    public abstract class Overheat : ModItem
    {
        private int OverheatTimer = 0;
        private int CooldownTimer = 0;
        public int OverheatLevel => OverheatTimer;
        public int CooldownLevel => CooldownTimer;
        private SlotId OverheatBrokeID;
        public SoundStyle OverheatBroke = new SoundStyle("SiriusMod/Assets/Sounds/OverheatBroke");
        
        public override void HoldItem(Player player)
        {
            if (player.controlUseItem)
            {
                if (CooldownTimer <= 0)
                {
                    OverheatTimer++;
                }
                
                float maxOverheat = player.GetModPlayer<SiriusModPlayer>().MaxOverheat;
                if (OverheatTimer >= maxOverheat)
                {
                    if (!SoundEngine.TryGetActiveSound(OverheatBrokeID, out var sound3))
                    {
                        OverheatBrokeID = SoundEngine.PlaySound(OverheatBroke);
                    }
                    
                    player.AddBuff(BuffID.OnFire, 300);
                    if (maxOverheat == 600)
                    {
                        CooldownTimer = 300;
                    }

                    if (maxOverheat == 1500)
                    {
                        CooldownTimer = 420;
                    }
                    OverheatTimer = 0;
                }
            }
            Main.NewText(player.GetModPlayer<SiriusModPlayer>().MaxOverheat);
            Main.NewText(player.GetModPlayer<SiriusModPlayer>().pickSDP);
            Main.NewText(CooldownLevel);
        }

        public override void UpdateInventory(Player player)
        {
            if (!player.controlUseItem)
            {
                if (OverheatTimer > 0)
                {
                    OverheatTimer--;
                }
            }

            if (CooldownTimer > 0)
            {
                CooldownTimer--;
            }
        }
        
        /*public Texture2D glowTexture = null;
        public int glowOffsetY = 0;
        public int glowOffsetX = 0;
        public override void PostDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (glowTexture != null)
            {
                Texture2D texture = glowTexture;
                spriteBatch.Draw
                (
                    texture,
                    new Vector2
                    (
                        item.position.X - Main.screenPosition.X + item.width * 0.5f,
                        item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
            base.PostDrawInWorld(item, spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
        }*/
    }
}
