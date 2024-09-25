using System;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Twig.Common.Players;
using Twig.Content.Buffs;
using Twig.Content.Items.Accessories;
using Twig.Content.NPC;

namespace Twig.Content.Projectiles
{
    public class GracingEnergyOrb : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.scale = 0.5f;
            Projectile.timeLeft = 60;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 1f, 1f, 1f);
            Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height,
                    DustID.TintableDustLighted, Projectile.velocity.X * 2f, Projectile.velocity.Y * 2f);
            // If found, change the velocity of the projectile and turn it in the direction of the target

            // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
            Projectile.velocity = (Main.LocalPlayer.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 20f;
            Projectile.rotation = Projectile.velocity.ToRotation();
            if (Math.Abs(Projectile.Center.X - Main.LocalPlayer.Center.X) < 20)
            {
                if (TwigModPlayer.DamageTypeForGracing == DamageClass.SummonMeleeSpeed ||
                    TwigModPlayer.DamageTypeForGracing == DamageClass.Summon)
                {
                    Main.LocalPlayer.Heal(1);
                }

                if (TwigModPlayer.DamageTypeForGracing == DamageClass.Ranged ||
                    TwigModPlayer.DamageTypeForGracing == DamageClass.Magic ||
                    TwigModPlayer.DamageTypeForGracing == DamageClass.Melee ||
                    TwigModPlayer.DamageTypeForGracing == DamageClass.Generic ||
                    TwigModPlayer.DamageTypeForGracing == DamageClass.Throwing ||
                    TwigModPlayer.DamageTypeForGracing == DamageClass.Default)
                {
                    if (Main.LocalPlayer.HeldItem.useTime <= 8)
                    {
                        Main.LocalPlayer.Heal(1);
                    }
                    else if (8 < Main.LocalPlayer.HeldItem.useTime && Main.LocalPlayer.HeldItem.useTime <= 20)
                    {
                        Main.LocalPlayer.Heal(3);
                    }
                    else if (21 < Main.LocalPlayer.HeldItem.useTime && Main.LocalPlayer.HeldItem.useTime <= 25)
                    {
                        Main.LocalPlayer.Heal(5);
                    }
                    else if (25 < Main.LocalPlayer.HeldItem.useTime && Main.LocalPlayer.HeldItem.useTime <= 30)
                    {
                        Main.LocalPlayer.Heal(7);
                    }
                    else if (30 < Main.LocalPlayer.HeldItem.useTime && Main.LocalPlayer.HeldItem.useTime <= 35)
                    {
                        Main.LocalPlayer.Heal(10);
                    }
                    else if (35 < Main.LocalPlayer.HeldItem.useTime && Main.LocalPlayer.HeldItem.useTime <= 45)
                    {
                        Main.LocalPlayer.Heal(15);
                    }
                    else if (45 < Main.LocalPlayer.HeldItem.useTime && Main.LocalPlayer.HeldItem.useTime <= 55)
                    {
                        Main.LocalPlayer.Heal(20);
                    }
                    else if (Main.LocalPlayer.HeldItem.useTime > 55)
                    {
                        Main.LocalPlayer.Heal(30);
                    }

                }

                Projectile.Kill();
            }
        }

        public Terraria.NPC FindClosestNPC(float maxDetectDistance)
            {
                Terraria.NPC closestNPC = null;

                // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
                float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

                // Loop through all NPCs(max always 200)
                for (int k = 0; k < Main.maxNPCs; k++)
                {
                    Terraria.NPC target = Main.npc[k];
                    // Check if NPC able to be targeted. It means that NPC is
                    // 1. active (alive)
                    // 2. chaseable (e.g. not a cultist archer)
                    // 3. max life bigger than 5 (e.g. not a critter)
                    // 4. can take damage (e.g. moonlord core after all it's parts are downed)
                    // 5. hostile (!friendly)
                    // 6. not immortal (e.g. not a target dummy)
                    if (target.CanBeChasedBy())
                    {
                        // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                        float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Main.LocalPlayer.Center);

                        // Check if it is within the radius
                        if (sqrDistanceToTarget < sqrMaxDetectDistance)
                        {
                            sqrMaxDetectDistance = sqrDistanceToTarget;
                            closestNPC = target;
                        }
                    }
                }

                return closestNPC;
            }
            //     if (SmallBoi)
            //     {
            //         Projectile.Kill();
            //     }
            //      // The maximum radius at which a projectile can detect a target
            //     float projSpeed = 50f; // The speed at which the projectile moves towards the target
            //     // Trying to find NPC closest to the projectile
            //     if (closestNPC != null){
            //         Point = new Vector2(closestNPC.Center.X, closestNPC.Center.Y - 2) - Projectile.Center;
            //         Projectile.velocity = (Point).SafeNormalize(Vector2.Zero) * projSpeed;
            //     }
            //
            //     // if (Hitted == false)
            //     // {
            //     //     // If found, change the velocity of the projectile and turn it in the direction of the target
            //     //     // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
            //
            //     //     
            //     // }
            //     // else
            //     // {
            //     //     KORRO.NPC.position = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * projSpeed;
            //     //     float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, KORRO.NPC.Center);
            //     //
            //     //     // Check if it is within the radius
            //     //     if (sqrDistanceToTarget < 5f)
            //     //     {
            //     //         Hitted = false;
            //     //         Projectile.Kill();
            //     //     }
            //     // }
            //     //
            //     //
            //     // Vector2 projVector = KORRO.NPC.Center - Projectile.Center;
            //     // // Projectile.rotation = projVector.ToRotation() - MathHelper.PiOver2;
            //     // if (projVector.X < 0f)
            //     // {
            //     //     KORRO.NPC.direction = 1;
            //     //     Projectile.direction = 1;
            //     // }
            //     // else
            //     // {
            //     //     KORRO.NPC.direction = -1;
            //     //     Projectile.direction = -1;
            //     // }
            //     // // Projectile.spriteDirection = (projVector.X > 0f) ? -1 : 1;
            //     // // if (Projectile.ai[0] == 0f && projVector.Length() > 400f)
            //     // // {
            //     // //     Projectile.ai[0] = 1f;
            //     // // }
            //     // // if (Projectile.ai[0] == 1f || Projectile.ai[0] == 2f)
            //     // // {
            //     // //     float KORRODist = projVector.Length();
            //     // //     if (KORRODist > 1500f)
            //     // //     {
            //     // //         Projectile.Kill();
            //     // //         return;
            //     // //     }
            //     // //     if (KORRODist > 600f)
            //     // //     {
            //     // //         Projectile.ai[0] = 2f;
            //     // //     }
            //     // //     Projectile.tileCollide = false;
            //     // //     float returnSpeed = 20f;
            //     // //     if (Projectile.ai[0] == 2f)
            //     // //     {
            //     // //         returnSpeed = 40f;
            //     // //     }
            //     // //     Projectile.velocity = Vector2.Normalize(projVector) * returnSpeed;
            //     // //     if (projVector.Length() < returnSpeed)
            //     // //     {
            //     // //         Projectile.Kill();
            //     // //         return;
            //     // //     }
            //     // // }
            //     // // Projectile.ai[1] += 1f;
            // }
            //
            // public Terraria.NPC FindClosestNPC(float maxDetectDistance)
            // {
            //
            //     // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            //     float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
            //
            //     // Loop through all NPCs(max always 200)
            //     for (int k = 0; k < Main.maxNPCs; k++)
            //     {
            //         targett = Main.npc[k];
            //         // Check if NPC able to be targeted. It means that NPC is
            //         // 1. active (alive)
            //         // 2. chaseable (e.g. not a cultist archer)
            //         // 3. max life bigger than 5 (e.g. not a critter)
            //         // 4. can take damage (e.g. moonlord core after all it's parts are downed)
            //         // 5. hostile (!friendly)
            //         // 6. not immortal (e.g. not a target dummy)
            //         if (targett.CanBeChasedBy())
            //         {
            //             // if (targett.height * 2 < KORRO.NPC.height)
            //             // {
            //             //     SmallBoi = true;
            //             // }
            //             float sqrDistanceToTarget = Vector2.DistanceSquared(targett.Center, Projectile.Center);
            //
            //             // Check if it is within the radius
            //             if (sqrDistanceToTarget < sqrMaxDetectDistance)
            //             {
            //                 sqrMaxDetectDistance = sqrDistanceToTarget;
            //                 closestNPC = targett;
            //             }   
            //             // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
            //             
            //         }
            //     }
            //     return closestNPC;
            // }
    }
}