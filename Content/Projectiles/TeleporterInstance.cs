using System.Linq;
using System.Numerics;
using Terraria;
using Terraria.ModLoader;
using Twig.Content.Items;
using Twig.Core.Systems;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Twig.Content.Projectiles
{
    public class TeleporterInstance : ModProjectile
    {
        public ref Player Owner => ref Main.player[Projectile.owner];
        public static bool GatesOpen = false;
        public static bool CanBeReOpened = true;
        public static bool TeleportUse = false;
        public static bool IsGateSet => WorldSaveSystem.TeleporterLocation != Vector2.Zero;
        public override void SetStaticDefaults()
        {
            // Total count animation frames
           Main.projFrames[Projectile.type] = 12;
        }

        public override void SetDefaults()
        {
            Projectile.width = 33; // The width of projectile hitbox
            Projectile.height = 33; // The height of projectile hitbox
            Projectile.friendly = false; // Can the projectile deal damage to enemies?
            Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.timeLeft = 7200;
            Projectile.aiStyle = -1;
            Projectile.light = 2f;
            DrawOffsetX -= 16;
            DrawOriginOffsetY -= 16;

        }
        public enum UseContext
        {
            Teleport = 0,
            Create = 1,
            Destroy = 2
        }
        
        internal void Teleport()
        {
            if (WorldSaveSystem.TeleporterLocation != Vector2.Zero && Main.projectile.Any((p) => p.active && p.owner == Main.myPlayer))
            {
                Owner.Bottom = WorldSaveSystem.TeleporterLocation;
                // Owner.velocity += Teleporter.OldVelocity;
                Owner.noFallDmg = true;
            }
            TeleportUse = false;
        }
        

        public override bool? CanDamage() => false;

        public override void AI()
        {
            if (Projectile.frame < 8 && GatesOpen)
            {
                if (++Projectile.frameCounter % 5 == 0)
                {
                    Projectile.frame++;
                }
            }
            else if (GatesOpen)
            {
                if (++Projectile.frameCounter % 6 == 0)
                {
                    Projectile.frameCounter = 0;
                    if (++Projectile.frame >= 10)
                    {
                        Projectile.frame = 8;
                    }
                }
            }
            Projectile.netUpdate = true;
            Lighting.AddLight(Projectile.Center, 0.1f, 0.3f, 0.8f); // R G B values from 0 to 1f. This is the red from the Crimson Heart pet
            if (Projectile.timeLeft < 5)
            {
                Projectile.timeLeft = 7200;
            }

            if (TeleportUse == true && Main.myPlayer == Projectile.owner)
            {
                Teleport();
            }

            if (GatesOpen == false && Main.myPlayer == Projectile.owner)
            {
                if (Projectile.frame > 0)
                {
                    if (++Projectile.frameCounter % 5 == 0)
                    {
                        Projectile.frame--;
                    }
                }
                else
                {
                    CanBeReOpened = true;
                    Projectile.Kill();
                    WorldSaveSystem.TeleporterLocation = Vector2.Zero;
                }

                
            }
        }
    }
}