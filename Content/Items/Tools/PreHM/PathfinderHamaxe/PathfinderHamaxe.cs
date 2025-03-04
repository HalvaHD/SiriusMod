using SiriusMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        
        private int OverheatTimer = 0;
        private int CooldownTimer = 0;
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

        public override bool CanUseItem(Player player)
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
        
        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int direction)
        {
            Texture2D Texture_Glow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Texture2D Texture_Overheat = ModContent.Request<Texture2D>(Texture + "_Overheat").Value;
            Texture2D Texture_kitkat = ModContent.Request<Texture2D>("Mods.SiriusMod.Assets.ExtraTextures.kitkat").Value;
            Texture2D Texture_kitkat_bar = ModContent.Request<Texture2D>("Mods.SiriusMod.Assets.ExtraTextures.kitkat_bar").Value;
            
            Rectangle rectangle = new Rectangle(0, 0, Texture_kitkat.Width, Texture_kitkat.Height);
            Vector2 origin = rectangle.Size() / 2f;
            Color color50 = alphaColor;
            
            Player player = Main.LocalPlayer;
            if (player.HeldItem.type == Item.type && player.controlUseItem)
            {
                // Main.EntitySpriteDraw(Texture_kitkat, player.Center, rectangle, color50, 1f, origin, 1f,  SpriteEffects.None, 0f);
                
                Vector2 position = player.Center - new Vector2(Texture_kitkat.Width / 2, 50f);
                spriteBatch.Draw(Texture_kitkat, position, Color.White);
                
                float overheatPercent = OverheatTimer / 420f;
                Rectangle barRect =  new Rectangle(0, 0, (int)(Texture_kitkat_bar.Width * overheatPercent), Texture_kitkat_bar.Height);
                
                spriteBatch.Draw(Texture_kitkat_bar, position, barRect, Color.White);
            }
            
            return true;
        }


    }
}
