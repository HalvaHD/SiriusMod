using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace ProtoMod.Content.Pets.LitosObliterator
{
    internal class LitosObliteratorProjectile : ModProjectile
    {
        public bool IsRotated;
        public int MaxFrame;
        public Vector2 IdlePosition;
        public float distanceToIdlePosition;
        public float speed;
        public float inertia;
        public Dust dust;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 12;
            Main.projPet[Projectile.type] = true;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 7; // how long you want the trail to be
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2; // recording mode

            // This code is needed to customize the vanity pet display in the player select screen. Quick explanation:
            // * It uses fluent API syntax, just like Recipe
            // * You start with ProjectileID.Sets.SimpleLoop, specifying the start and end frames aswell as the speed, and optionally if it should animate from the end after reaching the end, effectively "bouncing"
            // * To stop the animation if the player is not highlighted/is standing, as done by most grounded pets, add a .WhenNotSelected(0, 0) (you can customize it just like SimpleLoop)
            // * To set offset and direction, use .WithOffset(x, y) and .WithSpriteDirection(-1)
            // * To further customize the behavior and animation of the pet (as its AI does not run), you have access to a few vanilla presets in DelegateMethods.CharacterPreview to use via .WithCode(). You can also make your own, showcased in MinionBossPetProjectile
            ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] = ProjectileID.Sets.SimpleLoop(0, 5, 10)
                .WhenNotSelected(0, 5, 10).WithOffset(210f, 120f)
                .WithSpriteDirection((int)Math.PI);

        }

        public override void SetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 46;
            DrawOffsetX -= 16;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.scale = 0.7f;
        }

        public override bool PreAI()
        {
            return true;
        }
        public override bool PreDraw(ref Color lightColor)
        {
	        Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * Projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            SpriteEffects effects = SpriteEffects.None;
         
            Color color26 = lightColor;
            color26 = Projectile.GetAlpha(color26);
         
            float rotationOffset = Projectile.spriteDirection > 0 ? 0 : (float)Math.PI ;
         
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
         
            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Projectile.GetAlpha(lightColor), Projectile.rotation + rotationOffset, origin2, Projectile.scale, effects, 0);
            Texture2D texture = ModContent.Request<Texture2D>("Twig/Content/Pets/LitosObliterator/LitosObliteratorProjectile_Glowmask", AssetRequestMode.ImmediateLoad).Value;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Color.White * Projectile.Opacity, Projectile.rotation + rotationOffset, origin2, Projectile.scale, effects, 0);
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[Projectile.type]; i+= 4)
            {
                Color color27 = Color.WhiteSmoke;
                color27.A = 150;
                color27 *= (float)(ProjectileID.Sets.TrailCacheLength[Projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[Projectile.type];
                Vector2 value4 = Projectile.oldPos[i];
                float num165 = Projectile.oldRot[i];
                Main.EntitySpriteDraw(texture, value4 + Projectile.Size / 2f - Main.screenPosition + new Vector2(0, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, Projectile.rotation + rotationOffset, origin2, Projectile.scale, effects, 0);
            }

            return false;
        }

        public override void AI()
        {
            Projectile.scale = 0.9f;
            Player player = Main.player[Projectile.owner];
            if (Main.rand.NextFloat() < 0.37674417f && player.gravDir == 1) 
            { 
                // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                Vector2 dustposition = new Vector2(Projectile.Center.X - 16 * Projectile.spriteDirection, Projectile.Center.Y - 14 * player.gravDir);
                if (player.statLife > (player.statLifeMax2 / 2))
                {
                    dust = Main.dust[
                        Terraria.Dust.NewDust(dustposition, 0, 30, 330, -Projectile.velocity.X, -Projectile.velocity.Y * player.gravDir,
                            0, new Color(255, 255, 255), 0.87209296f)];
                }
                else
                {
                    // Red dust if player's life < 50%
                    dust = Main.dust[
                        Terraria.Dust.NewDust(dustposition, 0, 30, 331, -Projectile.velocity.X, -Projectile.velocity.Y * player.gravDir,
                            0, new Color(255, 255, 255), 0.87209296f)];
                }
            }
            
            // Lighting.AddLight(Projectile.Center, 1f, 0.1f, 0.1f);
            if (++Projectile.ai[0] % 5 == 0)
            {
                if (player.statLife > (player.statLifeMax2 / 2))
                {
                    if (++Projectile.frameCounter < 6)
                    {
                        ++Projectile.frame;
                    }
                    else
                    {
                        Projectile.frameCounter = 0;
                        Projectile.frame = 0;
                    }
                }
                else
                {
                    if (++Projectile.frameCounter < 12)
                    {
                        ++Projectile.frame;
                    }
                    else
                    {
                        Projectile.frameCounter = 6;
                        Projectile.frame = 6;
                    }
                }

            }
            
            if (player.direction == 1)
            {
                IdlePosition = new Vector2(player.Center.X - 8, player.Center.Y - 20);
                
            }
            else
            {
                IdlePosition = new Vector2(player.Center.X + 8, player.Center.Y - 20);
            }

            Vector2 position = new Vector2(player.Center.X + 32 * player.direction, player.Center.Y - 32) -
                               Projectile.Center;
            distanceToIdlePosition = (new Vector2(player.Center.X + 32 * player.direction, player.Center.Y - 32) -
                                     Projectile.Center).Length();
            if (distanceToIdlePosition > 600f) {
                // Speed up the minion if it's away from the player
                speed = 12f;
                inertia = 60f;
            }
            else {
                // Slow down the minion if closer to the player
                speed = 8f;
                inertia = 100f;
            }

            if (distanceToIdlePosition > 10f) {
                // The immediate range around the player (when it passively floats about)

                // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                position.Normalize();
                position *= speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + position) / inertia;
            }
            else if (Projectile.velocity == Vector2.Zero) {
                // If there is a case where it's not moving at all, give it a little "poke"
                Projectile.velocity.X = -2f;
                Projectile.velocity.Y = -2f;
            }

            if (Projectile.direction == -1)
            {
                Projectile.rotation = Projectile.rotation.AngleTowards(Projectile.velocity.ToRotation(), 0.1f);
            }
            else
            {
                Projectile.rotation = Projectile.rotation.AngleTowards(Projectile.velocity.ToRotation(), 0.1f);
            }

            
            
            
            
            
            
            
            // Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
            if (!player.dead && player.HasBuff(ModContent.BuffType<LitosObliteratorBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            
        }
    }
}

