using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary.Core;
using SiriusMod.Content.Projectiles;
using Terraria;
using Terraria.ModLoader;

namespace SiriusMod.Content.Bosses.Protector
{
	public class ProtectorParticle2 : ParticleLibrary.Core.Particle
	{
		public int timer = Main.rand.Next(50, 100);
		public float mult = Main.rand.NextFloat(10f, 31f) / 200f;
		public int timeLeftMax;
		public float size = 0f;
		public Projectile Owner;
		public Vector2 InitPos;

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
		public ProtectorParticle2(int timeLeft, float velocityMult = 0.99f)
		{
			TimeLeft = timeLeft;
		}

	/// <summary>
		/// This parameterless constructor allows us to use our particle in the NewParticle(T) methods without errors
		/// It's a good idea to provide default values for your parameter constructor unless it's not necessary
		/// </summary>
		public ProtectorParticle2() : this(30, 0.99f) { }

		/// <summary>
		/// Runs when the particle is created
		/// </summary>
		public override void Spawn()
		{
			timeLeftMax = TimeLeft;
			size = Main.rand.NextFloat(5f, 11f) / 10f;
			foreach (var proj in Main.ActiveProjectiles)
			{
				if (proj.type == ModContent.ProjectileType<ProtectorBlast>())
				{
					Owner = proj;
					
				}
			}

			if (Owner != null)
			{
				InitPos = Owner.Center;
			}
		}

		/// <summary>
		/// Runs every full frame (tick)
		/// </summary>
		public override void Update()
		{
			Scale = (TimeLeft / 20f) * 3;
			if (Owner != null)
			{
				Vector2 DrawPos = Owner.Center - InitPos;
				Position = Position + (Owner.Center - InitPos) * 0.12f;
			}
		}

		/// <summary>
		/// Runs every draw frame (interval depends on <see cref="Main.FrameSkipMode"/>)
		/// </summary>
		/// <param name="spriteBatch">The SpriteBatch to use.</param>
		/// <param name="location">The visual location, already taking into account <see cref="Main.screenPosition"/></param>
		public override void Draw(SpriteBatch spriteBatch, Vector2 location)
		{
			Texture2D ember = ModContent.Request<Texture2D>("SiriusMod/Content/Bosses/Protector/ProtectorParticle2").Value;

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
	}
}