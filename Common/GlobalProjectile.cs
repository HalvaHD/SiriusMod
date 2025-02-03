using ProtoMod.Common.Players;
using ProtoMod.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ProtoMod.Common;

public class GlobalProjectiles : GlobalProjectile
{
    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        if (source is EntitySource_Misc { Context: "KORROGatling" })
        {
            if (Main.hardMode)
            {
                projectile.penetrate = -1;
                projectile.damage = 1;
            }
        }
    }

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
    {
    }
}

