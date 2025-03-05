using SiriusMod.Content.Items;
using SiriusMod.Mechanics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using ReLogic.Content;
using SiriusMod.Content.Items.Tools.PreHM.PathfinderPickaxe;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Animations;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace SiriusMod.UI.Mechanics;

internal class OverheatScale : UIState
{
    private UIElement area;
    private UIImage frame;
    private UIImage progressBar;

    public override void OnInitialize()
    {
        // Vector2 screenPos = Main.LocalPlayer.Center - Main.screenPosition;
        area = new UIElement();
        area.Left.Set(-18, 0.5f);
        area.Top.Set(0, 0.55f);
        area.Width.Set(36, 0f);
        area.Height.Set(12, 0f);

        frame = new UIImage(ModContent.Request<Texture2D>("SiriusMod/Assets/ExtraTextures/Kitkat"));
        frame.Left.Set(0, 0f);
        frame.Top.Set(0, 0f);
        frame.Width.Set(36, 2f);
        frame.Height.Set(12, 2f);
        
        progressBar = new UIImage(ModContent.Request<Texture2D>("SiriusMod/Assets/ExtraTextures/Kitkat_Bar"));
        
        area.Append(frame);
        // area.Append(progressBar);
        Append(area);
    }

    public override void Draw(SpriteBatch spriteBatch)
    { 
        if (Main.LocalPlayer.HeldItem.ModItem is not Overheat)  // && !Main.LocalPlayer.controlUseItem
        {
            return;
        }
        
        base.Draw(spriteBatch);
    }
    
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
			
    }

    [Autoload(Side = ModSide.Client)]
    internal class OverheatScaleUISystem : ModSystem
    {
        private UserInterface OverheatScaleUserInterface;

        internal OverheatScale ExampleResourceBar;

        public override void Load()
        {
            ExampleResourceBar = new();
            OverheatScaleUserInterface = new();
            OverheatScaleUserInterface.SetState(ExampleResourceBar);

            string category = "UI";
        }

        public override void UpdateUI(GameTime gameTime)
        {
            OverheatScaleUserInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));
            if (resourceBarIndex != -1)
            {
                layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                    "ExampleMod: Example Resource Bar",
                    delegate
                    {
                        OverheatScaleUserInterface.Draw(Main.spriteBatch, new GameTime());
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
