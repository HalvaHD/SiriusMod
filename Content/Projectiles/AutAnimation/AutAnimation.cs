using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ProtoMod.Content.Projectiles.AutAnimation
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
				SoundStyle style = new SoundStyle("ProtoMod/Assets/Sounds/AncientPortal") { Volume = 10f };
				SoundEngine.PlaySound(style);
				/*for (int i = 0; i < 300; i++)
				{
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position = Main.rand.NextVector2CircularEdge(2f, 1.8f);
					Vector2 spawncenter = new Vector2(Projectile.position.X + 13*16, Projectile.position.Y + 190);
						
					var dust = Dust.NewDustPerfect(spawncenter + position, 27, position * 5, 150, new Color(255,0,0), 2f);
					
					dust.shader = GameShaders.Armor.GetSecondaryShader(46, Main.LocalPlayer);

				}*/
				// for (int i = 0; i < 300; i++)
				// {
				// 	// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
				// 	Vector2 position = Main.rand.NextVector2CircularEdge(1.6f, 1.4f);
				// 	Vector2 spawncenter = new Vector2(Projectile.position.X + 12.5f*16, Projectile.position.Y + 200);
				// 		
				// 	var dust = Dust.NewDustPerfect(spawncenter + position, 27, position * 5, 50, new Color(255,0,0), 2f);
				// 	
				// 	dust.shader = GameShaders.Armor.GetSecondaryShader(46, Main.LocalPlayer);
				//
				// }
				/*for (int i = 0; i < 300; i++)
				{
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position = Main.rand.NextVector2CircularEdge(1.6f, 1.4f);
					Vector2 spawncenter = new Vector2(Projectile.position.X + 13*16, Projectile.position.Y + 190);
						
					var dust = Dust.NewDustPerfect(spawncenter + position, 27, position * 5, 0, new Color(255,0,0), 2f);
						
					dust.shader = GameShaders.Armor.GetSecondaryShader(46, Main.LocalPlayer);

				}
				for (int i = 0; i < 300; i++)
				{
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position = Main.rand.NextVector2CircularEdge(1.5f, 0.5f);
					Vector2 spawncenter = new Vector2(Projectile.position.X + 13*16, Projectile.position.Y + 190);
						
					var dust = Dust.NewDustPerfect(spawncenter + position, 27, position * 5, 0, Color.Black, 2f);
					
					dust.shader = GameShaders.Armor.GetSecondaryShader(46, Main.LocalPlayer);

				}
				/*for (int i = 0; i < 300; i++)
				{
					// You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
					Vector2 position = Main.rand.NextVector2Circular(0.1f,1.6f);
					Vector2 spawncenter = new Vector2(Projectile.position.X + 13*16, Projectile.position.Y + 190);
						
					var dust = Dust.NewDustPerfect(spawncenter, 27, position, 255, new Color(255,255,255));
					
					dust.shader = GameShaders.Armor.GetSecondaryShader(46, Main.LocalPlayer);

				}*/
				
				Projectile.NewProjectile(new EntitySource_Film(), Projectile.Center, new Vector2(0),
					ModContent.ProjectileType<AutPortal.AutPortal>(), 0, 0);	
					
				
			}
		}
	}
}