using Terraria.ModLoader;
using Twig.Common.GlobalNPCs;

namespace Twig.Content.Buffs
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