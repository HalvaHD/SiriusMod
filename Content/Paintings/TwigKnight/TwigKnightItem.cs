using Terraria.ID;
using Terraria.ModLoader;

namespace Twig.Content.Paintings.TwigKnight
{
    internal class TwigKnightItem : ModItem
    {
        public override void SetDefaults()
        {
            Item.createTile = ModContent.TileType<TwigSheet>();
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