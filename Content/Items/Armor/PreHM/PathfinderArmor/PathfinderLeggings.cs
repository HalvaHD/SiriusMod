using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items.Armor.PreHM.PathfinderArmor
{
    [AutoloadEquip(EquipType.Legs)]
    public class PathfinderLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 2;
        }
    }
}
