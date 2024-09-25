using ProtoMod.Common.Utilities;
using ProtoMod.Content.Items.Accessories;
using ProtoMod.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Common.Players
{
    public partial class TwigModPlayer : ModPlayer
    {
        public static DamageClass DamageTypeForGracing;
        public override void PostHurt(Player.HurtInfo info)
        {
            if (RainyB)
            {
                var source = Player.GetSource_Accessory(FindAccessory(ModContent.ItemType<RainyBracelet>()));
                for (int n = 0; n < 3; n++)
                {
                    int RainyStarDamage = 20;

                    Projectile star = TwigUtils.ProjectileRain(source, Player.Center, 400f, 100f, 500f, 800f, 29f,
                        ProjectileID.StarVeilStar, RainyStarDamage, 4f, Player.whoAmI);
                    if (star.whoAmI.WithinBounds(Main.maxProjectiles))
                    {
                        star.DamageType = DamageClass.Generic;
                        star.usesLocalNPCImmunity = true;
                        star.localNPCHitCooldown = 5;
                    }
                }
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (proj.owner == Player.whoAmI)
            {
                if (DarkEnergy)
                {
                    if (proj.type != ModContent.ProjectileType<DarkEnergyOrb>())
                    {
                        SummonDarkOrbs(target);
                    }
                }
            }
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (DarkEnergy)
            {
                SummonDarkOrbs(target);
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (GracingEnergy)
            {
                DamageTypeForGracing = hit.DamageType;
                GracingEnergy = false;
                Projectile.NewProjectile(new EntitySource_OnHit(Player, target, "GracingOrb"), new Microsoft.Xna.Framework.Vector2(target.Center.X, target.Center.Y + 25),
                    Microsoft.Xna.Framework.Vector2.Zero, ModContent.ProjectileType<GracingEnergyOrb>(), 0, 0f);
            }
        }

        public void SummonDarkOrbs(NPC target)
        {
            if (DarkEnergy)
            {
                DarkEnergy = false;
                Projectile.NewProjectile(new EntitySource_OnHit(Player, target, "DarkOrb"), new Microsoft.Xna.Framework.Vector2(target.Center.X - 35, target.Center.Y),
                    Microsoft.Xna.Framework.Vector2.Zero, ModContent.ProjectileType<DarkEnergyOrb>(), 9, 0.1f);
                Projectile.NewProjectile(new EntitySource_OnHit(Player, target, "DarkOrb"), new Microsoft.Xna.Framework.Vector2(target.Center.X + 35, target.Center.Y),
                    Microsoft.Xna.Framework.Vector2.Zero, ModContent.ProjectileType<DarkEnergyOrb>(), 9, 0.1f);
                Projectile.NewProjectile(new EntitySource_OnHit(Player, target, "DarkOrb"), new Microsoft.Xna.Framework.Vector2(target.Center.X, target.Center.Y - 35),
                    Microsoft.Xna.Framework.Vector2.Zero, ModContent.ProjectileType<DarkEnergyOrb>(), 9, 0.1f);
            }
        }
    }
}

