using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace SiriusMod.Content.Projectiles
{
	// This file shows an animated projectile
	// This file also shows advanced drawing to center the drawn projectile correctly
	public class AutAnimation : ModProjectile
	{
		public bool InstancePerEntity => true;
		public bool CanSpawnThem = false;
        public static bool AutCanSpawn = true;

		public override void SetStaticDefaults()
		{
			// Total count animation frames
			Main.projFrames[Projectile.type] = 5;
		}

		// public override Color? GetAlpha(Color lightColor)
		// {
		// 	return new Color(0, 0, 255);
		// }

		public override void SetDefaults()
		{
			Projectile.width = 128; // The width of projectile hitbox
			Projectile.height = 132; // The height of projectile hitbox
			Projectile.friendly = false; // Can the projectile deal damage to enemies?
			Projectile.ignoreWater = true; // Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false; // Can the projectile collide with tiles?
			Projectile.timeLeft = 300;
			Projectile.scale = 3f;
			Projectile.aiStyle = -1;
			DrawOriginOffsetX = -95;
			DrawOriginOffsetY = 190;

		}

		// Allows you to determine the color and transparency in which a projectile is drawn
		// Return null to use the default color (normally light and buff color)
		// Returns null by default.

		public override void AI()
		{
			if (Projectile.active)
			{
				AutCanSpawn = false;
			}
			if (++Projectile.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				if (++Projectile.frame >= Main.projFrames[Projectile.type])
				{
					Projectile.frame = 0;
				}
			}

			Lighting.AddLight(Projectile.Center, 1f, 0.1f,
				0.3f); // R G B values from 0 to 1f. This is the red from the Crimson Heart pet

			if (Projectile.timeLeft == 240)
			{
				Projectile.velocity = new Vector2(0);
			}
			if (Projectile.timeLeft == 1)
			{
				SoundStyle style = new SoundStyle("SiriusMod/Assets/Sounds/AncientPortal") { Volume = 10f };
				SoundEngine.PlaySound(style);
				Projectile.NewProjectile(new EntitySource_Film(), Projectile.Center, new Vector2(0),
					ModContent.ProjectileType<AutPortal>(), 0, 0);	
					
				
			}
		}
	}
}