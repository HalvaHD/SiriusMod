using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SiriusMod.UI;
using SiriusMod.UI.Mechanics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace SiriusMod.Systems
{
    public class UIManageSystem : ModSystem
    {
        // public static Vector2 PreviousMouseWorld;
        //
        // public static Vector2 PreviousZoom;

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int buffDisplayIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Resource Bars");
            if (buffDisplayIndex != -1)
            {
            }

            int Index = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
            if (Index != -1)
            {

                // Overheat UI Bar
                layers.Insert(Index, new LegacyGameInterfaceLayer("Overheat UI", () =>
                {
                    OverheatUI.Draw(Main.spriteBatch, Main.LocalPlayer);
                    return true;
                }, InterfaceScaleType.None));
                
                // Gameraiders101 UI
                layers.Insert(Index, new LegacyGameInterfaceLayer("Pashalko UI", () =>
                {
                    PashalkoUI.Draw(Main.spriteBatch, Main.LocalPlayer);
                    return true;
                }, InterfaceScaleType.None));
            }


            int invasionIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Diagnose Net");
            if (invasionIndex != -1)
            {
            }
        }
    }
}