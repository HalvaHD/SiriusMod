using ProtoMod.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace ProtoMod.Content.Buffs
{
    public class GracingEnergyBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<TwigModPlayer>().GracingEnergy = true;
        }
    }
}   