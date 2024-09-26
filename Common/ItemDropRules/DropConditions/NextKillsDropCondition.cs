using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

namespace ProtoMod.Common.ItemDropRules.DropConditions
{
    public class NextKillsDropCondition : IItemDropRuleCondition
    {
        private static LocalizedText Description;

        public NextKillsDropCondition() {
            Description ??= Language.GetOrRegister("Mods.ProtoMod.DropConditions.NextKills");
        }

        public bool CanDrop(DropAttemptInfo info)
        {
            NPC npc = info.npc;
            return npc.boss
                   && ProtoMod.CheckKilledBosses.Contains(npc.type);
        }

        public bool CanShowItemDropInUI() {
            return true;
        }

        public string GetConditionDescription() {
            return Description.Value;
        }
    }
}