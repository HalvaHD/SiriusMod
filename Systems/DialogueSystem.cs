using Microsoft.Xna.Framework;
using SiriusMod.Common.Players;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SiriusMod.Systems
{
    public class DialogueSystem : ModSystem
    {
        public static int DialogueLimit = 0;
        public static string DialogueLine = "";
        public static string DialogueImage;
        public static Color DialougeColor;
        public static int DialogueSpeed = 1;
        public static SoundStyle TextSound = new SoundStyle("SiriusMod/Assets/Sounds/TextSound");
        // public static SoundStyle RoboticTextSound = new SoundStyle("SiriusMod/Assets/Sounds/TextSound_Robotic");
        public static void WriteDialogue(string character, string dialogue, Color textcolor, int dialogueSpeed, string dialogueSound)
        {
            if (Main.LocalPlayer.GetModPlayer<ProtoModPlayer>().DialogueShown == false)
            {
                DialogueLine = Language.GetTextValue($"Mods.SiriusMod.Dialogues.{dialogue}");
                DialogueLimit = DialogueLine.Length;
                DialogueImage = character;
                DialougeColor = textcolor;
                DialogueSpeed = dialogueSpeed;
                TextSound = new SoundStyle($"SiriusMod/Assets/Sounds/{dialogueSound}");
                Main.LocalPlayer.GetModPlayer<ProtoModPlayer>().DialogueShown = true;
            }
            
        }

        
    }
    
}
