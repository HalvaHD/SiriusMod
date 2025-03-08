using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items.Placeable
{
    public class Gameraiders101_Pashalko : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 52;
            Item.maxStack = Terraria.Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>(GetType().Name).Type;
        }
    }
}