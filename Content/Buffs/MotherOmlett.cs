using SiriusMod.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace SiriusMod.Content.Buffs
{
    public class MotherOmlett : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<DamageClassModifier>().NanoDodge = true;

            }
    }
}
