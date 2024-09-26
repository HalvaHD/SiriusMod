using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Common.Players;
using ProtoMod.Content.Items.Tools;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ProtoMod.Content.Projectiles
{
    // This file shows an animated projectile
    // This file also shows advanced drawing to center the drawn projectile correctly
    public class StarPickaxeCrystal : ModProjectile
    {
        public static bool IsStarPickaxeHeld;
        public static bool CanCrystalBeSeen;
        public static bool IsLastFrame;
        
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
        }

        public override void SetDefaults()
        {
            Projectile.width = 32; // The width of projectile hitbox
            Projectile.height = 40; // The height of projectile hitbox
            Projectile.friendly = false; // Can the projectile deal damage to enemies?
            Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.timeLeft = 3;
            Projectile.aiStyle = -1;
            DrawOffsetX -= 12;
            DrawOriginOffsetY -= 13;
            Projectile.scale = 0.7f;

        }

        // public override void OnKill(int timeLeft)
        // {
        //     CanCrystalBeSeen = false;
        // }
        public override Color? GetAlpha(Color lightColor)
        {
            Projectile.Opacity = 0;
            return Color.Aqua;

        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2D13 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            int num156 = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; //ypos of lower right corner of sprite to draw
            int y3;
            if (ProtoModPlayer.IsLastRealFrame)
            {
                y3 = num156 * 7;
            }
            else
            {
                y3 = num156 * Projectile.frame;
            }
            //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            

            Color color26 = Color.Aqua;
            color26 = Projectile.GetAlpha(color26);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);

            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), Projectile.GetAlpha(lightColor), Projectile.rotation , origin2, Projectile.scale, SpriteEffects.None);
            return false;
        }

        public override void AI()
        {
            ++Projectile.frameCounter;
            // if (Projectile.alpha != 0)
            // {
            //     Projectile.alpha -= 1;
            // }
            Player player = Main.player[Projectile.owner];
            Projectile.position = new Vector2(player.Center.X - 15, player.Center.Y - 64);
            if (IsStarPickaxeHeld)
            {
                Projectile.timeLeft = 3;
                if (CanCrystalBeSeen == false)
                {
                    CanCrystalBeSeen = true;
                }
            }
            else
            {
                CanCrystalBeSeen = false;
            }
            
            if (StarPickaxe.CrystalikState == 1)
            {
                
                if (Projectile.frameCounter % 5 == 0)
                {
                    if (Projectile.frame < 7 && ProtoModPlayer.IsLastRealFrame == false)
                    {
                        if (Projectile.frame == 0)
                        {
                            SoundStyle style = new SoundStyle("ProtoMod/Assets/Sounds/StarPickaxeCrystalBreakage");
                            SoundEngine.PlaySound(style);
                        }
                        Projectile.frame++;
                    }
                    else
                    {
                        Projectile.frame = 7;
                        ProtoModPlayer.IsLastRealFrame = true;
                    } 
                }
                
            }
            else
            {
                if (ProtoModPlayer.IsLastRealFrame == true)
                {
                    SoundStyle style = new SoundStyle("ProtoMod/Assets/Sounds/StarPickaxeCrystalRecovery");
                    SoundEngine.PlaySound(style);
                }
                ProtoModPlayer.IsLastRealFrame = false;
                if (Projectile.frameCounter % 5 == 0 && Projectile.frame > 0)
                {
                    // if (Projectile.frame == 7)
                    // {
                    //     SoundStyle style = new SoundStyle("Twig/Assets/Sounds/StarPickaxeCrystalRecovery");
                    //     SoundEngine.PlaySound(style);
                    // }
                    Projectile.frame--;
                }
            }
        }
    }
}