using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod;
using CalamityMod.Items.Placeables.FurnitureMonolith;
using CalamityMod.Projectiles.Boss;
using Luminance.Common.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary.Core;
using ProtoMod.Content.NPC.Bosses.Protector;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Particle = Luminance.Core.Graphics.Particle;

namespace ProtoMod.Content.Projectiles
{
    public class ProtectorBlast : ModProjectile
    {
        public override string Texture => "ProtoMod/Content/Projectiles/ProtectorBlastCircle";
        public Vector2 InitPos;
        public float a = 0.05f;
        public float b = 0.05f;
        public bool PreImpact = false;
        public bool Impact = false;
        SlotId PreImpactSound;
        SlotId ImpactSound;
        SlotId ChargeSound;
        readonly SoundStyle ImpactStyle = new SoundStyle("ProtoMod/Assets/Sounds/Impact");
        private readonly SoundStyle ChargeStyle = new SoundStyle("ProtoMod/Assets/Sounds/Charge") { Volume = 0.8f };
        readonly SoundStyle PreImpactStyle = new SoundStyle("ProtoMod/Assets/Sounds/Pre-Impact") {Volume = 5f};
        public static bool InvisibleDuringBoom = false;
        public static bool ImpactState;

        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 128;
            Projectile.height = 128;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.Opacity = 0f;
            Projectile.scale = 0.5f;
            Projectile.timeLeft = 1300;
        }

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.localAI[0]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.localAI[0] = reader.ReadSingle();
        }

        public override void OnSpawn(IEntitySource source)
        {
            InitPos = Projectile.Center;
            SoundStyle style = new SoundStyle("ProtoMod/Assets/Sounds/Charge");
            SoundEngine.SoundPlayer.Play(style);
            PreImpact = false;
            Impact = false;
            InvisibleDuringBoom = false;
        }

        public override void OnKill(int timeLeft)
        {
            Protector.ProjectileFirtsPhaseExist = false;

            SoundEngine.SoundPlayer.StopAll(ChargeStyle);
            SoundEngine.SoundPlayer.StopAll(PreImpactStyle);
            Protector.ShootCooldDown = 300;
            Luminance.Core.Graphics.ScreenShakeSystem.StartShake(5);
            InvisibleDuringBoom = false;
            ImpactState = false;
        }

        public override void AI()
        {
            if (Projectile.timeLeft < 1080 && Main.rand.NextBool(3))
            {
                Vector2 dustpos = new Vector2(Projectile.Center.X, Projectile.Center.Y) +
                                 Main.rand.NextVector2Circular(70 + ((1080 - Projectile.timeLeft)/10), 70 + ((1080 - Projectile.timeLeft)/10));
                ParticleLibrary.Core.Particle dust = ParticleSystem.NewParticle(dustpos, Vector2.Zero, new ProtectorParticle(), Color.White, 5f, Layer.BeforeProjectiles);
                dust.TimeLeft = 100;
                dust.Rotation += 0.1f;
                dust.Velocity = - (Projectile.Center - dust.Position) * 0.1f;
                dust.Position.X -= 5;
                dust.Position.Y -= 5;
                
                Vector2 dustpos2 = Main.rand.NextVector2CircularEdge(200 + ((1080 - Projectile.timeLeft)/10), 200 + ((1080 - Projectile.timeLeft)/10));
                ParticleLibrary.Core.Particle dust2 = ParticleSystem.NewParticle(Projectile.VisualPosition + dustpos2, Vector2.Zero, new ProtectorParticle2(), Color.White, 5f,Layer.BeforeProjectiles);
                dust2.TimeLeft = 20 ;
                Vector2 dust2velocity = (Projectile.Center - dust2.Position) * 0.05f;
                dust2.Velocity = dust2velocity;
                dust2.Rotation = dust2.Velocity.ToRotation();
                dust2.Position.X -= 5;
                dust2.Position.Y -= 5;
                
                Vector2 particlePosition = Projectile.Center + Main.rand.NextVector2CircularEdge(60 + ((1080 - Projectile.timeLeft)/10),60 + ((1080 - Projectile.timeLeft)/10));
                Vector2 particleVelocity = particlePosition - Projectile.Center;
                float particlerotation = (particleVelocity.ToRotation());
                Projectile ElectroBolt = Projectile.NewProjectileDirect(new EntitySource_Parent(Projectile), particlePosition, Vector2.Zero, 
                    ModContent.ProjectileType<ProtectorLightning>(), 0, 0);
                ElectroBolt.rotation = particlerotation;
                

            }
            a += 0.005f;
            b += 0.005f;
            if (Projectile.timeLeft > 180)
            {
                Projectile.Opacity = MathHelper.Clamp(Projectile.Opacity + 0.1f, 0f, 1f);
                if (!SoundEngine.TryGetActiveSound(ChargeSound, out var activeSound3))
                {
                    ChargeSound = SoundEngine.PlaySound(ChargeStyle);
                }
            }

            if (Projectile.timeLeft == 180)
            {
                Projectile.Opacity = 1f;
                if (!SoundEngine.TryGetActiveSound(ImpactSound, out var activeSound4))
                {
                    ImpactSound = SoundEngine.PlaySound(ImpactStyle);
                }
            }

            if (Projectile.timeLeft < 180)
            {
                Luminance.Core.Graphics.ScreenShakeSystem.StartShake(10);
                Projectile.Opacity -= 0.0055f;
            }


            Projectile.rotation = Projectile.velocity.ToRotation();

            foreach (var npc in Main.ActiveNPCs)
            {
                if (npc.type == ModContent.NPCType<Protector>())
                {
                        Projectile.position = new Vector2((npc.Center -
                                                           ((((npc.Center - new Vector2(
                                                                 Main.player[npc.target].Center.X - 650,
                                                                 Main.player[npc.target].Center.Y))) /
                                                             Main.player[npc.target].moveSpeed / 4f) *
                                                            0.3f)).X, (npc.Center).Y + 170);
                    


                }
            }

            if (Projectile.timeLeft == 360)
            {
                if (!SoundEngine.TryGetActiveSound(PreImpactSound, out var activeSound))
                {
                    PreImpactSound = SoundEngine.PlaySound(PreImpactStyle);
                }

            }

            if (Projectile.timeLeft < 360)
            {
                a += 1f;
                b += 1f;
                InvisibleDuringBoom = true;

            }

            if (Projectile.timeLeft < 320 && Projectile.timeLeft > 80)
            {
                ImpactState = true;
                
            }

            if (Projectile.timeLeft <= 80)
            {
                ImpactState = false;
                foreach (var proj in Main.ActiveProjectiles)
                {
                    if (proj.type == ModContent.ProjectileType<YellowCrystalProjectile>())
                    {
                        proj.Kill();
                    }
                }
            }
        }

        public override bool CanHitPlayer(Player target) => false;

        public override void PostDraw(Color lightColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, Main.DefaultSamplerState,
                DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect)null,
                Main.GameViewMatrix.TransformationMatrix);


            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 origin = texture.Size() * 0.5f;
            Vector2 drawPosition = Projectile.Center - Main.screenPosition;
            if (Projectile.timeLeft < 1080)
            {
                drawPosition += Main.rand.NextVector2Circular(-3f, 3f);
            }
            Vector2 scale =
                (new Vector2(Projectile.velocity.Length() * 0.12f + 1f, 1f) / texture.Size() * Projectile.Size) *
                new Vector2(a, b);
            Color color = Projectile.GetAlpha(Color.Lerp(Color.Red, Color.White, 0.45f)) * 0.55f; //0.45 - original

            for (int i = 0; i < 25; i++)
            {
                Vector2 drawOffset = (MathHelper.TwoPi * i / 6f).ToRotationVector2() * 1.6f;
                Main.spriteBatch.Draw(texture, drawPosition + drawOffset, null, color * 0.7f, Projectile.rotation, origin,
                    scale, SpriteEffects.None, 0f);
            }

            Main.spriteBatch.Draw(texture, drawPosition, null, color, Projectile.rotation, origin, scale * 3f,
                SpriteEffects.None, 0f);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState,
                DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect)null,
                Main.GameViewMatrix.TransformationMatrix);


        }

        public override bool PreDraw(ref Color lightColor)
        {
            return false;
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs,
            List<int> behindProjectiles, List<int> overPlayers,
            List<int> overWiresUI)
        {
            overPlayers.Add(index);
            overWiresUI.Add(index);
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return false;
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {

        }
    }

    public class ProtectoModSystem : ModSystem
    {

        public override void ModifyLightingBrightness(ref float scale)
        {
            // if (ProtectorBlast.ImpactState == true)
            // {
            //     scale = 10f;
            // }
        }
    }
    
}
    


