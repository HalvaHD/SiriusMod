using Terraria.ID;
using Terraria.ModLoader;

namespace Twig.Content.Paintings.Madeline
{
    internal class MadelineItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.createTile = ModContent.TileType<MadelineSheet>();
            Item.width = 28;
            Item.height = 14;
            Item.rare = ItemRarityID.Green;
            Item.maxStack = 99;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
        }
    }
}