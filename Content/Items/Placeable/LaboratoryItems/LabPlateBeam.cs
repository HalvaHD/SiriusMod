using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Placeable.LaboratoryItems
{
    public class LabPlateBeam : ModItem
    {
        public override void SetStaticDefaults()
            => Item.ResearchUnlockCount = 100;

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 8;
            Item.maxStack = Terraria.Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 7;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.createWall = Mod.Find<ModWall>(GetType().Name).Type;
        }

        public override void AddRecipes()
        {
        }
    }
}