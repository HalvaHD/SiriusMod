using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace ProtoMod.Content.Projectiles
{
    public class TwigGlobalProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        
        // private bool firstTick = true;
        
        public bool lowRender;

        public static HashSet<Rectangle> CannotDestroyRectangle = new HashSet<Rectangle>();

        public float DamageMultiplier = 1;

        public override void SetDefaults(Projectile projectile)
        {
            //if (projectile.CountsAsClass(DamageClass.Summon) || projectile.minion || projectile.sentry || projectile.minionSlots > 0 || ProjectileID.Sets.MinionShot[projectile.type] || ProjectileID.Sets.SentryShot[projectile.type])
            //{
            //    if (!ProjectileID.Sets.IsAWhip[projectile.type])
            //        lowRender = true;
            //}

            //switch (projectile.type)
            //{
            //    case ProjectileID.FlowerPetal:
            //    case ProjectileID.HallowStar:
            //    case ProjectileID.RainbowFront:
            //    case ProjectileID.RainbowBack:
            //        lowRender = true;
            //        break;

            //    default:
            //        break;
            //}

            if (projectile.friendly)
                lowRender = true;
        }

        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.FinalDamage *= DamageMultiplier;
        }
        
        public static bool OkayToDestroyTile(Tile tile)
        {
            if (tile == null)
            {
                return false;
            }
            bool noDungeon = !Terraria.NPC.downedBoss3 && (ProtoModSets.Walls.DungeonWall[tile.WallType] || ProtoModSets.Tiles.DungeonTile[tile.TileType]);

            bool noHMOre = ProtoModSets.Tiles.HardmodeOre[tile.TileType] && !Terraria.NPC.downedMechBossAny;
            bool noChloro = tile.TileType == TileID.Chlorophyte && !(Terraria.NPC.downedMechBoss1 && Terraria.NPC.downedMechBoss2 && Terraria.NPC.downedMechBoss3);
            bool noLihzahrd = (tile.TileType == TileID.LihzahrdBrick || tile.WallType == WallID.LihzahrdBrickUnsafe) && !Terraria.NPC.downedGolemBoss;
            bool noAbyss = false;

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
            {
                if (calamity.TryFind("AbyssGravel", out ModTile gravel) && calamity.TryFind("Voidstone", out ModTile voidstone))
                    noAbyss = tile.TileType == gravel.Type || tile.TileType == voidstone.Type;
            }

            if (noDungeon || noHMOre || noChloro || noLihzahrd || noAbyss ||
                ProtoModSets.Tiles.InstaCannotDestroy[tile.TileType] ||
                ProtoModSets.Walls.InstaCannotDestroy[tile.WallType])
                return false;

            return true;
        }
        public static bool OkayToDestroyTileAt(int x, int y, bool bypassVanillaCanPlace = false) // Testing for blocks that should not be destroyed
        {
            if (!WorldGen.InWorld(x, y))
                return false;
            Tile tile = Main.tile[x, y];
            if (tile == null)
            {
                return false;
            }
            if (CannotDestroyRectangle != null && CannotDestroyRectangle.Any())
            {
                foreach (Rectangle rect in CannotDestroyRectangle)
                {
                    if (rect.Contains(x * 16, y * 16))
                    {
                        return false;
                    }
                }
            }
            Rectangle area = new(x, y, 3, 3);
            if (!bypassVanillaCanPlace && GenVars.structures != null && !GenVars.structures.CanPlace(area))
            {
                return false;
            }
            
            return OkayToDestroyTile(tile);
        }

        // public static bool TileIsLiterallyAir(Tile tile)
        // {
        //     return tile.TileType == 0 && tile.WallType == 0 && tile.LiquidAmount == 0 /*&& tile.sTileHeader == 0 && tile.bTileHeader == 0 && tile.bTileHeader2 == 0 && tile.bTileHeader3 == 0*/ && tile.TileFrameX == 0 && tile.TileFrameY == 0;
        // }

        // public static bool TileBelongsToMagicStorage(Tile tile)
        // {
        //     return Fargowiltas.ModLoaded["MagicStorage"] && TileLoader.GetTile(tile.TileType)?.Mod == ModLoader.GetMod("MagicStorage");
        // }
    }
}