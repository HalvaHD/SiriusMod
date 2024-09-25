using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Twig.Content.Buffs;
using Twig.Content.Items.Placeable;
using CalamityMod.Items.Potions;
using Twig.Content.Projectiles;

namespace Twig.Content.Items.Healing
{
    [JITWhenModsEnabled("CalamityMod")]
    public class MedicinePipeFourthTier: ModItem
    {
        public override string Texture => "Twig/Content/Items/Healing/MedicinePipe";
        
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("CalamityMod", out Mod calamity) && calamity != null;
        }
        
        public static LocalizedText RestoreLifeText { get; private set; }
        public static int HealAmount = 250;

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
            MedicinePipeBuff.DamageInc = 0.095f;
            MedicinePipeBuff.FishingInc = 60;
            player.AddBuff(ModContent.BuffType<MedicinePipeBuff>(), 3600*5);
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
                .AddIngredient<MedicinePipeThirdTier>()
                .AddIngredient(ModContent.ItemType<SupremeHealingPotion>(), 25)
                .AddIngredient(ModContent.ItemType<StarBar>(), 30)
                .AddTile(TileID.Anvils)
                .Register();

        }
    }
}   