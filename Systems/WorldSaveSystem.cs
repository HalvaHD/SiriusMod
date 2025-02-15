using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SiriusMod.Systems
{
    public class WorldSaveSystem : ModSystem
    {
        public static Vector2 TeleporterLocation
        {
            get;
            set;
        } = Vector2.Zero;
        
        public override void SaveWorldData(TagCompound tag)
        {
            if (WorldGen.generatingWorld)
                return;
            tag["TeleporterLocationX"] = TeleporterLocation.X;
            tag["TeleporterLocationY"] = TeleporterLocation.Y;
            tag["KilledBosses"] = SiriusMod.CheckKilledBosses;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            SiriusMod.CheckKilledBosses = new List<int>(tag.GetList<int>("KilledBoss"));
        }
    }
}