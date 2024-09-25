using System.Collections.Generic;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using ProtoMod.Content.Items;
using ProtoMod.Content.NPC;
using Terraria;
using Terraria.DataStructures;
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
            DrawOriginOffsetY -= 315; //Originally for 0.4 scale use -=190
            DrawOffsetX -= 298;
            Projectile.hide = true;

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

        public override void OnSpawn(IEntitySource source)
        {
            Main.dayTime = false;
        }


        // Allows you to determine the color and transparency in which a projectile is drawn
        // Return null to use the default color (normally light and buff color)
        // Returns null by default.

        public override void AI()
        {
            if (Projectile.timeLeft == 220)
            {
                SummonNPCs(Main.LocalPlayer);
            }
            if (Projectile.timeLeft > 60)
            {
                if (Projectile.ai[0]++ % 20 == 0)
                {
                    Vector2 spawnPos = new Vector2(0f, -50f).RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(360f)));

                    Dust.NewDustPerfect(Projectile.Center + spawnPos,
                        DustID.Adamantite,spawnPos.RotatedBy(Main.rand.NextFloat(-10f, 10f)), 0,Color.White);
                }
                
                if (Terraria.NPC.FindFirstNPC(ModContent.NPCType<HALVA>()) != -1)
                {
                    CameraPanSystem.PanTowards(Main.npc[Terraria.NPC.FindFirstNPC(ModContent.NPCType<HALVA>())].Center, 1f);
                }
                else
                {
                    CameraPanSystem.PanTowards(Projectile.Center, 1f);
                }

                if (Terraria.NPC.FindFirstNPC(ModContent.NPCType<HALVA>()) != -1 &&
                    Terraria.NPC.FindFirstNPC(ModContent.NPCType<KORRO>()) != -1)
                {
                    ModContent.GetModNPC(ModContent.NPCType<HALVA>()).NPC.GravityMultiplier *= 0.1f;
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
                Projectile.rotation += MathHelper.Pi / 12;

            }

            if (Projectile.timeLeft > 140)
            {
                if (Projectile.scale < 0.4f)
                {
                    Projectile.scale += 0.03f;
                }
                
            }

            if (Projectile.timeLeft < 40)
            {
                Projectile.scale -= 0.01f;
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

            if (Terraria.NPC.FindFirstNPC(ModContent.NPCType<HALVA>()) == -1)
            {
                Terraria.NPC.NewNPC(new EntitySource_ItemUse(player, new Item(type)),
                    (int)Projectile.Center.X,
                    (int)Projectile.Center.Y,
                    ModContent.NPCType<HALVA>(), Target: 255);
            }
            Main.NewText((object)Language.GetTextValue("Mods.Twig.ItemChat.AutNPCs"));
            AutAnimation.AutAnimation.AutCanSpawn = true;
        }
    }
    

    public class AutPortalLight : ModSystem
    {
        public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
        {
            if (AutPortal.PortalOpen)
            {
                backgroundColor = Color.Black;
                tileColor = Color.DarkRed;
            } 
        }

        public override void ModifyLightingBrightness(ref float scale)
        {
            if (AutPortal.PortalOpen)
            {
                scale = 0.5f;
            } 
        }
    }
}