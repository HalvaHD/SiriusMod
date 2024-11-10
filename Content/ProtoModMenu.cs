using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace ProtoMod.Content
{
	public class ProtoModMenu : ModMenu
	{
		private const string menuAssetPath = "ProtoMod/Assets/Textures/Menu";
			// TODO: According to playthrough, needed a way to save these vars
		public bool IsHALVAMet = true;
		public Texture2D HALVAAsset => ModContent.Request<Texture2D>($"{menuAssetPath}/HALVA_Menu").Value;
		
		public bool IsZeleMet = true;
		public Texture2D ZeleAsset => ModContent.Request<Texture2D>($"{menuAssetPath}/Zele_Menu").Value;
		
		public bool IsLitosMet = true;
		public Texture2D LitosAsset => ModContent.Request<Texture2D>($"{menuAssetPath}/Litos_Menu").Value;
		
		public bool IsKORROMet = true;
		public Texture2D KORROAsset => ModContent.Request<Texture2D>($"{menuAssetPath}/KORRO_Menu").Value;
		public bool IsChloreMet = true;
		public Texture2D ChloreAsset => ModContent.Request<Texture2D>($"{menuAssetPath}/Chlore_Menu").Value;
	
		public static Asset<Texture2D> FoggyNoise { get; private set; }

		
		public override Asset<Texture2D> Logo => ModContent.Request<Texture2D>($"{menuAssetPath}/Logo");

		public override Asset<Texture2D> SunTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/BlankPixel");

		public override Asset<Texture2D> MoonTexture => ModContent.Request<Texture2D>($"{menuAssetPath}/BlankPixel");

		public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/LostInTechnoAbyss");

		public override ModSurfaceBackgroundStyle MenuBackgroundStyle => ModContent.GetInstance<NullBackground>();
		
		public static MiscShaderData FogFog => GameShaders.Misc["Protogenesis:FogOverlay"];
		
		public static Asset<Texture2D> Pixel { get; private set; }
		internal static readonly FieldInfo UImageFieldMisc0 = typeof (MiscShaderData).GetField("_uImage0", BindingFlags.Instance | BindingFlags.NonPublic);
		internal static readonly FieldInfo UImageFieldMisc1 = typeof (MiscShaderData).GetField("_uImage1", BindingFlags.Instance | BindingFlags.NonPublic);


		public override string DisplayName => "ProtoGenesis Style";

		public int MoveCount;
		public int Count;
		public float FogScale;
		public Vector2 FogPosition;

		public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
		{
			Texture2D texture2D = ModContent.Request<Texture2D>("ProtoMod/Assets/Textures/Backgrounds/ProtogenesisBackground_Static").Value;
				FoggyNoise = ModContent.Request<Texture2D>("ProtoMod/Assets/ExtraTextures/FoggyNoise", AssetRequestMode.ImmediateLoad);

			Vector2 zero = Vector2.Zero;
			float num1 = Main.screenWidth / (float) texture2D.Width;
			float num2 = Main.screenHeight / (float) texture2D.Height;
			float num3 = num1;
			if (num1 != (double)num2)
			{
				if (num2 > (double)num1)
				{
					num3 = num2;
					zero.X -= (float)((texture2D.Width * (double)num3 - Main.screenWidth) * 0.5);
				}
				else
					zero.Y -= (float)((texture2D.Height * (double)num3 - Main.screenHeight) * 0.5);
			}

			spriteBatch.Draw(texture2D, zero, new Rectangle?(), Color.White, 0.0f, Vector2.Zero, num3, 0, 0.0f);
			Main.dayTime = false;
			Main.time = 27000;
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
			Texture2D pixel = Pixel.Value;
			Vector2 scale = new Vector2(Main.screenWidth, Main.screenHeight) / pixel.Size() * 1.1f;

			spriteBatch.EnterShaderRegion();

			FogFog.Apply();
			FogFog.SetShaderTexture(FoggyNoise);
			spriteBatch.Draw(pixel, new Vector2(zero.X + 880, zero.Y + 336), null, new Color(0,139,139, 255), 0f, pixel.Size() * 0.5f, scale, 0, 0f);
			spriteBatch.ExitShaderRegion();
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
			Vector2 drawPos = new(Main.screenWidth * 0.5f, 100f);
			float interpolant = (1f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.5f)) * 0.5f;
			logoScale = MathHelper.Lerp(0.75f, 0.85f, interpolant);
			spriteBatch.Draw(Logo.Value, drawPos, null, Color.White, logoRotation, Logo.Value.Size() * 0.5f, logoScale, SpriteEffects.None, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
			if (IsZeleMet)
			{
				spriteBatch.Draw(ZeleAsset, new Vector2(zero.X + 609 , zero.Y + 730), new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 0.4f, 0, 0.0f);
			}
			if (IsLitosMet)
			{
				spriteBatch.Draw(LitosAsset, new Vector2(zero.X + 480 , zero.Y + 640), new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 0.4f, 0, 0.0f);
			}
			if (IsChloreMet)
			{
				spriteBatch.Draw(ChloreAsset, new Vector2(zero.X + 970 , zero.Y + 675), new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 0.4f, 0, 0.0f);
			}

			if (IsHALVAMet)
			{
				spriteBatch.Draw(HALVAAsset, new Vector2(zero.X + 1060 , zero.Y + 786), new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 0.4f, 0, 0.0f);
			}
			if (IsKORROMet)
			{
				spriteBatch.Draw(KORROAsset, new Vector2(zero.X + 1100 , zero.Y + 786), new Rectangle?(), Color.White, 0.0f, Vector2.Zero, 0.4f, 0, 0.0f);
			}

			return false;
		}

		public override void Unload()
		{
			FoggyNoise = null;
			Pixel = null;
		}

		public override void Load()
		{
			var fog = Mod.Assets.Request<Effect>("Assets/Effects/FogShader", AssetRequestMode.ImmediateLoad);
			GameShaders.Misc["Protogenesis:FogOverlay"] = new MiscShaderData(fog, "FogPass");
			Pixel = ModContent.Request<Texture2D>("ProtoMod/Assets/ExtraTextures/Pixel", AssetRequestMode.ImmediateLoad);
		}
		
	
	}

	public static class MenuUtils
	{
		public static void EnterShaderRegion(
			this SpriteBatch spriteBatch,
			BlendState newBlendState = null,
			Effect effect = null)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, effect, Main.UIScaleMatrix);
		}

		public static void ExitShaderRegion(this SpriteBatch spriteBatch)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect) null, Main.UIScaleMatrix);
		}

		public static MiscShaderData SetShaderTexture(
			this MiscShaderData shader,
			Asset<Texture2D> texture,
			int index = 1)
		{
			switch (index)
			{
				case 0:
					ProtoModMenu.UImageFieldMisc0.SetValue(shader, texture);
					break;
				case 1:
					ProtoModMenu.UImageFieldMisc1.SetValue(shader, texture);
					break;
			}

			return shader;
		}
	}
}
