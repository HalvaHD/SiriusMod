using System;
using SiriusMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SiriusMod.Mechanics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items.Tools.PreHM.PathfinderPickaxe
{
    public class PathfinderPickaxe : Overheat
    {
        public override void SetDefaults()
        {
            Item.damage = 7;
            Item.DamageType = DamageClass.Melee;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 9;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
            Item.pick = 59;
            Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox) {
            if (Main.rand.NextBool(10)) {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<LabMossDust>());
            }
        }
        
        public override void HoldItem(Player player)
        {
            base.HoldItem(player);
            
            if (player.controlUseItem)
            {
                if (OverheatLevel >= 300 && OverheatLevel < player.GetModPlayer<SiriusModPlayer>().MaxOverheat)
                {
                    float bonusRatio = (OverheatLevel - 300f) / (player.GetModPlayer<SiriusModPlayer>().MaxOverheat - 300f);
                    player.pickSpeed -= MathHelper.Clamp(bonusRatio * player.GetModPlayer<SiriusModPlayer>().pickSDP, 0f, player.GetModPlayer<SiriusModPlayer>().pickSDP) * 0.01f;
                }
            }
        }

        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);
            
            if (CooldownLevel > 0)
            {
                player.pickSpeed = Math.Max(player.pickSpeed, 1.0f);
                Item.useTime = 50;
                Item.useAnimation = 30;
            }
            else
            {
                Item.useTime = 9;
                Item.useAnimation = 20;
            }
        }
        
        public Texture2D glowTexture = ModContent.Request<Texture2D>("SiriusMod/Content/Items/Tools/PreHM/PathfinderPickaxe/PathfinderPickaxe_Glow").Value;
        public int glowOffsetY = 0;
        public int glowOffsetX = 0;
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            if (glowTexture != null)
            {
                Texture2D texture = glowTexture;
                spriteBatch.Draw
                (
                    texture,
                    new Vector2
                    (
                        Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                        Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
            base.PostDrawInWorld(spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
        }
    }
}
