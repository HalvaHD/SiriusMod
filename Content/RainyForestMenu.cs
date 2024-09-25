using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace Twig.Content
{
	public class ExampleModMenu : ModMenu
	{
		private const string menuAssetPath = "Twig/Assets/Textures/Menu"; // Creates a constant variable representing the texture path, so we don't have to write it out multiple times

		public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>($"{menuAssetPath}/Logo");

		public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/BlankPixel");

		public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/BlankPixel");

		public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/UndertheRain");

		public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<NullBackground>();

		public override string DisplayName => "Rainy Forest Style";

		public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
		{
			logoScale = 0.20f;
			Texture2D texture2D = ModContent.Request<Texture2D>("Twig/Assets/Textures/Backgrounds/RainyBackground").Value;
			Vector2 zero = Vector2.Zero;
			float num1 = Main.screenWidth / (float) texture2D.Width;
			float num2 = Main.screenHeight / (float) texture2D.Height;
			float num3 = num1;
			// ReSharper disable once CompareOfFloatsByEqualityOperator
			if (num1 != (double) num2)
			{
				if (num2 > (double) num1)
				{
					num3 = num2;
					zero.X -= (float) ((texture2D.Width * (double) num3 - Main.screenWidth) * 0.5);
				}
				else
					zero.Y -= (float) ((texture2D.Height * (double) num3 - Main.screenHeight) * 0.5);
			}
			spriteBatch.Draw(texture2D, zero, new Rectangle?(), Color.White, 0.0f, Vector2.Zero, num3, 0, 0.0f);
			Main.dayTime = true;
			Main.time = 27000;
			return true;
		}
	}
}
