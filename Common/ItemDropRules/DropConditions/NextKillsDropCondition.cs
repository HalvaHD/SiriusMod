using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace Twig.Common.ItemDropRules.DropConditions
{
    // Very simple drop condition: drop during daytime
    public class NextKillsDropCondition : IItemDropRuleCondition
    {
        private static LocalizedText Description;

        public NextKillsDropCondition() {
            Description ??= Language.GetOrRegister("Mods.Twig.DropConditions.NextKills");
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            NPC npc = info.npc;
            return npc.boss
                   && Twig.CheckKilledBosses.Contains(npc.type);
        }

        public bool CanShowItemDropInUI() {
            return true;
        }

        public string GetConditionDescription() {
            return Description.Value;
        }
    }
}