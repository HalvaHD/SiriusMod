using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Placeable.LaboratoryItems
{
    public class LabDoor : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 136;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>(GetType().Name).Type;
        }

        public override void AddRecipes()
        {
        }
        
    }
}