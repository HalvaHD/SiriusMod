using System.Collections.Generic;
using System.Linq;
using SiriusMod.Content.Items.Armor.PreHM.BrokenPathfinderArmor;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items
{
    public class SiriusGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(item, tooltips);
            
            Player player = Main.player[Main.myPlayer];
            string setBonusKey = Language.GetTextValue("LegacyTooltip.48");

            foreach (var tooltip in tooltips.Where(tooltip => tooltip.Name == "SetBonus"))
            {
                if (player.armor[0].type == ModContent.ItemType<BrokenPathfinderHelmet>() &&
                    player.armor[1].type == ModContent.ItemType<BrokenPathfinderBreastplate>() &&
                    player.armor[2].type == ModContent.ItemType<BrokenPathfinderLeggings>())
                {
                    if (Language.ActiveCulture == GameCulture.FromCultureName(GameCulture.CultureName.Russian))
                    {
                        tooltip.Text = $"{setBonusKey} {Language.GetTextValue("Mods.SiriusMod.Items.BrokenPathfinderHelmet.SetBonus")}".Replace("42ff42", "6c8898"); 
                    }
                    else
                    {
                        tooltip.Text =  $"[c/6c8898:{setBonusKey}] {Language.GetTextValue("Mods.SiriusMod.Items.BrokenPathfinderHelmet.SetBonus")}";
                    }
                }
            }
        }
    }
}
