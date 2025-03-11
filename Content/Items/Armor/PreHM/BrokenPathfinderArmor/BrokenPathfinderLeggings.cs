using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items.Armor.PreHM.BrokenPathfinderArmor
{
    [AutoloadEquip(EquipType.Legs)]
    public class BrokenPathfinderLeggings : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.defense = 1;
        }
    }
}