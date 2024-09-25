using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Twig.Content.Items.Placeable;

namespace Twig.Content.Items.Consumables
{
    public class GracingEnergyPotion : ModItem
    {
        public override void SetStaticDefaults() {
            Item.ResearchUnlockCount = 20;

            // Dust that will appear in these colors when the item with ItemUseStyleID.DrinkLiquid is used
            ItemID.Sets.DrinkParticleColors[Type] =
            [
                new Color(0, 255,  255),
                new Color(255, 255, 255),
                new Color(0, 255, 255)
            ];
        }

        public override void SetDefaults() {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = ItemRarityID.Yellow;
            Item.buffType = ModContent.BuffType<Buffs.GracingEnergyBuff>(); // Specify an existing buff to be applied when used.
            Item.buffTime = 14400 ; // The amount of time the buff declared in Item.buffType will last in ticks. 5400 / 60 is 90, so this buff will last 90 seconds.
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BottledWater)
                .AddIngredient(ItemID.CrystalShard, 3)
                .AddIngredient(ItemID.PixieDust, 3)
                .AddIngredient(ModContent.ItemType<StarBar>())
                .AddIngredient(ItemID.SoulofLight, 2)
                .AddTile(TileID.AlchemyTable)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}