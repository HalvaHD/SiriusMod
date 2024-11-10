using Luminance.Common.Utilities;
using Microsoft.Xna.Framework;
using ProtoMod.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items
{
    public class YellowCrystalShard : ModItem
    {
        public override void SetStaticDefaults() {
            ItemID.Sets.ItemIconPulse[Item.type] = true; 
            ItemID.Sets.ItemNoGravity[Item.type] = false; 
            

            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults() {
            Item.width = 18;
            Item.height = 18;
            Item.maxStack = 1;
            Item.value = 100;
            Item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateInventory(Player player)
        {
            if (Collision.TileCollision(player.position, player.velocity, player.width, player.height, true, false,
                    (int)player.gravDir).Y == 0f)
            {
                player.ConsumeItem(Item.type);
                Projectile.NewProjectile(new EntitySource_Parent(Item),player.Center, Vector2.Zero,
                    ModContent.ProjectileType<YellowCrystalProjectile>(), 0, 0);
            }

            
        }
    }
}