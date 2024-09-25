using Terraria;
using Terraria.ModLoader;
using Twig.Common.Players;

namespace Twig.Content.Buffs
{
    public class DarkEnergyBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<TwigModPlayer>().DarkEnergy = true;
        }
    }
}   