using Terraria;
using Terraria.ModLoader;
using Twig.Common.Players;

namespace Twig.Content.Buffs
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
