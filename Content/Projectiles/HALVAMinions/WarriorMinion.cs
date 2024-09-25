using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Content.NPC;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Projectiles.HALVAMinions
{
	// This file contains all the code necessary for a minion
	// - ModItem - the weapon which you use to summon the minion with
	// - ModBuff - the icon you can click on to despawn the minion
	// - ModProjectile - the minion itself

	// It is not recommended to put all these classes in the same file. For demonstrations sake they are all compacted together so you get a better overview.
	// To get a better understanding of how everything works together, and how to code minion AI, read the guide: https://github.com/tModLoader/tModLoader/wiki/Basic-Minion-Guide
	// This is NOT an in-depth guide to advanced minion AI
	
	// This minion shows a few mandatory things that make it behave properly.
	// Its attack pattern is simple: If an enemy is in range of 43 tiles, it will fly to it and deal contact damage
	// If the player targets a certain NPC with right-click, it will fly through tiles to it
	// If it isn't attacking, it will float near the player with minimal movement
	public class WarriorMinion : ModProjectile
	{
		public bool foundTarget;
		public Vector2 targetCenter;
		public bool CanDropBomb;
		public bool CanbeDropped;
		public Terraria.NPC owner;
		public override void SetStaticDefaults() {
			// Sets the amount of frames this minion has on its spritesheet
			Main.projFrames[Projectile.type] = 1; ; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
			ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
		}

		public sealed override void SetDefaults() {
			Projectile.width = 32;
			Projectile.height = 18;
			Projectile.tileCollide = false; // Makes the minion go through tiles freely
			// These below are needed for a minion weapon
			Projectile.friendly = true; // Only controls if it deals damage to enemies on contact (more on that later)
			// Projectile.minion = true; // Declares this as a minion (has many effects)
			Projectile.DamageType = DamageClass.Ranged; // Declares the damage type (needed for it to deal damage)
			Projectile.penetrate = -1; // Needed so the minion doesn't despawn on collision with enemies or tiles
			Projectile.timeLeft = 600;
		}
		
		public override bool PreDraw(ref Color lightColor)
        {
	        Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
	        Texture2D textureBomb = ModContent.Request<Texture2D>("Twig/Content/Projectiles/HALVAMinions/NUCLEARPOWER", AssetRequestMode.ImmediateLoad).Value;
            int num156 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * Projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            int num1566 = TextureAssets.Projectile[ModContent.ProjectileType<NUCLEARPOWER>()].Value.Height; //ypos of lower right corner of sprite to draw
            int y36 = num156 * Projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle2 = new(0, y36, textureBomb.Width, num1566);
            Vector2 origin22 = rectangle.Size() / 2f;

            SpriteEffects effects = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Color color26 = lightColor;
            Projectile.GetAlpha(color26);

            float rotationOffset = Projectile.spriteDirection > 0 ? 0 : (float)Math.PI / 2;

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);

            Main.EntitySpriteDraw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rectangle, Projectile.GetAlpha(lightColor), Projectile.rotation + rotationOffset, origin2, Projectile.scale, effects);
            Texture2D texture = ModContent.Request<Texture2D>("Twig/Content/Projectiles/HALVAMinions/WarriorMinion_Glowmask", AssetRequestMode.ImmediateLoad).Value;
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), rectangle, Color.White * Projectile.Opacity, Projectile.rotation + rotationOffset, origin2, Projectile.scale, effects);
            if (Main.hardMode)
            {
	            Main.EntitySpriteDraw(textureBomb, new Vector2((Projectile.Center - Main.screenPosition +
	                                                            new Vector2(0f, Projectile.gfxOffY)).X - 10, ((Projectile.Center - Main.screenPosition +
		            new Vector2(0f, Projectile.gfxOffY)).Y + 10)), rectangle2, Color.White * Projectile.Opacity, Projectile.rotation + rotationOffset, origin22, Projectile.scale, effects); 
            }
            return false;
        }
		public override void AI() {
			if(Projectile.timeLeft == 599)	
			{
				Projectile.ai[1] = 0;
			}
			Terraria.NPC owner = ModContent.GetModNPC(ModContent.NPCType<HALVA>()).NPC;
			if (owner.active)
			{
				Projectile.timeLeft = 2;
			}

			GeneralBehavior(out Vector2 vectorToIdlePosition, out float distanceToIdlePosition);
			Terraria.NPC Targett = FindClosestNPC(300);
			if (Targett != null)
			{
				foundTarget = true;
				targetCenter = Targett.Center;
			}
			else
			{
				foundTarget = false;
			}

			if (!Main.hardMode)
			{
				Movement(foundTarget , targetCenter, distanceToIdlePosition, vectorToIdlePosition);
			}
			else
			{
				MovementHardmode(foundTarget , targetCenter, distanceToIdlePosition, vectorToIdlePosition);
			}
			
			Visuals();
			if (!Main.hardMode)
			{
				Terraria.NPC target = FindClosestNPC(200);
				if (target != null)
				{
					Vector2 velocity = ((target.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 50f).RotatedByRandom(MathHelper.ToRadians(10));
					if (Projectile.timeLeft % 3 == 0)
						Projectile.NewProjectile(new EntitySource_Misc("HALVAGatling"), Projectile.Center, velocity,
							ProjectileID.Bullet, 8, 0.1f);
				}
			}
			else
			{
				if (CanDropBomb)
				{
					if (DateTime.Now.Month == 4 && DateTime.Now.Day == 1)
					{
						CombatText.NewText(Projectile.Hitbox, Color.IndianRed, "Legalize Nuclear Bombs", true);

					}
					CanDropBomb = false;
					Projectile.NewProjectile(new EntitySource_Misc("NUCLEARR"), Projectile.Center,
						Vector2.Zero, ModContent.ProjectileType<NUCLEARPOWER>(), 0, 0);
					Projectile.Kill();
				}
			}
		}

		// This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not

		private void GeneralBehavior(out Vector2 vectorToIdlePosition, out float distanceToIdlePosition) {
			foreach (var npc in Main.ActiveNPCs)
			{
				if (npc.type == ModContent.NPCType<HALVA>())
				{
					owner = npc;
				}
			}
			Vector2 idlePosition = owner.Center;
			idlePosition.Y -= 66f; // Go up 32 coordinates (three tiles from the center of the player)

			// // If your minion doesn't aimlessly move around when it's idle, you need to "put" it into the line of other summoned minions
			// // The index is projectile.minionPos
			// float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -owner.direction;
			// idlePosition.X += minionPositionOffsetX; // Go behind the player

			// All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

			// Teleport to player if distance is too big
			vectorToIdlePosition = idlePosition - Projectile.Center;
			distanceToIdlePosition = vectorToIdlePosition.Length();
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
		            float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);
		
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

		public override bool? CanHitNPC(Terraria.NPC target)
		{
			return false;
		}

		private void Movement(bool foundTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition) {
			// Default movement parameters (here for attacking)
			float speed = 8f;
			float inertia = 20f;

			if (foundTarget) {
				// Minion has a target: attack (here, fly towards the enemy)
				// The immediate range around the target (so it doesn't latch onto it when close)
				Vector2 direction = new Vector2(targetCenter.X, targetCenter.Y - 48) - Projectile.Center;
				direction.Normalize();
				direction *= speed;

				Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
				
			}
			else {
				// Minion doesn't have a target: return to player and idle
				if (distanceToIdlePosition > 600f) {
					// Speed up the minion if it's away from the player
					speed = 12f;
					inertia = 60f;
				}
				else {
					// Slow down the minion if closer to the player
					speed = 4f;
					inertia = 80f;
				}

				if (distanceToIdlePosition > 20f) {
					// The immediate range around the player (when it passively floats about)

					// This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
					vectorToIdlePosition.Normalize();
					vectorToIdlePosition *= speed;
					Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
				}
				else if (Projectile.velocity == Vector2.Zero) {
					// If there is a case where it's not moving at all, give it a little "poke"
					Projectile.velocity.X = -0.15f;
					Projectile.velocity.Y = -0.05f;
				}
			}
		}
		private void MovementHardmode(bool foundTarget, Vector2 targetCenter, float distanceToIdlePosition, Vector2 vectorToIdlePosition) {
			// Default movement parameters (here for attacking)
			float speed = 8f;
			float inertia = 20f;

			if (foundTarget) {
				// Minion has a target: attack (here, fly towards the enemy)
				// The immediate range around the target (so it doesn't latch onto it when close)
				if (Math.Abs(Projectile.Center.Y - this.targetCenter.Y) > 48 && CanbeDropped == false)
				{
					Vector2 direction = new Vector2(targetCenter.X, targetCenter.Y  - 32) - Projectile.Center;
					direction.Normalize();
					direction *= speed;
					Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
				}
				else
				{
					CanbeDropped = true;
					Vector2 direction = new Vector2(targetCenter.X, targetCenter.Y - 320) - Projectile.Center;
					direction.Normalize();
					direction *= speed;
					Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
				}

				if (Math.Abs(Projectile.Center.Y - this.targetCenter.Y) > 270)
				{
					CanDropBomb = true;
				}
				
				
			}
			else {
				// Minion doesn't have a target: return to player and idle
				if (distanceToIdlePosition > 600f) {
					// Speed up the minion if it's away from the player
					speed = 12f;
					inertia = 60f;
				}
				else {
					// Slow down the minion if closer to the player
					speed = 4f;
					inertia = 80f;
				}

				if (distanceToIdlePosition > 20f) {
					// The immediate range around the player (when it passively floats about)

					// This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
					vectorToIdlePosition.Normalize();
					vectorToIdlePosition *= speed;
					Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
				}
				else if (Projectile.velocity == Vector2.Zero) {
					// If there is a case where it's not moving at all, give it a little "poke"
					Projectile.velocity.X = -0.15f;
					Projectile.velocity.Y = -0.05f;
				}
			}
		}

		private void Visuals() {
			// So it will lean slightly towards the direction it's moving
			Projectile.rotation = Projectile.velocity.X * 0.05f;

			// This is a simple "loop through all frames from top to bottom" animation
			int frameSpeed = 5;

			Projectile.frameCounter++;

			if (Projectile.frameCounter >= frameSpeed) {
				Projectile.frameCounter = 0;
				Projectile.frame++;

				if (Projectile.frame >= Main.projFrames[Projectile.type]) {
					Projectile.frame = 0;
				}
			}

			// Some visuals here
			Lighting.AddLight(Projectile.Center, Color.Transparent.ToVector3() * 0.78f);
		}
	}
}
