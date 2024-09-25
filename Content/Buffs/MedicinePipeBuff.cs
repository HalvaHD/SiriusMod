using Humanizer;
using Terraria;
using Terraria.ModLoader;

namespace ProtoMod.Content.Buffs
{
    public class MedicinePipeBuff : ModBuff
    {
        public static float DamageInc;
        public static int FishingInc;
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage<GenericDamageClass>() += DamageInc;
            player.fishingSkill += FishingInc;
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            tip = tip.FormatWith(DamageInc * 100, FishingInc);
        }
    }
}
