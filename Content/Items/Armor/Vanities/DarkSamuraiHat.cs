using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items.Armor.Vanities
{
    // This tells tModLoader to look for a texture called MinionBossMask_Head, which is the texture on the player
    // and then registers this item to be accepted in head equip slots
    [AutoloadEquip(EquipType.Head)]
    public class DarkSamuraiHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            int equipSlotHead = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head);
            ArmorIDs.Head.Sets.DrawHatHair[equipSlotHead] = true;
        }

        public override void Load()
        {
            // The code below runs only if we're not loading on a server
            if (Main.netMode == NetmodeID.Server)
                return;

            // Add equip textures
            // EquipLoader.AddEquipTexture(Mod, "Twig/Content/Items/Armor/Vanities/DarkSamuraiHat_Head", EquipType.Head, this,
            //     equipTexture: new DarkHead());
        }

        public static bool HitEffect = false;
        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 15;

            // Common values for every boss mask
            Item.rare = ItemRarityID.Blue;
            Item.value = Terraria.Item.sellPrice(silver: 75);
            Item.vanity = true;
            Item.maxStack = 1;
        }
        
        // The code belows allows to give effects to armor even if it is in vanity armor slot
        public override void EquipFrameEffects(Player player, EquipType type)
        {
            player.yoraiz0rDarkness = true;
            player.armorEffectDrawShadowLokis = true;
            
        }
    }

}