using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items.Placeable.LaboratoryItems
{
    public class LabDoor : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 41;
            Item.maxStack = Terraria.Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            //Item.createTile = Mod.Find<ModTile>(GetType().Name).Type;
        }

        public override void AddRecipes()
        {
        }
    }
}