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
    public class PathfinderHelmRange : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 1;
        }
    }
}