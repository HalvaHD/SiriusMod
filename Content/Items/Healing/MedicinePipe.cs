using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Content.Buffs;
using ProtoMod.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Healing
{
    public class MedicinePipe : ModItem
    {
        public static LocalizedText RestoreLifeText { get; private set; }
        public static int HealAmount = 100;
        public static bool IsAPotion = false;

        public override void SetStaticDefaults() {
            RestoreLifeText = this.GetLocalization(nameof(RestoreLifeText));
            
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
            Item.shopSpecialCurrency = Twig.CosmicCryCurrencyID2;
            Item.potion = true;
            Item.healLife = 100;
            Item.noUseGraphic = true;
        }

        public override bool? UseItem(Player player)
        {
            Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), 64,
                64, 0f, 0, ModContent.ProjectileType<MedicinePipeAnimation>(), 0, 0);
            MedicinePipeBuff.DamageInc = 0.035f;
            MedicinePipeBuff.FishingInc = 15;
            player.AddBuff(ModContent.BuffType<MedicinePipeBuff>(), 3600*5);
            return true;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor,
            Vector2 origin, float scale)
        {
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, 5);
        }

        public override void GetHealLife(Player player, bool quickHeal, ref int healValue)
        {
            healValue = HealAmount;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips) {
            TooltipLine line = tooltips.FirstOrDefault(x => x.Mod == "Terraria" && x.Name == "HealLife");

            if (line != null) {
                line.Text = Language.GetTextValue("CommonItemTooltip.RestoresLife", RestoreLifeText.Format(HealAmount));
            }
        }
        
    }
    
    
}   