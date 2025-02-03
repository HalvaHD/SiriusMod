using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Content.Currencies;
using ProtoMod.Content.Items;
using ProtoMod.Content.Projectiles;
using ReLogic.Content;
using Terraria.GameContent.UI;
using Terraria.ModLoader;

namespace ProtoMod
{
	public class ProtoMod : Mod
	{
		public static string EffectsPath => "ProtoMod/Assets/Effects";
		public static string ExtraTexturesPath => "ProtoMod/Assets/ExtraTextures";
		public static string MusicPath => "ProtoMod/Assets/Music";
		public static string SoundsPath => "ProtoMod/Assets/Sounds";
		public static string TexturesPath => "ProtoMod/Assets/Textures";
		
		
		
		
		
		public static int CosmicCryCurrencyID;
		public static int CosmicCryCurrencyID2;
		public static int CosmicCryCurrencyID5;
		public static List<int> CheckKilledBosses = new ();
		
		public static Effect SpriteRotation;

		public readonly List<Effect> Effects = new()
		{
			SpriteRotation
		};
			

		public override void Load()
		{
			SpriteRotation = ModContent.Request<Effect>($"{EffectsPath}/SpriteRotation", AssetRequestMode.ImmediateLoad).Value;
		}

		public override void PostSetupContent()
		{
			base.PostSetupContent();
		}

		public override void Unload()
		{
			base.Unload();
		}
	}
}