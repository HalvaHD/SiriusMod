using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod
{
    public class SiriusModSets : ModSystem
    {
        // Спасибо тем, кто придумал эту систему!
        public class Items
        {
            public static bool[] MechanicalAccessory;
            public static bool[] InfoAccessory;
            public static bool[] NonBuffPotion;
            public static bool[] BuffStation;
        }
        public class Tiles
        {
            public static bool[] InstaCannotDestroy;
            public static bool[] DungeonTile;
            public static bool[] HardmodeOre;
        }
        public class Walls
        {
            public static bool[] InstaCannotDestroy;
            public static bool[] DungeonWall;
        }

        public override void PostSetupContent()
        {
            #region Items
            SetFactory itemFactory = new(ItemLoader.ItemCount);

            Items.MechanicalAccessory = itemFactory.CreateBoolSet(false,
                ItemID.MechanicalLens,
                ItemID.WireKite,
                ItemID.Ruler,
                ItemID.LaserRuler,
                ItemID.PaintSprayer,
                ItemID.ArchitectGizmoPack,
                ItemID.HandOfCreation,
                ItemID.ActuationAccessory,
                ItemID.EncumberingStone,
                ItemID.DontHurtCrittersBook,
                ItemID.DontHurtComboBook,
                ItemID.DontHurtNatureBook,
                ItemID.LucyTheAxe);

            Items.InfoAccessory = itemFactory.CreateBoolSet(false,
                ItemID.CopperWatch,
                ItemID.TinWatch,
                ItemID.SilverWatch,
                ItemID.TungstenWatch,
                ItemID.GoldWatch,
                ItemID.PlatinumWatch,
                ItemID.Compass,
                ItemID.DepthMeter,
                ItemID.GPS,
                ItemID.PDA,
                ItemID.CellPhone,
                5358,
                5359,
                5360,
                5361,
                ItemID.GoblinTech,
                ItemID.DPSMeter,
                ItemID.MetalDetector,
                ItemID.Stopwatch,
                ItemID.LifeformAnalyzer,
                ItemID.FishermansGuide,
                ItemID.WeatherRadio,
                ItemID.Sextant,
                ItemID.Radar,
                ItemID.TallyCounter);


            Items.NonBuffPotion = itemFactory.CreateBoolSet(false,
                ItemID.RecallPotion,
                ItemID.PotionOfReturn,
                ItemID.WormholePotion,
                ItemID.TeleportationPotion);

            Items.BuffStation = itemFactory.CreateBoolSet(false,
                ItemID.SharpeningStation,
                ItemID.AmmoBox,
                ItemID.CrystalBall,
                ItemID.BewitchingTable,
                ItemID.WarTable);
            #endregion
            #region Tiles
            SetFactory tileFactory = new(TileLoader.TileCount);

            Tiles.InstaCannotDestroy = tileFactory.CreateBoolSet(false);

            Tiles.DungeonTile = tileFactory.CreateBoolSet(false,
                TileID.BlueDungeonBrick,
                TileID.GreenDungeonBrick,
                TileID.PinkDungeonBrick);

            Tiles.HardmodeOre = tileFactory.CreateBoolSet(false,
                TileID.Cobalt,
                TileID.Palladium,
                TileID.Mythril,
                TileID.Orichalcum,
                TileID.Adamantite,
                TileID.Titanium);
            #endregion
            #region Walls
            SetFactory wallFactory = new(WallLoader.WallCount); 

            Walls.InstaCannotDestroy = wallFactory.CreateBoolSet(false);

            Walls.DungeonWall = wallFactory.CreateBoolSet(false,
                WallID.BlueDungeonSlabUnsafe, 
                WallID.BlueDungeonTileUnsafe, 
                WallID.BlueDungeonUnsafe, 
                WallID.GreenDungeonSlabUnsafe, 
                WallID.GreenDungeonTileUnsafe, 
                WallID.GreenDungeonUnsafe, 
                WallID.PinkDungeonSlabUnsafe, 
                WallID.PinkDungeonTileUnsafe, 
                WallID.PinkDungeonUnsafe);
            #endregion
        }
    }
}
