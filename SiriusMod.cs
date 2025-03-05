using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Content.Sources;
using SiriusMod.Core;
using Terraria.ModLoader;

namespace SiriusMod
{
	public partial class SiriusMod : Mod
	{

		public static SiriusMod Instance { get; private set; }
		public static string EffectsPath => "SiriusMod/Assets/Effects";
		public static string ExtraTexturesPath => "SiriusMod/Assets/ExtraTextures";
		public static string MusicPath => "SiriusMod/Assets/Music";
		public static string SoundsPath => "SiriusMod/Assets/Sounds";
		public static string TexturesPath => "SiriusMod/Assets/Textures";

		public static Effect SpriteRotation;
		
		public static List<int> CheckKilledBosses = new ();

		public SiriusMod()
		{
			Instance = this;
		}
			

		public override void Load()
		{
			SpriteRotation =  ModContent.Request<Effect>($"{EffectsPath}/SpriteRotation", AssetRequestMode.ImmediateLoad).Value;
		}

        // public override IContentSource CreateDefaultContentSource()
        // {
        //     PathRedirectContentSource contentSource = new PathRedirectContentSource(base.CreateDefaultContentSource());
        //     contentSource.MapDirectory("Content", "Assets");
        //     return contentSource;
        // } // Doesn't required as for now

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