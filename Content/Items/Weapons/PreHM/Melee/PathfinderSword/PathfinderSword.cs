using SiriusMod.Content.Dusts;
using Microsoft.Xna.Framework;
using SiriusMod.Mechanics;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items.Weapons.PreHM.Melee.PathfinderSword
{
    public class PathfinderSword : Overheat
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 19;
            Item.useAnimation = 19;
            Item.autoReuse = true;
            Item.useTurn = true;

            Item.DamageType = DamageClass.Melee;
            Item.damage = 13;
            Item.knockBack = 6;
            Item.crit = 4;

            Item.UseSound = SoundID.Item1;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(silver: 50);
        }
        
        public override void MeleeEffects(Player player, Rectangle hitbox) {
            if (Main.rand.NextBool(10)) {
                if (OverheatLevel < 1000)
                {
                    Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<OverheatDustCold>());
                }

                if (OverheatLevel >= 1000 || CooldownLevel > 0)
                {
                    Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<OverheatDustHot>());
                }
            }
        }

        public override void OnHitNPC(Player player, Terraria.NPC target, Terraria.NPC.HitInfo hit, int damageDone)
        {
            if (CooldownLevel > 0)
            {
                target.AddBuff(BuffID.OnFire, 60);
            }
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
                if (OverheatLevel >= 300 && OverheatLevel < player.GetModPlayer<SiriusModPlayer>().MaxOverheat)
                {
                    float bonusRatio = (OverheatLevel - 300f) / (player.GetModPlayer<SiriusModPlayer>().MaxOverheat - 300f);
                    damage += MathHelper.Clamp(bonusRatio * player.GetModPlayer<SiriusModPlayer>().swordDMG, 0f, player.GetModPlayer<SiriusModPlayer>().swordDMG) * 0.01f;
                }
        }
        
        public override void UpdateInventory(Player player)
        {
            base.UpdateInventory(player);

            if (CooldownLevel > 0)
            {
                Item.damage = 5;
            }

            else
            {
                Item.damage = 13;
            }
        }
        
    }
}
            
