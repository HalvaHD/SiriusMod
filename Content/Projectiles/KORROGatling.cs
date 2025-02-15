using Microsoft.Xna.Framework;
using SiriusMod.Content.NPC;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Projectiles
{
    public class KORROGatling : ModProjectile
    {
        public Terraria.NPC owner;
        public Vector2 StaticNPCPosition;
        public int StaticNPCDirection;
        public int StaticNPCSpriteDirection;
        public static bool SavePosition = false;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 34;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 240;
            Projectile.DamageType = DamageClass.Ranged;
        }
        

        public override void AI()
        {
            foreach (var npc in Main.ActiveNPCs)
            {
                if (npc.type == ModContent.NPCType<KORRO>())
                {
                    owner = npc;
                }
            }
            Terraria.NPC target = FindClosestNPC(600);
            if ( target != null && (target.Center - owner.Center).X < 0)
            {
                owner.direction = -1;
                owner.spriteDirection = -1;
            }
            else if (target != null && (target.Center - owner.Center).X > 0)
            {
                owner.direction = 1;
                owner.spriteDirection = 1;
            }

            owner.aiStyle = 0;
            Projectile.Center = new Vector2(owner.Center.X + 20 * owner.spriteDirection, owner.Center.Y);
            Projectile.spriteDirection = owner.spriteDirection < 0 ? 1 : -1;
            if (target != null)
            {
                Vector2 velocity = ((target.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 50f).RotatedByRandom(MathHelper.ToRadians(7));
                if (Projectile.timeLeft % 2 == 0)
                {
                    Projectile.NewProjectile(new EntitySource_Misc("KORROGatling"), Projectile.Center, velocity,
                        ProjectileID.Bullet, 10, 1f);
                }
                
                
            }

            if (Projectile.timeLeft < 10)
            {
                owner.aiStyle = 7;
            }
        }

        public override void OnKill(int timeLeft)
        {
            owner.aiStyle = 7;
        }

        public Terraria.NPC FindClosestNPC(float maxDetectDistance)
        {
            Terraria.NPC closestNPC = null;
		
            // Using squared values in distance checks will let us skip square root calculations, drastically improving this method's speed.
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;
		
            // Loop through all NPCs(max always 200)
            for (int k = 0; k < Main.maxNPCs; k++)
            {
                Terraria.NPC target = Main.npc[k];
                // Check if NPC able to be targeted. It means that NPC is
                // 1. active (alive)
                // 2. chaseable (e.g. not a cultist archer)
                // 3. max life bigger than 5 (e.g. not a critter)
                // 4. can take damage (e.g. moonlord core after all it's parts are downed)
                // 5. hostile (!friendly)
                // 6. not immortal (e.g. not a target dummy)
                if (target.CanBeChasedBy())
                {
                    // The DistanceSquared function returns a squared distance between 2 points, skipping relatively expensive square root calculations
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);
		
                    // Check if it is within the radius
                    if (sqrDistanceToTarget < sqrMaxDetectDistance && !target.behindTiles)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }
		
            return closestNPC;
        }

        // public override void OnHitNPC(Terraria.NPC target, Terraria.NPC.HitInfo hit, int damageDone)
        // {
        //     Player p = Main.player[Main.myPlayer];
        //     for (int i = 0; i < Main.npc.Length; i++)
        //     {
        //         Terraria.NPC n = Main.npc[i];
        //         if (n.type == ModContent.NPCType<KORRO>())
        //         {
        //             n.velocity = (new Vector2(target.Center.X, target.Center.Y - 25) - n.Center).SafeNormalize(Vector2.Zero) * 120f;
        //             damageDone = 200;
        //             n.AddBuff(ModContent.BuffType<KORROKnockback>(), 15, true);
        //         }
        //     }
        // }
    }
}