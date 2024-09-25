using ProtoMod.Common.GlobalNPCs;
using Terraria.ModLoader;

namespace ProtoMod.Content.Buffs
{
    public class DarkEnergyDebuff : ModBuff
    {
        public override void Update(Terraria.NPC npc, ref int buffIndex)
        {
            npc.defDefense -= 3;
            npc.GetGlobalNPC<GlobalNPCHitModfiy>().DarkCorrosion = true;
        }
    }
}   