using SiriusMod.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace SiriusMod.Content.Buffs
{
    public class GracingEnergyBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ProtoModPlayer>().GracingEnergy = true;
        }
    }
}   