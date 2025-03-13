using Terraria.DataStructures;
using Terraria.ModLoader;

namespace SiriusMod.Core.PlayerLayers;

public class HeldItemLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.ArmOverItem);

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        if (drawInfo.drawPlayer.HeldItem.ModItem is IHeldItemGlowing heldItemGlowing)
            heldItemGlowing.DrawGlowmask(drawInfo);
    }
}