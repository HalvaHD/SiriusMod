using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Paintings.YumorousPainting
{
    internal class YumorousItem : ModItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("CalamityRuTranslate", out Mod trutranslate) && trutranslate != null;
        }
        public override void SetDefaults()
        {
            Item.createTile = ModContent.TileType<YumorousSheet>();
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