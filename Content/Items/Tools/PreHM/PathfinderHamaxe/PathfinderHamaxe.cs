using SiriusMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleLibrary.UI.Elements.Base;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items.Tools.PreHM.PathfinderHamaxe
{
    public class PathfinderHamaxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 9;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 10;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.axe = 12;
            Item.hammer = 59;
            Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox) 
        {
            if (Main.rand.NextBool(10)) 
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<LabMossDust>());
            }
        }

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale,
            int whoAmI)
        {
            Texture2D texture_Glow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Texture2D texture_Overheat = ModContent.Request<Texture2D>(Texture + "_Overheat").Value;
            Texture2D texture_kitkat = ModContent.Request<Texture2D>("Mods.SiriusMod.Assets.ExtraTextures.kitkat").Value;
            Texture2D texture_kitkat_bar = ModContent.Request<Texture2D>("Mods.SiriusMod.Assets.ExtraTextures.kitkat_bar").Value;
            
            Rectangle rectangle = new Rectangle(0, 0, texture_kitkat.Width, texture_kitkat.Height);
            Vector2 origin = rectangle.Size() / 2f;
            
            return true;
        }
        
        int OverheatTimer = 0;
        int CooldownTimer = 0;
        public override void HoldItem(Player player)
        {
            if (player.controlUseItem)
            {
                if (CooldownTimer <= 0)
                {
                    OverheatTimer++;
                }
                
                if (OverheatTimer >= 420)
                {
                    player.AddBuff(BuffID.OnFire, 300);
                    CooldownTimer = 540;
                    OverheatTimer = 0;
                }
            }
        }

        public override bool? UseItem(Player player)
        {
            if (CooldownTimer > 0)
            {
                return false;
            }
            return true;
        }

        public override void UpdateInventory(Player player)
        {
            if (!player.controlUseItem)
            {
                if (OverheatTimer > 0)
                {
                    OverheatTimer--;
                }
            }

            if (CooldownTimer > 0)
            {
                CooldownTimer--;
            }
        }
    }
}



















