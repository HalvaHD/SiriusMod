using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Pets.Berserk
{
    internal class BerserkItem : ModItem
    {
        // Names and descriptions of all ExamplePetX classes are defined using .hjson files in the Localization folder
        public override void SetDefaults()
        {

            Item.CloneDefaults(ItemID.ZephyrFish); // Copy the Defaults of the Zephyr Fish Item.
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.LightRed;

            Item.shoot = ModContent.ProjectileType<BerserkProjectile>(); // "Shoot" your pet projectile.
            Item.buffType = ModContent.BuffType<BerserkBuff>(); // Apply buff upon usage of the Item.
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600);
            }
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {

        }
    }
}
