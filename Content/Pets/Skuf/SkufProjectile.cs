using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProtoMod.Content.Pets.Skuf
{
    internal class SkufProjectile : ModProjectile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("CalamityRuTranslate", out Mod trutranslate) && trutranslate != null;
        }
        
        public string ChatterRepeat = null;
        public List<int> keys = [1,2,3,4,5,6,7,8,9,10,11,12,13,14];
        public static string key;
        public static string text;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 8;
            Main.projPet[Projectile.type] = true;

            // This code is needed to customize the vanity pet display in the player select screen. Quick explanation:
            // * It uses fluent API syntax, just like Recipe
            // * You start with ProjectileID.Sets.SimpleLoop, specifying the start and end frames aswell as the speed, and optionally if it should animate from the end after reaching the end, effectively "bouncing"
            // * To stop the animation if the player is not highlighted/is standing, as done by most grounded pets, add a .WhenNotSelected(0, 0) (you can customize it just like SimpleLoop)
            // * To set offset and direction, use .WithOffset(x, y) and .WithSpriteDirection(-1)
            // * To further customize the behavior and animation of the pet (as its AI does not run), you have access to a few vanilla presets in DelegateMethods.CharacterPreview to use via .WithCode(). You can also make your own, showcased in MinionBossPetProjectile
            ProjectileID.Sets.CharacterPreviewAnimations[Projectile.type] = ProjectileID.Sets.SimpleLoop(1, 1, 1)
               .WhenNotSelected(0, 0).WithOffset(-14f, -20f)
                .WithSpriteDirection(-1);

        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.ZephyrFish); // Copy the stats of the ZephyrFish
            AIType = ProjectileID.ZephyrFish; // Mimic as the ZephyrFish during AI.
            DrawOffsetX -= 9;
            DrawOriginOffsetY -= 9;
        }

        public override bool PreAI()
        {
            return true;
        }
        int syncTimer;

        public override void AI()
        {
            if (++Projectile.ai[1] > 3600)
            {
                SkufShizaSayings();
                Projectile.ai[1] = 0;
            }
            Player player = Main.player[Projectile.owner];
            if (!player.dead && player.HasBuff(ModContent.BuffType<SkufBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            // Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
            // if (!player.dead && player.HasBuff(ModContent.BuffType<YumBuff>()))
            // {
            //     Projectile.timeLeft = 2;
            // }
            DelegateMethods.v3_1 = new Vector3(1f, 0.5f, 0.9f);
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * 6f, 20f, DelegateMethods.CastLightOpen);
            Utils.PlotTileLine(Projectile.Left, Projectile.Right, 20f, DelegateMethods.CastLightOpen);
            if (++syncTimer > 20)
            {
                syncTimer = 0;
                Projectile.netUpdate = true;
            }
        }
        public void SkufShizaSayings()
        {
            Player player = Main.player[Projectile.owner];
            if (Projectile.owner == Main.myPlayer)
            {
                if (!player.dead && !player.ghost)
                {
                    SoundStyle style = new SoundStyle("Terraria/Sounds/Chat") { Pitch = .68f, PitchVariance = .22f}; 
                    SoundEngine.PlaySound(style);
                }
                
                int keynum = Main.rand.Next(keys);
                int tempkey = keynum;
                key = keynum.ToString();
                if (ChatterRepeat == null)
                {
                    ChatterRepeat = key;
                }
                else if (ChatterRepeat == key)
                {
                    keys.Remove(keynum);
                    keynum = Main.rand.Next(keys);
                    key = keynum.ToString();
                    ChatterRepeat = key;
                    keys.Add(tempkey);
                }
                Vector2 pos = Projectile.Center;
                // pos = Vector2.Lerp(pos, new Vector2(Main.player[Projectile.owner].Center.X, Main.player[Projectile.owner].Center.Y - 32) , 0.5f);
                text = Language.GetTextValue($"Mods.Twig.Items.SkufItem.BrotKripChatter.Phrase{key}");
                PopupText.NewText(new AdvancedPopupRequest()
                {
                    Text = text,
                    DurationInFrames = 420,
                    Velocity = 7 * -Vector2.UnitY,
                    Color = new Color(219,255,0)
                }, pos);

                //FargoSoulsUtil.HeartDust(
                //    Projectile.Center, 
                //    addedVel: 0.5f * new Vector2(7 * Projectile.direction, -7),
                //    spreadModifier: 0.5f,
                //    scaleModifier: 0.5f
                //);
            }
        }
    }
    public class PoputTextPatch : ILoadable
    {
        public void Load(Mod mod)
        {
            On_PopupText.Update += On_PopupTextOnUpdate;
        }

        public void Unload()
        {
            On_PopupText.Update -= On_PopupTextOnUpdate;
        }
        private void On_PopupTextOnUpdate(On_PopupText.orig_Update orig, PopupText self, int whoami)
        {
            orig.Invoke(self, whoami);

            if (self.name == SkufProjectile.text && self.active)
                self.scale = 0.7f;
        }
    }
    
}

