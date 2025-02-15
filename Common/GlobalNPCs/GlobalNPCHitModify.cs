using Terraria;
using Terraria.ModLoader;

namespace SiriusMod.Common.GlobalNPCs
{
    public class GlobalNPCHitModfiy : GlobalNPC
    {
        public bool DarkCorrosion;
        public override void ResetEffects(NPC npc)
        {
            DarkCorrosion = false;
        }

        public override bool InstancePerEntity => true;

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (DarkCorrosion)
            {
                npc.lifeRegen -= 12; 
            }
        }
    }
}

