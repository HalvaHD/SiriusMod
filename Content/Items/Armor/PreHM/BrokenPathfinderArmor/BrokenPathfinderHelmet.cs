using SiriusMod.Mechanics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SiriusMod.Content.Items.Weapons;
using SiriusMod.Content.Items.Tools.PreHM.PathfinderPickaxe;

namespace SiriusMod.Content.Items.Armor.PreHM.BrokenPathfinderArmor
{
    [AutoloadEquip(EquipType.Head)]
    public class BrokenPathfinderHelmet : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 20;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.defense = 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<BrokenPathfinderBreastplate>() && legs.type == ModContent.ItemType<BrokenPathfinderLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            SiriusModPlayer siriusPlayer = player.GetModPlayer<SiriusModPlayer>();
            
            player.setBonus = this.GetLocalizedValue("SetBonus");
            player.statDefense += 2;
            siriusPlayer.MaxOverheat = 1500f;
            siriusPlayer.pickSDP = 40f;
        }
    }
}