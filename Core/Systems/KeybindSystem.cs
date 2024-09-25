using Terraria.ModLoader;

namespace ProtoMod.Core.Systems
{
    public class KeybindSystem : ModSystem
    {
        public static ModKeybind TeleporterCreateKey { get; private set; }

        public static ModKeybind TeleporterDestroyKey { get; private set; }

        public override void Load()
        {
            TeleporterCreateKey = KeybindLoader.RegisterKeybind(Mod, "TeleporterCreateKey", "W");
            TeleporterDestroyKey = KeybindLoader.RegisterKeybind(Mod, "TeleporterDestroyKey", "Q");
        }

        public override void Unload()
        {
            TeleporterCreateKey = null;
            TeleporterDestroyKey = null;

        }
    }
}

