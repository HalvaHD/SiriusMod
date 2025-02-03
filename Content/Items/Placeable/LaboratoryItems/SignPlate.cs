using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace ProtoMod.Content.Items.Placeable.LaboratoryItems
{
    public class SignPlate : ModItem
    {
        public override void SetStaticDefaults()
            => Item.ResearchUnlockCount = 100;

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 20;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createTile = Mod.Find<ModTile>(GetType().Name).Type;
        }

        public override void AddRecipes()
        {
        }
    }
}