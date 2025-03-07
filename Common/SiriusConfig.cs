using System.ComponentModel;
using SiriusMod.Mechanics;
using SiriusMod.UI.Mechanics;
using Terraria.ModLoader.Config;

namespace SiriusMod.Common
{
    // TODO: Add background [BackgroundColor(192, 54, 64, 192)] and slidecolor [SliderColor(224, 165, 56, 128)]
    public class SiriusConfig : ModConfig
    {
        public static SiriusConfig Instance;
        
        public override ConfigScope Mode => ConfigScope.ClientSide;
        
        #region Graphics
        
        [Range(0f, 100f)]
        [Increment(1f)]
        [DefaultValue(OverheatUI.DefaultPosX)]
        public float OverheatUIPosX { get; set; }
        
        [Range(0f, 100f)]
        [Increment(1f)]
        [DefaultValue(OverheatUI.DefaultPosY)]
        public float OverheatUIPosY { get; set; }
        
        [DefaultValue(true)]
        public bool BarsPosLock { get; set; }
        
        #endregion
        
    }
}

