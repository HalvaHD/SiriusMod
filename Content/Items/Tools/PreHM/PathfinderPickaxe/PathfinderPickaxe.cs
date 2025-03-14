using System;
using SiriusMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiriusMod.Core;
using SiriusMod.Mechanics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items.Tools.PreHM.PathfinderPickaxe
{
    public class PathfinderPickaxe : Overheat, IHeldItemGlowing
    {
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 9;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.pick = 59;
            Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox) {
            if (Main.rand.NextBool(10)) {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<LabMossDust>());
            }
        }
        
        public override void OnHitNPC(Player player, Terraria.NPC target, Terraria.NPC.HitInfo hit, int damageDone)
        {
            if (CooldownLevel > 0)
            {
                target.AddBuff(BuffID.OnFire, 60);
            }
        }
        
        public override void HoldItem(Player player)
        {
            base.HoldItem(player);
            
            if (player.controlUseItem)
            {
                if (OverheatLevel >= 300 && OverheatLevel < player.GetModPlayer<SiriusModPlayer>().MaxOverheat)
                {
                    float bonusRatio = (OverheatLevel - 300f) / (player.GetModPlayer<SiriusModPlayer>().MaxOverheat - 300f);
                    player.pickSpeed -= MathHelper.Clamp(bonusRatio * player.GetModPlayer<SiriusModPlayer>().pickSDP, 0f, player.GetModPlayer<SiriusModPlayer>().pickSDP) * 0.01f;
                }
            }
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            
            if (CooldownLevel > 0)
            {
                Item.useTime = 50;
                Item.useAnimation = 30;
            }
            else
            {
                Item.useTime = 9;
                Item.useAnimation = 20;
            }
        }
        
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D glowmaskTexture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Vector2 origin = glowmaskTexture.Size() / 2f;

            spriteBatch.Draw(glowmaskTexture, Item.Center - Main.screenPosition, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
        }

        public void DrawGlowmask(PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;
            float maxOverheat = player.GetModPlayer<SiriusModPlayer>().MaxOverheat;
            float overheatLevel = OverheatLevel;
            
            if (player.itemAnimation == 0)
                return;
            
            Texture2D glowmaskTexture = ModContent.Request<Texture2D>(Texture + "_Animated").Value;
            
            int frameCount = 7;
            int frameHeight = glowmaskTexture.Height / frameCount;
            int currentFrame = (int) (overheatLevel / (maxOverheat / frameCount));
            currentFrame = Math.Clamp(currentFrame, 1, frameCount);
            
            Rectangle itemFrame = glowmaskTexture.Frame(1, frameCount, 0, currentFrame);
            Vector2 origin = new Vector2(player.direction == 1 ? 0 : glowmaskTexture.Width, player.gravDir == -1f ? 0 : frameHeight);

            DrawData data = new DrawData(glowmaskTexture, drawInfo.ItemLocation - Main.screenPosition, itemFrame, Color.White, player.itemRotation, origin, player.HeldItem.scale, drawInfo.playerEffect);
            drawInfo.DrawDataCache.Add(data);
        }
    }
}
