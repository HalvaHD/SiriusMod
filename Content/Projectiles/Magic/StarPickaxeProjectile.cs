using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Projectiles.Magic
{
    // This file shows an animated projectile
    // This file also shows advanced drawing to center the drawn projectile correctly
    public class StarPickaxeProjectile : ModProjectile
    {
        public override string Texture => $"Twig/Content/Items/Tools/StarPickaxe";
        
        public override void SetStaticDefaults()
        {
            // Total count animation frames
            //Main.projFrames[Projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40; // The width of projectile hitbox
            Projectile.height = 40; // The height of projectile hitbox
            Projectile.friendly = true; // Can the projectile deal damage to enemies?
            Projectile.ignoreWater = false; // Does the projectile's speed be influenced by water?
            Projectile.tileCollide = false; // Can the projectile collide with tiles?
            Projectile.timeLeft = 80;
            Projectile.aiStyle = -1;

        }

        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs,
            List<int> behindProjectiles, List<int> overPlayers,
            List<int> overWiresUI)
        {
            behindNPCs.Add(index);
        }
        private void DestroyTiles()
        {
            if (Projectile.owner != Main.myPlayer)
                return;
            int num1 = 3;
            int num2 = (int) (Projectile.Center.X / 16.0 - num1);
            int num3 = (int) (Projectile.Center.X / 16.0 + num1);
            int num4 = (int) (Projectile.Center.Y / 16.0 - num1);
            int num5 = (int) (Projectile.Center.Y / 16.0 + num1);
            if (num2 < 0)
                num2 = 0;
            if (num3 > Main.maxTilesX)
                num3 = Main.maxTilesX;
            if (num4 < 0)
                num4 = 0;
            if (num5 > Main.maxTilesY)
                num5 = Main.maxTilesY;
            AchievementsHelper.CurrentlyMining = true;
            for (int index1 = num2; index1 <= num3; ++index1)
            {
                for (int index2 = num4; index2 <= num5; ++index2)
                {
                    double num6 = Math.Abs(index1 - Projectile.Center.X / 16f);
                    float num7 = Math.Abs(index2 - Projectile.Center.Y / 16f);
                    if (Math.Sqrt(num6 * num6 + num7 * (double) num7) < num1)
                    { 
                        WorldGen.KillTile(index1, index2);
                    }
                }
            }
            AchievementsHelper.CurrentlyMining = false;
        }
        

        public override void AI()
        {
			Vector2 dustPosition = Projectile.Center + new Vector2(Main.rand.Next(-4, 5), Main.rand.Next(-4, 5));
			Dust dust = Dust.NewDustPerfect(dustPosition, DustID.BlueFairy, null, 100, Color.Lime, 0.8f);
			dust.velocity *= 0.3f;
			dust.noGravity = true;

			// In Multi Player (MP) This code only runs on the client of the projectile's owner, this is because it relies on mouse position, which isn't the same across all clients.
			if (Main.myPlayer == Projectile.owner && Projectile.ai[0] == 0f) {
                float maxDistance = 5f; // This also sets the maximun speed the projectile can reach while following the cursor.
					Vector2 vectorToCursor = Main.MouseWorld - Projectile.Center;
					float distanceToCursor = vectorToCursor.Length();

					// Here we can see that the speed of the projectile depends on the distance to the cursor.
					if (distanceToCursor > maxDistance) {
						distanceToCursor = maxDistance / distanceToCursor;
						vectorToCursor *= distanceToCursor;
					}

					int velocityXBy1000 = (int)(vectorToCursor.X * 1000f);
					int oldVelocityXBy1000 = (int)(Projectile.velocity.X * 1000f);
					int velocityYBy1000 = (int)(vectorToCursor.Y * 1000f);
					int oldVelocityYBy1000 = (int)(Projectile.velocity.Y * 1000f);

					// This code checks if the precious velocity of the projectile is different enough from its new velocity, and if it is, syncs it with the server and the other clients in MP.
					// We previously multiplied the speed by 1000, then casted it to int, this is to reduce its precision and prevent the speed from being synced too much.
					if (velocityXBy1000 != oldVelocityXBy1000 || velocityYBy1000 != oldVelocityYBy1000) {
						Projectile.netUpdate = true;
					}

					Projectile.velocity = vectorToCursor;

            }

			// Set the rotation so the projectile points towards where it's going.
            Projectile.rotation += 0.6f;
            DestroyTiles();
        }
    }
}