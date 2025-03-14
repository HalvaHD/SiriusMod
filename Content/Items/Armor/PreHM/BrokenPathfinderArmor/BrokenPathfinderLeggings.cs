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
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.defense = 1;
        }
    }
}