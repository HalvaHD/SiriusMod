using ProtoMod.Common.Utilities;
using ProtoMod.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Common.Players
{
    public partial class ProtoModPlayer
    {
        public static DamageClass DamageTypeForGracing;
        public override void PostHurt(Player.HurtInfo info)
        {
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
           
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            
        }

        public void SummonDarkOrbs(NPC target)
        {
        }
    }
}

