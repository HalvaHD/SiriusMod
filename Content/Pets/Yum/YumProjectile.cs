using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProtoMod.Content.Pets.Yum
{
    internal class YumProjectile : ModProjectile
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModLoader.TryGetMod("CalamityRuTranslate", out Mod trutranslate) && trutranslate != null;
        }

        public string ChatterRepeat = null;
        public List<int> keys = [1, 2, 3, 4, 5, 6, 7];
        public static string key;
        public static string text;
        public bool EGGS = false;

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
            if (EGGS == true)
            {
                // Projectile.rotation += MathHelper.ToRadians(10);
            }
            if (++Projectile.ai[1] > 3600)
            {
                YumShizaSayings();
                Projectile.ai[1] = 0;
            }

            Player player = Main.player[Projectile.owner];
            if (++Projectile.ai[2] > 60)
            {
                if (player.HasItem(ItemID.Eggnog) || player.HasItem(ItemID.BlueEgg) ||
                    player.HasItem(ItemID.FriedEgg) || player.HasItem(ItemID.LizardEgg) ||
                    player.HasItem(ItemID.RottenEgg) || player.HasItem(ItemID.SpiderEgg) ||
                    player.HasItem(ItemID.AntlionEggs))
                {
                    EGGS = true;
                }
                else
                {
                    if (EGGS == true)
                    {
                        EGGS = false;
                        Projectile.rotation = 0;
                    }
                }
                Projectile.ai[2] = 0;
            }

            

            if (!player.dead && player.HasBuff(ModContent.BuffType<YumBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            // Keep the projectile from disappearing as long as the player isn't dead and has the pet buff.
            // if (!player.dead && player.HasBuff(ModContent.BuffType<YumBuff>()))
            // {
            //     Projectile.timeLeft = 2;
            // }
            DelegateMethods.v3_1 = new Vector3(1f, 0.5f, 0.9f);
            Utils.PlotTileLine(Projectile.Center, Projectile.Center + Projectile.velocity * 6f, 20f,
                DelegateMethods.CastLightOpen);
            Utils.PlotTileLine(Projectile.Left, Projectile.Right, 20f, DelegateMethods.CastLightOpen);
            if (++syncTimer > 20)
            {
                syncTimer = 0;
                Projectile.netUpdate = true;
            }
        }

        public void YumShizaSayings()
        {
            Player player = Main.player[Projectile.owner];
            if (EGGS == false)
            {
                if (player.name == "T90_Topnado" && !keys.Contains(7))
                {
                    keys.Add(8);
                }

                if (Projectile.owner == Main.myPlayer)
                {
                    if (!Main.player[Projectile.owner].dead && !Main.player[Projectile.owner].ghost)
                    {
                        SoundStyle style = new SoundStyle("Terraria/Sounds/Chat")
                            { Pitch = .68f, PitchVariance = .22f };
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
                }
            }
            else
            {
                key = "9";
            }

            Vector2 pos = Projectile.Center;
            text = Language.GetTextValue($"Mods.ProtoMod.Items.YumItem.YumChatter.Phrase{key}");

            PopupText.NewText(new AdvancedPopupRequest()
            {
                Text = text,
                DurationInFrames = 420,
                Velocity = 7 * -Vector2.UnitY,
                Color = new Color(255, 59, 226),

            }, pos);
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

            if (self.name == YumProjectile.text && self.active)
                self.scale = 0.7f;
        }
    }
}


