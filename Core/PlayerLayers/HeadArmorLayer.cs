using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace SiriusMod.Core.PlayerLayers;

public class HeadArmorLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Head);

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        Item headItem = drawInfo.drawPlayer.armor[0];
        Item vanityHeadItem = drawInfo.drawPlayer.armor[10];

        if (vanityHeadItem.IsAir && headItem.ModItem is IHeadArmorGlowing headArmorGlowing)
        {
            headArmorGlowing.DrawGlowmask(drawInfo);
        }
        else if (vanityHeadItem.ModItem is IHeadArmorGlowing vanityHeadArmorGlowing)
        {
            vanityHeadArmorGlowing.DrawGlowmask(drawInfo);
        }
    }
}