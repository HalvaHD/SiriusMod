using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Pets.SlimeHero
{
    internal class SlimeHeroProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
            Main.projPet[Projectile.type] = true;

            // This code is needed to customize the vanity pet display in the player select screen. Quick explanation:
            // * It uses fluent API syntax, just like Recipe
            // * You start with ProjectileID.Sets.SimpleLoop, specifying the start and end frames aswell as the speed, and optionally if it should animate from the end after reaching the end, effectively "bouncing"
            // * To stop the animation if the player is not highlighted/is standing, as done by most grounded pets, add a .WhenNotSelected(0, 0) (you can customize it just like SimpleLoop)
            // * To set offset and direction, use .WithOffset(x, y) and .WithSpriteDirection(-1)
            // * To further customize the behavior and animation of the pet (as its AI does not run), you have access to a few vanilla presets in DelegateMethods.CharacterPreview to use via .WithCode(). You can also make your own, showcased in MinionBossPetProjectile
            ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] = ProjectileID.Sets.SimpleLoop(1, 1, 1)
               .WhenNotSelected(0, 0).WithOffset(-14f, -20f)
                .WithSpriteDirection(0);

        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ZephyrFish); // Copy the stats of the ZephyrFish
            AIType = ProjectileID.ZephyrFish; // Mimic as the ZephyrFish during AI.
            DrawOffsetX -= 12;
            DrawOriginOffsetY -= 8;
        }

        public override bool PreAI()
        {
            Player player = Main.player[Projectile.owner];



            return true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
            if (!player.dead && player.HasBuff(ModContent.BuffType<SlimeHeroBuff>()))
            {
                Projectile.timeLeft = 2;
            }
        }
    }
}