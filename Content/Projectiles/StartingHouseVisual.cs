using Microsoft.Xna.Framework;
using SiriusMod.Content.Items.Placeable;
using Terraria;
using Terraria.ModLoader;

namespace SiriusMod.Content.Projectiles
{
    public class StartingHouseVisual : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 416;
            Projectile.height = 448;
            Projectile.timeLeft = 10;
            Projectile.tileCollide = false;
            Projectile.Opacity = 0.5f;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 mouse = Main.MouseWorld;

            if (player.position.X > mouse.X)
            {
                Projectile.position.X = mouse.X - Projectile.width + 4;
                Projectile.position.Y = mouse.Y - Projectile.height + 8;
            }
            else
            {
                Projectile.position.X = mouse.X - 4;
                Projectile.position.Y = mouse.Y - Projectile.height + 8;
            }

            Projectile.timeLeft++;

            if (player.HeldItem.type != ModContent.ItemType<House1>())
            {
                Projectile.Kill();
            }

            Projectile.hide = Projectile.owner != Main.myPlayer;
        }
    }
}