using System;
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
    public class DarkEnergyOrb : ModProjectile
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
            Lighting.AddLight(Projectile.Center, 0f,0f,0f);
            Terraria.NPC target = FindClosestNPC(300f);
            if (target == null)
                return;
            // If found, change the velocity of the projectile and turn it in the direction of the target
            // Use the SafeNormalize extension method to avoid NaNs returned by Vector2.Normalize when the vector is zero
            Projectile.velocity = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 20f;
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnHitNPC(Terraria.NPC target, Terraria.NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<DarkEnergyDebuff>(), 200);
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