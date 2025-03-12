using SiriusMod.Mechanics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SiriusMod.Content.Items.Weapons;
using SiriusMod.Content.Items.Tools.PreHM.PathfinderPickaxe;

namespace SiriusMod.Content.Items.Armor.PreHM.PathfinderArmor
{
    [AutoloadEquip(EquipType.Head)]
    public class PathfinderHelmMelee : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 1;
        }
        
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<PathfinderBreastplate>() && legs.type == ModContent.ItemType<PathfinderLeggings>();
        }
        
        
    }
}
