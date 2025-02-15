using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace SiriusMod.Content.Projectiles
{
    public class YellowCrystalProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            Projectile.timeLeft = 900;
            Projectile.alpha = 120;
            DrawOffsetX -= 1;
            DrawOriginOffsetY -= 16;
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 0f,0f,0f);
            if (Projectile.Colliding(Projectile.Hitbox, Main.player[Projectile.owner].Hitbox))
            {
                
            }
        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers,
            List<int> overWiresUI)
        {
            overPlayers.Add(index);
        }
    }
}