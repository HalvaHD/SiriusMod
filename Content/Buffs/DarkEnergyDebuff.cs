using SiriusMod.Common.GlobalNPCs;
using Terraria.ModLoader;

namespace SiriusMod.Content.Buffs
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