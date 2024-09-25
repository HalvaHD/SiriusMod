using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Twig.Content.Buffs
{
    public class NanoIssue : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[this.Type] = true;
            Main.buffNoSave[this.Type] = false;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
            
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage<GenericDamageClass>() -= 0.10f;
            player.ClearBuff(ModContent.BuffType<MotherOmlett>());

        }
    }
}
