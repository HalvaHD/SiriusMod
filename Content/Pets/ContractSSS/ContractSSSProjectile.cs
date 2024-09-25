using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Twig.Content.Pets.CatX;

namespace Twig.Content.Pets.ContractSSS
{
    public class ContractSSSProjectile : ModProjectile
    {
        public Vector2 IdlePosition;
        public bool Isflying = false;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 9;
            Main.projPet[Projectile.type] = true;

            // This code is needed to customize the vanity pet display in the player select screen. Quick explanation:
            // * It uses fluent API syntax, just like Recipe
            // * You start with ProjectileID.Sets.SimpleLoop, specifying the start and end frames aswell as the speed, and optionally if it should animate from the end after reaching the end, effectively "bouncing"
            // * To stop the animation if the player is not highlighted/is standing, as done by most grounded pets, add a .WhenNotSelected(0, 0) (you can customize it just like SimpleLoop)
            // * To set offset and direction, use .WithOffset(x, y) and .WithSpriteDirection(-1)
            // * To further customize the behavior and animation of the pet (as its AI does not run), you have access to a few vanilla presets in DelegateMethods.CharacterPreview to use via .WithCode(). You can also make your own, showcased in MinionBossPetProjectile
            ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] = ProjectileID.Sets.SimpleLoop(1, 1, 1)
               .WhenNotSelected(0, 0).WithOffset(5f, 0f)
                .WithSpriteDirection(-1);

        }

        public override void SetDefaults()
        {
            Projectile.height = 30;
            Projectile.width = 29;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 2;
            Projectile.scale = 0.8f;
            DrawOriginOffsetY -= 27;
            DrawOffsetX -= 18;
        }

        public override bool PreAI()
        {
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return true;
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            Player player = Main.player[Projectile.owner];
            if (Isflying == true)
            {
                Projectile.aiStyle = -1;
                Projectile.tileCollide = false;

                if (Projectile.velocity.X > 0)
                {
                    Projectile.spriteDirection = -1;
                }
                else
                {
                    Projectile.spriteDirection = 1;
                }

                if (Projectile.velocity.X == 0)
                {
                    Projectile.frame = 0;
                }

                if (Projectile.frame < 4)
                {
                    if (Projectile.frameCounter % 10 == 0)
                    {
                        Projectile.frame++;
                    }
                }
                else
                {
                    Projectile.frame = 1;
                    Projectile.frameCounter = 0;
                }

                float speed = 10;
                float inertia = 1;
                Vector2 position = IdlePosition - Projectile.Center;
                float distanceToIdlePosition =
                    (new Vector2(player.Center.X + 32 * player.direction, player.Center.Y - 32) -
                     Projectile.Center).Length();
                if (distanceToIdlePosition > 600f)
                {
                    speed = 12f;
                    inertia = 60f;
                }
                else
                {
                    speed = 8f;
                    inertia = 100f;
                }

                if (distanceToIdlePosition > 10f)
                {
                    position.Normalize();
                    position *= speed;
                    Projectile.velocity = (Projectile.velocity * (inertia - 1) + position) / inertia;
                }
                else if (Projectile.velocity == Vector2.Zero)
                {
                    Projectile.frame = 0;
                }
            }

            if (Collision.TileCollision(player.position, player.velocity, player.width, player.height, true, false,
                (int)player.gravDir).Y == 0f)
            {
                Isflying = false;
                Projectile.aiStyle = 26;
                AIType = ProjectileID.FennecFox;

            }
            else
            {
                Isflying = true;
                if (player.direction == 1)
                {
                    IdlePosition = new Vector2(player.Center.X + 14, player.Center.Y);

                }
                else
                {
                    IdlePosition = new Vector2(player.Center.X - 14, player.Center.Y);
                }
            }

            if (!player.dead && player.HasBuff(ModContent.BuffType<ContractSSSBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            if (Projectile.velocity.X != 0 && Isflying == false)
            {
                if (Projectile.frame < 8)
                {
                    if (Projectile.frameCounter % 10 == 0)
                    {
                        Projectile.frame++;
                    }
                }
                else
                {
                    Projectile.frame = 2;
                    Projectile.frameCounter = 0;
                }
            }
            else if (Isflying == false)
            {
                Projectile.frame = 0;
            }

        }
    }
}

