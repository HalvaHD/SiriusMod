using SiriusMod.Common.Players;
using Terraria;
using Terraria.ModLoader;

namespace SiriusMod.Content.Buffs
{
    public class MedicineJokeBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetAttackSpeed<GenericDamageClass>() += 0.01f;
            player.GetAttackSpeed<MagicDamageClass>() += 0.01f;
            player.GetAttackSpeed<MeleeDamageClass>() += 0.01f;
            player.GetAttackSpeed<ThrowingDamageClass>() += 0.01f;
            player.GetAttackSpeed<RangedDamageClass>() += 0.01f;
            player.GetAttackSpeed<SummonDamageClass>() += 0.01f;
            
            player.GetDamage<GenericDamageClass>() += 0.01f;
            player.GetDamage<MagicDamageClass>() += 0.01f;
            player.GetDamage<MeleeDamageClass>() += 0.01f;
            player.GetDamage<ThrowingDamageClass>() += 0.01f;
            player.GetDamage<RangedDamageClass>() += 0.01f;
            player.GetDamage<SummonDamageClass>() += 0.01f;
            
            player.GetCritChance<GenericDamageClass>() += 1;
            player.GetCritChance<MagicDamageClass>() += 1;
            player.GetCritChance<MeleeDamageClass>() += 1;
            player.GetCritChance<ThrowingDamageClass>() += 1;
            player.GetCritChance<RangedDamageClass>() += 1;
            player.GetCritChance<SummonDamageClass>() += 1;
            
            player.GetKnockback<GenericDamageClass>() += 0.01f;
            player.GetKnockback<MagicDamageClass>() += 0.01f;
            player.GetKnockback<MeleeDamageClass>() += 0.01f;
            player.GetKnockback<ThrowingDamageClass>() += 0.01f;
            player.GetKnockback<RangedDamageClass>() += 0.01f;
            player.GetKnockback<SummonDamageClass>() += 0.01f;
            
            player.GetArmorPenetration<GenericDamageClass>() += 1f;
            player.GetArmorPenetration<MagicDamageClass>() += 1f;
            player.GetArmorPenetration<MeleeDamageClass>() += 1f;
            player.GetArmorPenetration<ThrowingDamageClass>() += 1f;
            player.GetArmorPenetration<RangedDamageClass>() += 1f;
            player.GetArmorPenetration<SummonDamageClass>() += 1f;
            
            player.breath += 1;
            player.statDefense += 1;
            player.fishingSkill += 1;
            player.statLifeMax2 += 1;
            player.statManaMax2 += 1;
            player.luck += 0.1f; // Needed some check
            player.lifeRegen += 2; // This counts as 1 health per second
            player.moveSpeed += 0.01f;
            player.jumpSpeedBoost += 0.05f; // Plus 1% to jump speed as I assume
            player.maxMinions += 1;
            player.maxTurrets += 1;
            player.GetModPlayer<ProtoModPlayer>().MedicineJokeBuffActive = true;


        }
        
    }
}
