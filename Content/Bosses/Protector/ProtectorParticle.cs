using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary.Core;
using Terraria;
using Terraria.ModLoader;

namespace SiriusMod.Content.Bosses.Protector
{
	public class ProtectorParticle : ParticleLibrary.Core.Particle
	{
		public int timer = Main.rand.Next(50, 100);
		public float speedX = Main.rand.NextFloat(4f, 9f);
		public float mult = Main.rand.NextFloat(10f, 31f) / 200f;
		public int timeLeftMax;
		public float size = 0f;

		public float VelocityMult { get; init; }

		/// <summary>
		/// With the removal of the AI array, you can no longer pass data into a particle via <see cref="ParticleSystem.NewParticle(Vector2, Vector2, Core.Particle, Color, Vector2, Layer)"/>
		/// Instead, with recent fixes, you can instantiate the particle's constructor and pass it in that way. This way, code is much more readable.
		/// <para>
		/// NOTE: If you plan on allowing your particle to be automatically instantiated with the NewParticle(T) methods, you MUST have a parameterless constructor
		/// </para>
		/// </summary>
		/// <param name="timeLeft"></param>
		/// <param name="velocityMult"></param>
		public ProtectorParticle(int timeLeft, float velocityMult = 0.99f)
		{
			if (timeLeft <= timeLeftMax / 2f)
			{

				Opacity = MathHelper.Lerp(1f, 0f, (float)(timeLeftMax / 2f - timeLeft) / (timeLeftMax / 2f));
			}

			float sineX = (float)Math.Sin(Main.GlobalTimeWrappedHourly * speedX);

			// Makes the particle change directions or speeds.
			// Timer is used for keeping track of the current cycle
			if (timer == 0)
				NewMovementCycle();

			// Adds the wind velocity to the particle.
			// It adds less the faster it is already going.
			Velocity += new Vector2(
				Main.windSpeedCurrent * (Main.windPhysicsStrength * 3f) *
				MathHelper.Lerp(1f, 0.1f, Math.Abs(Velocity.X) / 6f), 0f);
			// Add the sine component to the velocity.
			// This is scaled by the mult, which changes every cycle.
			Velocity += new Vector2(sineX * mult, -Main.rand.NextFloat(1f, 2f) / 100f);

			// Clamp the velocity so the particle doesnt go too fast.
			Utils.Clamp(Velocity.X, -6f, 6f);
			Utils.Clamp(Velocity.Y, -6f, 6f);
		

		// Decrement the timer
			timer--;

			TimeLeft = timeLeft;
			VelocityMult = velocityMult;
			VelocityAcceleration.X = VelocityMult;
			VelocityAcceleration.Y = VelocityMult;
		}

	/// <summary>
		/// This parameterless constructor allows us to use our particle in the NewParticle(T) methods without errors
		/// It's a good idea to provide default values for your parameter constructor unless it's not necessary
		/// </summary>
		public ProtectorParticle() : this(100, 0.99f) { }

		/// <summary>
		/// Runs when the particle is created
		/// </summary>
		public override void Spawn()
		{
			timeLeftMax = TimeLeft;
			size = Main.rand.NextFloat(5f, 11f) / 10f;
		}

		/// <summary>
		/// Runs every full frame (tick)
		/// </summary>
		public override void Update()
		{
			Scale = (TimeLeft / 100f) * 2;
		}

		/// <summary>
		/// Runs every draw frame (interval depends on <see cref="Main.FrameSkipMode"/>)
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch to use.</param>
		/// <param name="location">The visual location, already taking into account <see cref="Main.screenPosition"/></param>
		public override void Draw(SpriteBatch spriteBatch, Vector2 location)
		{
			Texture2D ember = ModContent.Request<Texture2D>("SiriusMod/Content/Bosses/Protector/ProtectorParticle").Value;

			Color bright = Color.Multiply(new(240, 149, 46, 0), Opacity);
			Color mid = Color.Multiply(new(187, 63, 25, 0), Opacity);
			Color dark = Color.Multiply(new(131, 23, 37, 0), Opacity);

			Color emberColor = Color.Multiply(Color.Lerp(bright, mid, (float)(timeLeftMax - TimeLeft) / timeLeftMax), Opacity);

			float pixelRatio = 1f / 64f;
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);
			spriteBatch.Draw(ember, VisualPosition, new Rectangle(0, 0, 20, 20), Color, Rotation, new Vector2(1.5f, 1.5f), 1f * Scale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);

		}

		/// <summary>
		/// Runs when <see cref="Core.Particle.TimeLeft"/> reaches 0 or when <see cref="Core.Particle.Kill"/> is called.
		/// </summary>
		private void NewMovementCycle()
		{
			timer = Main.rand.Next(50, 100);
			speedX = Main.rand.NextFloat(3f, 10f);
			mult = Main.rand.NextFloat(10f, 31f) / 200f;
		}
    }
}