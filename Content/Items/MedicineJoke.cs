using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Common.Players;
using ProtoMod.Content.Buffs;
using ProtoMod.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items
{
    public class MedicineJoke : ModItem
    {
        public static bool JokeUsage = false;
        public override void SetStaticDefaults() {
            
            // Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 19));
            Item.ResearchUnlockCount = 1;

            // // Dust that will appear in these colors when the item with ItemUseStyleID.DrinkLiquid is used
            // ItemID.Sets.DrinkParticleColors[Type] = new Color[3] {
            //     new Color(240, 240, 240),
            //     new Color(200, 200, 200),
            //     new Color(140, 140, 140)
            // };
        }

        public override void SetDefaults() {
            Item.width = 64;
            Item.height = 64;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 120;
            Item.useTime = 170;
            Item.useTurn = true;
            Item.UseSound = null;
            Item.maxStack = 1;
            Item.consumable = false;
            Item.rare = ItemRarityID.Lime;
            Item.shopCustomPrice = 2;
            Item.shopSpecialCurrency = ProtoMod.CosmicCryCurrencyID2;
            Item.noUseGraphic = true;
        }

        public override bool? UseItem(Player player)
        {
            Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), 64,
                64, 0f, 0, ModContent.ProjectileType<MedicineJokeAnimation>(), 0, 0);
            if (player.GetModPlayer<ProtoModPlayer>().MedicineJokeBuffActive && player.whoAmI == Main.myPlayer && ProtoModPlayer.MedicineJokeUseCD == 0)
            {
                int buff = player.FindBuffIndex(ModContent.BuffType<MedicineJokeBuff>());
                player.buffTime[buff] += 3600;
                ProtoModPlayer.MedicineJokeUseCD = 170;    
            }
            else
            {
                player.AddBuff(ModContent.BuffType<MedicineJokeBuff>(), 3600);
            }
            
            return true;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor,
            Vector2 origin, float scale)
        {
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, 5f);
        }
        
    }
    
}   