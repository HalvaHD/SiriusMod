using Terraria.ModLoader;

namespace ProtoMod.Content.Projectiles
{
    public class QIKaboomDamage : ModProjectile
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = 0;
            Projectile.penetrate = 1;
            Projectile.scale = 1f;
            Projectile.damage = 70;
            Projectile.timeLeft = 5;
        }

        public override void AI()
        {
        }
    }
}