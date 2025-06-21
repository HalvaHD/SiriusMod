using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod
{
    [ReinitializeDuringResizeArrays]
    public static class CustomSiriusSets
    {
        public const string MechanicalAccessorySetKey = "MechanicalAccessory";

        public static bool[] MechanicalAccessory = ItemID.Sets.Factory.CreateNamedSet(MechanicalAccessorySetKey)
            .Description("Mechanical accessory set")
            .RegisterBoolSet(false);
        
        public const string InfoAccessorySetKey = "InfoAccessory";

        public static bool[] InfoAccessory = ItemID.Sets.Factory.CreateNamedSet(InfoAccessorySetKey)
            .Description("Info accessory set")
            .RegisterBoolSet(false);
        
        public const string NotBuffPotionSetKey = "NotBuffPotion";

        public static bool[] NotBuffPotion = ItemID.Sets.Factory.CreateNamedSet(NotBuffPotionSetKey)
            .Description("Info accessory set")
            .RegisterBoolSet(false);
        
        public const string BuffStationSetKey = "BuffStation";

        public static bool[] BuffStation = ItemID.Sets.Factory.CreateNamedSet(BuffStationSetKey)
            .Description("Info accessory set")
            .RegisterBoolSet(false);
        
        public const string InstaUnbreakableSetKey = "InstaUnbreakable";

        public static bool[] InstaUnbreakable = ItemID.Sets.Factory.CreateNamedSet(InstaUnbreakableSetKey)
            .Description("Info accessory set")
            .RegisterBoolSet(false);
        
        public const string DungeonTileSetKey = "DungeonTile";

        public static bool[] DungeonTile = ItemID.Sets.Factory.CreateNamedSet(DungeonTileSetKey)
            .Description("Info accessory set")
            .RegisterBoolSet(false);
        
        public const string HardmodeOreSetKey = "HardmodeOre";

        public static bool[] HardmodeOre = ItemID.Sets.Factory.CreateNamedSet(HardmodeOreSetKey)
            .Description("Info accessory set")
            .RegisterBoolSet(false);
        
        public const string DungeonWallSetKey = "DungeonWall";

        public static bool[] DungeonWall = ItemID.Sets.Factory.CreateNamedSet(DungeonWallSetKey)
            .Description("Info accessory set")
            .RegisterBoolSet(false);


    }
    public class CustomSiriusSetsSystem : ModSystem
    {
        public override void SetStaticDefaults()
        {
            #region Items
            
            #region MechanicalAccessory
            CustomSiriusSets.MechanicalAccessory[ItemID.MechanicalLens] = true;
            CustomSiriusSets.MechanicalAccessory[ItemID.WireKite] = true;
            CustomSiriusSets.MechanicalAccessory[ItemID.Ruler] = true;
            CustomSiriusSets.MechanicalAccessory[ItemID.LaserRuler] = true;
            CustomSiriusSets.MechanicalAccessory[ItemID.PaintSprayer] = true;
            CustomSiriusSets.MechanicalAccessory[ItemID.ArchitectGizmoPack] = true;
            CustomSiriusSets.MechanicalAccessory[ItemID.HandOfCreation] = true;
            CustomSiriusSets.MechanicalAccessory[ItemID.EncumberingStone] = true;
            CustomSiriusSets.MechanicalAccessory[ItemID.DontHurtCrittersBook] = true;
            CustomSiriusSets.MechanicalAccessory[ItemID.DontHurtComboBook] = true;
            CustomSiriusSets.MechanicalAccessory[ItemID.DontHurtNatureBook] = true; 
            CustomSiriusSets.MechanicalAccessory[ItemID.LucyTheAxe] = true;
            #endregion

            #region InfoAccessory
            CustomSiriusSets.InfoAccessory[ItemID.CopperWatch] = true;
            CustomSiriusSets.InfoAccessory[ItemID.TinWatch] = true;
            CustomSiriusSets.InfoAccessory[ItemID.SilverWatch] = true;
            CustomSiriusSets.InfoAccessory[ItemID.TungstenWatch] = true;
            CustomSiriusSets.InfoAccessory[ItemID.GoldWatch] = true;
            CustomSiriusSets.InfoAccessory[ItemID.PlatinumWatch] = true;
            CustomSiriusSets.InfoAccessory[ItemID.Compass] = true;
            CustomSiriusSets.InfoAccessory[ItemID.DepthMeter] = true;
            CustomSiriusSets.InfoAccessory[ItemID.GPS] = true;
            CustomSiriusSets.InfoAccessory[ItemID.PDA] = true;
            CustomSiriusSets.InfoAccessory[ItemID.CellPhone] = true;
            CustomSiriusSets.InfoAccessory[ItemID.Shellphone] = true;
            CustomSiriusSets.InfoAccessory[ItemID.ShellphoneSpawn] = true;
            CustomSiriusSets.InfoAccessory[ItemID.ShellphoneOcean] = true;
            CustomSiriusSets.InfoAccessory[ItemID.ShellphoneHell] = true;
            CustomSiriusSets.InfoAccessory[ItemID.GoblinTech] = true;
            CustomSiriusSets.InfoAccessory[ItemID.DPSMeter] = true;
            CustomSiriusSets.InfoAccessory[ItemID.MetalDetector] = true;
            CustomSiriusSets.InfoAccessory[ItemID.LifeformAnalyzer] = true;
            CustomSiriusSets.InfoAccessory[ItemID.FishermansGuide] = true;
            CustomSiriusSets.InfoAccessory[ItemID.WeatherRadio] = true;
            CustomSiriusSets.InfoAccessory[ItemID.Sextant] = true;
            CustomSiriusSets.InfoAccessory[ItemID.Radar] = true;
            CustomSiriusSets.InfoAccessory[ItemID.TallyCounter] = true;
            #endregion
            
            #region NotBuffPotio
            CustomSiriusSets.NotBuffPotion[ItemID.RecallPotion] = true;
            CustomSiriusSets.NotBuffPotion[ItemID.PotionOfReturn] = true;
            CustomSiriusSets.NotBuffPotion[ItemID.WormholePotion] = true;
            CustomSiriusSets.NotBuffPotion[ItemID.TeleportationPotion] = true;
            #endregion

            #region BuffStation

            CustomSiriusSets.BuffStation[ItemID.SharpeningStation] = true;
            CustomSiriusSets.BuffStation[ItemID.AmmoBox] = true;
            CustomSiriusSets.BuffStation[ItemID.CrystalBall] = true;
            CustomSiriusSets.BuffStation[ItemID.BewitchingTable] = true;
            CustomSiriusSets.BuffStation[ItemID.WarTable] = true;
            #endregion

            #endregion
            
            #region Tiles
            #region DungeonTile
            CustomSiriusSets.DungeonTile[TileID.BlueDungeonBrick] = true;
            CustomSiriusSets.DungeonTile[TileID.GreenDungeonBrick] = true;
            CustomSiriusSets.DungeonTile[TileID.PinkDungeonBrick] = true;
            #endregion

            #region HardmodeOre
            CustomSiriusSets.HardmodeOre[TileID.Cobalt] = true;
            CustomSiriusSets.HardmodeOre[TileID.Palladium] = true;
            CustomSiriusSets.HardmodeOre[TileID.Mythril] = true;
            CustomSiriusSets.HardmodeOre[TileID.Orichalcum] = true;
            CustomSiriusSets.HardmodeOre[TileID.Adamantite] = true;
            CustomSiriusSets.HardmodeOre[TileID.Titanium] = true;
            #endregion
            #endregion
            
            #region Walls
            CustomSiriusSets.DungeonWall[WallID.BlueDungeonSlabUnsafe] = true;
            CustomSiriusSets.DungeonWall[WallID.BlueDungeonTileUnsafe] = true;
            CustomSiriusSets.DungeonWall[WallID.BlueDungeonUnsafe] = true;
            CustomSiriusSets.DungeonWall[WallID.GreenDungeonSlabUnsafe] = true;
            CustomSiriusSets.DungeonWall[WallID.GreenDungeonTileUnsafe] = true;
            CustomSiriusSets.DungeonWall[WallID.GreenDungeonUnsafe] = true;
            CustomSiriusSets.DungeonWall[WallID.PinkDungeonSlabUnsafe] = true;
            CustomSiriusSets.DungeonWall[WallID.PinkDungeonTileUnsafe] = true;
            CustomSiriusSets.DungeonWall[WallID.PinkDungeonUnsafe] = true;
            #endregion
        }
    }
}
