using Microsoft.Xna.Framework;
using StructureHelper;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Projectiles
{
    public class StartingHouseProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.timeLeft = 1;
        }
        
        public override void OnKill(int timeLeft)
        {
            Vector2 position = Projectile.Center;
            SoundEngine.PlaySound(SoundID.Item14, position);
            Player player = Main.player[Projectile.owner];
            Vector2 mouse = Main.MouseWorld;
            ModProjectile proj = ModContent.GetModProjectile(ModContent.ProjectileType<StartingHouseVisual>());

            if (player.whoAmI == Main.myPlayer)
            {
                if (player.position.X < mouse.X)
                {
                    Generator.GenerateStructure("Assets/Schematics/FinalFinalHouse",
                        new Point16((int)(mouse.X - Projectile.width + 4) / 16, (int)(mouse.Y - Projectile.height + 8)/ 16 - 25), Mod);
                }
                else
                {
                    Generator.GenerateStructure("Assets/Schematics/FinalFinalHouse",
                        new Point16((int)(mouse.X - 4) / 16 - 27, (int)(mouse.Y - Projectile.height + 8) / 16 - 25), Mod);
                } // 28 width 26 height
                
            }
            
        }
    }
}