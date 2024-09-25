using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Twig.Content.Projectiles.HALVAMinions;

public class NUCLEARPOWER : ModProjectile
{
    public List<Terraria.NPC> Listik;
    public Vector2 LastCenter;

    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 1;
        ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
    }

    public override void SetDefaults()
    {
        Projectile.width = 68;
        Projectile.height = 28;
        Projectile.tileCollide = true; // Makes the minion go through tiles freely
        // These below are needed for a minion weapon
        Projectile.friendly = true; // Only controls if it deals damage to enemies on contact (more on that later)
        // Projectile.minion = true; // Declares this as a minion (has many effects)
        Projectile.DamageType = DamageClass.Ranged; // Declares the damage type (needed for it to deal damage)
        Projectile.penetrate = -1; // Needed so the minion doesn't despawn on collision with enemies or tiles
        DrawOffsetX += 3;
        DrawOriginOffsetY -= 1;
    }

    public override void AI()
    {
        Projectile.velocity.Y += 0.1f; // 0.1f for arrow gravity, 0.4f for knife gravity
        if (Projectile.velocity.Y >
            16f) // This check implements "terminal velocity". We don't want the projectile to keep getting faster and faster. Past 16f this projectile will travel through blocks, so this check is useful.
        {
            Projectile.velocity.Y = 16f;
        }
        Projectile.rotation = Projectile.velocity.ToRotation();
    }

    public override void OnSpawn(IEntitySource source)
    {
        
    }

    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        for (int i = 0; i < 250; i++) {
            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.Center.X - 400, Projectile.Center.Y - 200),800, 400, DustID.Smoke, 0f, 0f, 100, default, 10f);
            dust.velocity *= 1.4f;
        }

        // Fire Dust spawn
        for (int i = 0; i < 250; i++) {
            Dust dust = Dust.NewDustDirect(new Vector2(Projectile.Center.X - 400, Projectile.Center.Y - 200), 800, 400, DustID.Smoke, 0f, 0f, 100, default, 10f);
            dust.noGravity = true;
            dust.velocity *= 5f;
            dust = Dust.NewDustDirect(new Vector2(Projectile.Center.X - 400, Projectile.Center.Y - 200), 800, Projectile.height, DustID.Torch, 0f, 0f, 100, default, 3f);
            dust.velocity *= 3f;
        }
        SoundStyle style = new SoundStyle("Twig/Assets/Sounds/NUCLEAREXPLOSION") {Volume = 1f}; 
        
        SoundEngine.PlaySound(style);
        foreach (Terraria.NPC target in Main.ActiveNPCs)
        {
            float distance = Math.Abs(Projectile.Center.X - target.Center.X);
            if (target.CanBeChasedBy() && distance < 640)
            {
                target.SimpleStrikeNPC(500, 1, true);
            }
        }

        
        return true;
    }
}

