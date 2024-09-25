using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Content.Buffs;
using ProtoMod.Content.Items.Placeable;
using ProtoMod.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Healing
{
    public class MedicinePipeThirdTier : ModItem
    {
        public override string Texture => "Twig/Content/Items/Healing/MedicinePipe";
        public static LocalizedText RestoreLifeText { get; private set; }
        public static int HealAmount = 200;

        public override void SetStaticDefaults() {
            RestoreLifeText = this.GetLocalization(nameof(RestoreLifeText));
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults() {
            Item.width = 64;
            Item.height = 64;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 140;
            Item.useTime = 140;
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
        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor,
            Vector2 origin, float scale)
        {
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, 3f);
        }

        public override bool? UseItem(Player player)
        {
            Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), 64,
                64, 0f, 0, ModContent.ProjectileType<MedicinePipeAnimation>(), 0, 0);
            MedicinePipeBuff.DamageInc = 0.075f;
            MedicinePipeBuff.FishingInc = 45;
            player.AddBuff(ModContent.BuffType<Buffs.MedicinePipeBuff>(), 3600*5);
            return true;
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

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<MedicinePipeSecondTier>()
                .AddIngredient(ItemID.SuperHealingPotion, 25)
                .AddIngredient(ModContent.ItemType<StarBar>(), 15)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}   