using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using SiriusMod.Common.Players;
using SiriusMod.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace SiriusMod.UI.DialogueUI
{
	// This custom UI will show whenever the player is holding the ExampleCustomResourceWeapon item and will display the player's custom resource amounts that are tracked in ExampleResourcePlayer
	internal class DialogueUI : UIState
	{
		// For this bar we'll be using a frame texture and then a gradient inside bar, as it's one of the more simpler approaches while still looking decent.
		// Once this is all set up make sure to go and do the required stuff for most UI's in the ModSystem class.
		public static UIText text;
		private UIElement area;
		private UIImage DialogueWindow;
		// private UIImage DialogueWindowText;
		private UIImage IconImage;
		public static int i;
		public static int Counter;
		public static int AnimationCounter = 1;
		public bool IsDrawnFully = false;
		public bool SkipDialouge = false;
		public SlotId TextSound; 
		public SlotId WindowSound; 
		
		public SoundStyle WindowSoundStyle = new SoundStyle("SiriusMod/Assets/Sounds/DialogueShow");

		public override void OnInitialize() {
			// Create a UIElement for all the elements to sit on top of, this simplifies the numbers as nested elements can be positioned relative to the top left corner of this element. 
			// UIElement is invisible and has no padding.
			area = new UIElement();
			area.Left.Set(-area.Width.Pixels - 1650, 1f); // Place the resource bar to the left of the hearts.
			area.Top.Set(600, 0f); // Placing it just a bit below the top of the screen.
			area.Width.Set(700, 0f); // We will be placing the following 2 UIElements within this 182x60 area.
			area.Height.Set(300, 0f);

			IconImage = new UIImage(ModContent.Request<Texture2D>("SiriusMod/UI/DialogueUI/HALVADefault")); // Frame of our resource bar
			IconImage.Left.Set(45, 0f);
			IconImage.Top.Set(25, 0f);
			IconImage.Width.Set(100, 0f);
			IconImage.Height.Set(34, 0f);
			
			// DialogueWindowText = new UIImage(ModContent.Request<Texture2D>("SiriusMod/UI/PPCDialogueWindow/Dialogue_UI_Copy")); // Frame of our resource bar
			// DialogueWindowText.Left.Set(5, 0f);
			// DialogueWindowText.Top.Set(0, 0f);
			// DialogueWindowText.Width.Set(138, 0f);
			// DialogueWindowText.Height.Set(34, 0f);
			

			text = new UIText("1111", 0.8f); // text to show stat
			text.Width.Set(0, 0f);
			text.Height.Set(34, 0f);
			text.Top.Set(70, 0f);
			text.Left.Set(200, 0f);
			

			
			DialogueWindow = new UIImage(ModContent.Request<Texture2D>("SiriusMod/UI/DialogueUI/DialogueUI")); // Frame of our resource bar
			DialogueWindow.Left.Set(5, 0f);
			DialogueWindow.Top.Set(0, 0f);
			DialogueWindow.Width.Set(138, 0f);
			DialogueWindow.Height.Set(34, 0f);
			
			area.OnLeftClick += SkipDialogue;
			area.Append(text);
			area.Append(DialogueWindow);
			Append(area);
		}
		private void SkipDialogue(UIMouseEvent evt, UIElement listeningElement) {
			if (i < DialogueSystem.DialogueLimit)
			{
				i = DialogueSystem.DialogueLimit;
				text.SetText(Wrap(DialogueSystem.DialogueLine, 40));
			}
			else
			{
				i = 0;
				Counter = 0;
				text.SetText("");
				IconImage.Remove();
				SkipDialouge = true;
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{

			if (Main.LocalPlayer.GetModPlayer<ProtoModPlayer>().DialogueShown == true)
			{
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp,
					DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);
				base.Draw(spriteBatch);
				spriteBatch.End();
				spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp,
					DepthStencilState.None, Main.Rasterizer, null, Main.UIScaleMatrix);

			}
		}
		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			
		}
		public override void Update(GameTime gameTime) {
			if (SkipDialouge == true && AnimationCounter != 1)
			{
				if (Counter % 4 == 0)
				{
					AnimationCounter--;
					DialogueWindow.SetImage(ModContent.Request<Texture2D>($"SiriusMod/UI/DialogueUI/start{AnimationCounter}"));
				}
			}
			else if (SkipDialouge == true && AnimationCounter == 1)
			{
				Main.LocalPlayer.GetModPlayer<ProtoModPlayer>().DialogueShown = false;
				SkipDialouge = false;
			}
			// Main.NewText("ААА");
			if (Main.LocalPlayer.GetModPlayer<ProtoModPlayer>().DialogueShown != true)
				return;
			IconImage.SetImage(ModContent.Request<Texture2D>($"SiriusMod/UI/DialogueUI/{DialogueSystem.DialogueImage}"));
			if (SkipDialouge != true)
			{
				if (AnimationCounter < 9)
				{
					DialogueWindow.SetImage(ModContent.Request<Texture2D>($"SiriusMod/UI/DialogueUI/start{AnimationCounter}"));
					if (Counter % 4 == 0)
					{
						AnimationCounter++;
					}

					if (AnimationCounter == 2)
					{
						if (!SoundEngine.TryGetActiveSound(WindowSound, out var activeSound))
						{
							WindowSound = SoundEngine.PlaySound(WindowSoundStyle);
						}
					}
				
				
				}
				if (AnimationCounter >= 6)
				{
					area.Append(IconImage);
				}

				Counter++;
			
				if (i <= DialogueSystem.DialogueLimit)
				{
					text.SetText(Wrap(DialogueSystem.DialogueLine.Substring(0, i), 40));
					if (Counter % DialogueSystem.DialogueSpeed == 0)
					{
						i++;
						if (!SoundEngine.TryGetActiveSound(TextSound, out var activeSound))
						{
							TextSound = SoundEngine.PlaySound(DialogueSystem.TextSound);
						}
					
					}
				}
			}
			base.Update(gameTime);
		}
		// Thank you for such cool code!!!!
		internal static string Wrap(ReadOnlySpan<char> textt, int limit)
		{
			const int MaxNewLine = 8;
			// Just try 2.275f and found it fits
			limit = (GameCulture.CultureName)Language.ActiveCulture.LegacyId switch
			{
				GameCulture.CultureName.Chinese => (int)(limit / 2.275f),
				_ => limit,
			};
			textt = textt.Trim();
			int start = 0;
			StringBuilder stringBuilder = new StringBuilder(textt.Length + MaxNewLine);
			while (limit + start < textt.Length)
			{
				var line = textt.Slice(start, limit);
				int length = line.Length, skip = 0;
				for (int i = 0; i < line.Length; i++)
				{
					if (line[i] == '\n')
					{
						length = i;
						skip = 1;
						break;
					}
					else if (char.IsWhiteSpace(line[i]))
					{
						length = i;
						skip = 1;
					}
				}
				stringBuilder.Append(line[..length]).AppendLine();
				start += length + skip;
			}
			stringBuilder.Append(textt[start..]);

			//Bad memory copy
			return stringBuilder.ToString();
		
        }
       
    
	}
	
	[Autoload(Side = ModSide.Client)]
	internal class DialogueUISystem : ModSystem
	{
		private UserInterface DialoguePPCUI;

		internal DialogueUI DialogueUI;

		public static LocalizedText ExampleResourceText { get; private set; }

		public override void Load() {
			DialogueUI = new();
			DialoguePPCUI = new();
			DialoguePPCUI.SetState(DialogueUI);

			string category = "UI";
			ExampleResourceText ??= Mod.GetLocalization($"{category}.Dialogue");
		}

		public override void UpdateUI(GameTime gameTime) {
			DialoguePPCUI?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers) {
			int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
			if (resourceBarIndex != -1) {
				layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
					"SiriusMod: DialogueUI",
					delegate {
						DialoguePPCUI.Draw(Main.spriteBatch, new GameTime());
						return true;
					},
					InterfaceScaleType.UI)
				);
			}
		}
	}
}