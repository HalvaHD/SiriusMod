using SiriusMod.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace SiriusMod.Content.Buffs
{
    public class DarkEnergyBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<ProtoModPlayer>().DarkEnergy = true;
        }
    }
}   