using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SiriusMod.Helpers;

public static class ItemHelper
{
    /// <summary>
    /// Проверяет, надеты ли на игроке конкретные части брони.
    /// </summary>
    /// <param name="player">Игрок, у которого проверяется экипировка.</param>
    /// <param name="armorPieces">Массив ID предметов брони (шлем, нагрудник, поножи).</param>
    /// <returns>
    /// Возвращает <c>true</c>, если все переданные предметы брони надеты на игроке, иначе <c>false</c>.
    /// </returns>
    public static bool HasArmor(this Player player, params int[] armorPieces)
    {
        return armorPieces.Select((t, i) => player.armor[i].type == t).All(x => x);
    }

    public static void AdjustSetBonusColor(TooltipLine tooltip, string color)
    {
        string setBonusKey = Language.GetTextValue("LegacyTooltip.48");
        
        if (ModLoader.TryGetMod("CalamityRuTranslate", out Mod tru) && tru != null)
        {
            tooltip.Text = tooltip.Text.Replace("42ff42", color);
        }
        else
        {
            tooltip.Text = tooltip.Text.Replace(setBonusKey, $"[c/{color}:{setBonusKey}]");
        }
    }

    public static void AdjustSetBonusColor(TooltipLine tooltip, Color color)
    {
        string setBonusKey = Language.GetTextValue("LegacyTooltip.48");
        
        if (ModLoader.TryGetMod("CalamityRuTranslate", out Mod tru) && tru != null)
        {
            tooltip.Text = tooltip.Text.Replace("42ff42", color.Hex3());
        }
        else
        {
            tooltip.Text = tooltip.Text.Replace(setBonusKey, $"[c/{color.Hex3()}:{setBonusKey}]");
        }
    }
}