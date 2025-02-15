using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace SiriusMod.Common.ItemDropRules.DropConditions
{
    public class NextKillsDropCondition : IItemDropRuleCondition
    {
        private static LocalizedText Description;

        public NextKillsDropCondition() {
            Description ??= Language.GetOrRegister("Mods.SiriusMod.DropConditions.NextKills");
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            NPC npc = info.npc;
            return npc.boss
                   && SiriusMod.CheckKilledBosses.Contains(npc.type);
        }

        public bool CanShowItemDropInUI() {
            return true;
        }

        public string GetConditionDescription() {
            return Description.Value;
        }
    }
}