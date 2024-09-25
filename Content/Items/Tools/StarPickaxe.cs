using Microsoft.Xna.Framework;
using ProtoMod.Common.Players;
using ProtoMod.Content.Projectiles;
using ProtoMod.Content.Projectiles.Magic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Tools
{
    public class StarPickaxe : ModItem
    {
        public static int CrystalikState;
        public override void SetDefaults() {
            Item.damage = 20;
            Item.DamageType = DamageClass.Magic;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 7;
            Item.value = Item.buyPrice(gold: 1); // Buy this item for one gold - change gold to any coin and change the value to any number <= 100
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item1;
            // Item.shoot = ModContent.ProjectileType<StarPickaxeProjectile>();
            Item.autoReuse = true;
            Item.pick = 100; // How strong the pickaxe is, see https://terraria.wiki.gg/wiki/Pickaxe_power for a list of common values
            Item.attackSpeedOnlyAffectsWeaponAnimation = true; // Melee speed affects how fast the tool swings for damage purposes, but not how fast it can dig
        }

        public override void HoldItem(Player player)
        {
            if (StarPickaxeCrystal.CanCrystalBeSeen != true)
            {
                Projectile.NewProjectile(new EntitySource_Misc("CrystalShown"),
                    new Vector2(player.Center.X - 15, player.Center.Y - 56), Vector2.Zero,
                    ModContent.ProjectileType<StarPickaxeCrystal>(), 0, 0, player.whoAmI);
            }
            
        }

        public override void MeleeEffects(Player player, Rectangle hitbox) {
            /*if (Main.rand.NextBool(10)) {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<ExampleCustomDrawDust>());
            }*/
        }

        public override bool AltFunctionUse(Player player)
        {
            if (PlayerInput.Triggers.Current.MouseRight && TwigModPlayer.StarPickaxeHoldTime == 0 && TwigModPlayer.StarPickaxeCD == -1)
            {
                TwigModPlayer.StarPickaxeHoldTime = 60;
                TwigModPlayer.StarPickaxeAvailable = true;
            }
            return true;
        }
        public static void PickaxeShoot(Player player)
        {
                Projectile.NewProjectile(new EntitySource_ItemUse(player, player.HeldItem),
                    new Vector2(player.Center.X, player.Center.Y - 32), Vector2.Zero,
                    ModContent.ProjectileType<StarPickaxeProjectile>(), 10, 1f);
                TwigModPlayer.StarPickaxeCD = 720;
        }

        public override bool CanUseItem(Player player)
        {
            return true;
        }

        // public override bool CanShoot(Player player)
        // {
        //     if (player.altFunctionUse == 2 && player.statMana >= 20)
        //     {
        //         if (player.ownedProjectileCounts[ModContent.ProjectileType<StarPickaxeHoldout>()] < 2)
        //         {
        //             return true;
        //         }
        //         else
        //         {
        //             player.CheckMana(20, true);
        //             return true;
        //         }
        //     }
        //     return false;
        // }


        public override void UseAnimation(Player player) {
            // Randomly causes the player to use Example Pickaxe Emote when using the item
            /*if (Main.myPlayer == player.whoAmI && player.ItemTimeIsZero && Main.rand.NextBool(60)) {
                EmoteBubble.MakePlayerEmote(player, ModContent.EmoteBubbleType<ExamplePickaxeEmote>());
            }*/
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes() {
        }

        
    }
}