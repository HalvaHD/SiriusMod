using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using SiriusMod.Content.Items.Armor.PreHM.BrokenPathfinderArmor;
using SiriusMod.Helpers;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace SiriusMod.Content.Items
{
    public class SiriusGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            Player player = Main.player[Main.myPlayer];

            foreach (var tooltip in tooltips.Where(tooltip => tooltip.Name == "SetBonus"))
            {
                if (player.HasArmor(ItemType<BrokenPathfinderHelmet>(), ItemType<BrokenPathfinderBreastplate>(), ItemType<BrokenPathfinderLeggings>()))
                {
                    ItemHelper.AdjustSetBonusColor(tooltip, new Color(218, 58, 58)); //hex-код #da3a3a как в локализации
                }
            }
        }
    }
}
