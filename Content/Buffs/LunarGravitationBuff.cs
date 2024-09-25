using Terraria;
using Terraria.ModLoader;

namespace ProtoMod.Content.Buffs
{
    public class LunarGravitationBuff : ModBuff
    {
        public override void Update(Player player, ref int buffIndex)
        { 
            Player.jumpHeight = 24; // Технически 9 блоков, по факту прыжок в 8
            player.jumpSpeedBoost += 0.08f;
        }
    }
}   