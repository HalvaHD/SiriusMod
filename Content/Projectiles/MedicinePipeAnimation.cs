using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace ProtoMod.Content.Projectiles
{
	// This file shows an animated projectile
	// This file also shows advanced drawing to center the drawn projectile correctly
	public class MedicinePipeAnimation : ModProjectile
	{
		public static int Frame = 0;

		public override void SetStaticDefaults()
		{
			// Total count animation frames
			Main.projFrames[Projectile.type] = 18;
		}

		public override void SetDefaults()
		{
			Projectile.width = 64; // The width of projectile hitbox
			Projectile.height = 64; // The height of projectile hitbox
			Projectile.friendly = false; // Can the projectile deal damage to enemies?
			Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
			Projectile.tileCollide = false; // Can the projectile collide with tiles?
			Projectile.timeLeft = 210;
			Projectile.scale = 0.74f;
			Projectile.aiStyle = -1;

		}

		// Allows you to determine the color and transparency in which a projectile is drawn
		// Return null to use the default color (normally light and buff color)
		// Returns null by default.

		public override void AI()
		{
			if (Projectile.timeLeft < 170)
			{
				if (Projectile.timeLeft == 169)
                {
                	if (Main.LocalPlayer.direction == 1)
                	{
                		Projectile.rotation += 0.25f;
                	}
                	if (Main.LocalPlayer.direction == -1)
                	{
                		Projectile.rotation -= 0.25f;
                	}
                	
                }
                if (++Projectile.frameCounter % 5 == 0)
                {
                	Projectile.frame++;
                }
    
                if (Projectile.timeLeft < 165)
                {
	                if (Projectile.timeLeft == 164)
	                {
		                SoundStyle style = new SoundStyle("Twig/Assets/Sounds/MedicinePipeSweetWoosh");
		                SoundEngine.PlaySound(style);
	                }
                	if (Main.LocalPlayer.direction == -1)
                	{
                		Projectile.position.X = Main.LocalPlayer.Center.X - 3.1f * 16;
                	}
                	if (Main.LocalPlayer.direction == 1)
                	{
                		Projectile.position.X = Main.LocalPlayer.Center.X - 0.8f * 16;
                	}
                	
                	// if (Main.LocalPlayer.direction == -1)
                	// {
                	// 	Projectile.position.X = Main.LocalPlayer.position.X - 2f * 16;
                	// }
                	Projectile.position.Y = Main.LocalPlayer.Center.Y - 2.9f *16;
                }
    
                if (Main.LocalPlayer.direction == -1)
                {
                	Projectile.spriteDirection = -1;
                	if (Projectile.rotation == 0.25f)
                	{
                		Projectile.rotation -= 0.5f;
                	}
                }
                else
                {
                	Projectile.spriteDirection = 1;
                	if (Projectile.rotation == -0.25f)
                	{
                		Projectile.rotation += 0.5f;
                	}
                }
                
                
                Lighting.AddLight(Projectile.Center, 0.19f, 0.8f,
                	0.19f); // R G B values from 0 to 1f. This is the red from the Crimson Heart pet
                
				
			}

		}
	}
}