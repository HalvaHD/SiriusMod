using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Placeable.LaboratoryItems
{
    public class BrokenServers : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 76;
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