using System.Collections.Generic;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Content.Items;
using ProtoMod.Content.NPC;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProtoMod.Content.Projectiles.AutPortal
{
    // This file shows an animated projectile
    // This file also shows advanced drawing to center the drawn projectile correctly
    public class AutPortal : ModProjectile
    {
        public static bool PortalOpen;
        public static bool ColorChange;
        
        public override string Texture => "ProtoMod/Assets/Textures/Menu/BlankPixel";
        public override void SetStaticDefaults()
        {
            // Total count animation frames
            //Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 615; // The width of projectile hitbox
            Projectile.height = 621; // The height of projectile hitbox
            Projectile.friendly = false; // Can the projectile deal damage to enemies?
            Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.timeLeft = 300;
            Projectile.aiStyle = -1;
            //Projectile.scale = 0.4f;
            Projectile.scale = 0.01f;
            Projectile.hide = false;

        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs,
            List<int> behindProjectiles, List<int> overPlayers,
            List<int> overWiresUI)
        {
            behindNPCs.Add(index);
        }
        public float Timer {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        
        

        public override void AI()
        {
            if (Projectile.timeLeft == 220)
            {
                SummonNPCs(Main.LocalPlayer);
            }
            if (Projectile.timeLeft > 60)
            {

                
                if (Terraria.NPC.FindFirstNPC(ModContent.NPCType<HALVA_Prime>()) != -1)
                {
                    CameraPanSystem.PanTowards(Main.npc[Terraria.NPC.FindFirstNPC(ModContent.NPCType<HALVA_Prime>())].Center, 1f);
                }
                else
                {
                    CameraPanSystem.PanTowards(Projectile.Center, 1f);
                }

                if (Terraria.NPC.FindFirstNPC(ModContent.NPCType<HALVA_Prime>()) != -1 &&
                    Terraria.NPC.FindFirstNPC(ModContent.NPCType<KORRO>()) != -1)
                {
                    ModContent.GetModNPC(ModContent.NPCType<HALVA_Prime>()).NPC.GravityMultiplier *= 0.1f;
                    ModContent.GetModNPC(ModContent.NPCType<KORRO>()).NPC.GravityMultiplier *= 0.1f;
                }
                
                PortalOpen = true;
            }

            if (Projectile.timeLeft < 20)
            {
                PortalOpen = false;
                ColorChange = true;
            }

            if (Projectile.timeLeft < 3)
            {
                ColorChange = false;
            }
            Timer++;
            
            if (++Projectile.frameCounter >= 3)
            {
                Projectile.frameCounter = 0;
                // Projectile.rotation += MathHelper.Pi / 12;

            }

            if (Projectile.timeLeft > 140)
            {
                if (Projectile.scale <= 1.75f)
                {
                    Projectile.scale += 0.03f;
                }
                
            }

            if (Projectile.timeLeft < 40)
            {
                if (Projectile.scale > 0f)
                    Projectile.scale -= 0.1f;
                if (Projectile.scale < 0.1f)
                    Projectile.Opacity = 0f;
            }
            
        }
        public virtual void SummonNPCs(Player player)
        {
            ScreenShakeSystem.StartShakeAtPoint(Projectile.Center, 6f, 2f, shakeStrengthDissipationIncrement:0.02f);
            int type = ModContent.ItemType<Aut>();
            if (Terraria.NPC.FindFirstNPC(ModContent.NPCType<KORRO>()) == -1)
            {
                Terraria.NPC.NewNPC(new EntitySource_ItemUse(player, new Item(type)), (int)Projectile.Center.X,
                    (int)Projectile.Center.Y,
                    ModContent.NPCType<KORRO>(), Target: 255);
            }

            if (Terraria.NPC.FindFirstNPC(ModContent.NPCType<HALVA_Prime>()) == -1)
            {
                Terraria.NPC.NewNPC(new EntitySource_ItemUse(player, new Item(type)),
                    (int)Projectile.Center.X,
                    (int)Projectile.Center.Y,
                    ModContent.NPCType<HALVA_Prime>(), Target: 255);
            }
            Main.NewText((object)Language.GetTextValue("ProtoMod.Twig.ItemChat.AutNPCs"));
            AutAnimation.AutAnimation.AutCanSpawn = true;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D extraTexture1 = ModContent.Request<Texture2D>($"ProtoMod/Assets/ExtraTextures/Vortex").Value;

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, Main.DefaultSamplerState,
                DepthStencilState.None, Main.Rasterizer, ProtoMod.SpriteRotation, Main.GameViewMatrix.ZoomMatrix);
            ProtoMod.SpriteRotation.Parameters["rotation"].SetValue(MathHelper.ToRadians(Main.GlobalTimeWrappedHourly * 180f));
            Color color = Color.Lerp(Color.White, Color.DarkRed, 0.6f);
            ProtoMod.SpriteRotation.Parameters["uColor"].SetValue(color.ToVector4());

            Main.spriteBatch.Draw(extraTexture1, Projectile.Center - Main.screenPosition, new Rectangle?(), Color.White,
                Projectile.rotation, extraTexture1.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState,
                DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);
            return false;
        }
    }
    

    public class AutPortalLight : ModSystem
    {
    }
}