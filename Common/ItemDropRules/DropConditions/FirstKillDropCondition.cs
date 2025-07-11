using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace SiriusMod.Common.ItemDropRules.DropConditions
{
    // Very simple drop condition: drop during daytime
    public class FirstKillDropCondition : IItemDropRuleCondition
    {
        private static LocalizedText Description;

        public FirstKillDropCondition() {
            Description ??= Language.GetOrRegister("Mods.SiriusMod.DropConditions.FirstKill");
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            NPC npc = info.npc;
            return npc.boss
                   && !SiriusMod.CheckKilledBosses.Contains(npc.type);
        }

        public bool CanShowItemDropInUI() {
            return true;
        }

        public string GetConditionDescription() {
            return Description.Value;
        }
    }
}