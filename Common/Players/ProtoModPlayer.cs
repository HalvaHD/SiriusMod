using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Twig.Content.Items;
using Twig.Content.Items.Tools;
using Terraria.ID;
using Twig.Content.Projectiles;

namespace Twig.Common.Players
{
    public partial class TwigModPlayer
    {
        #region Variables
        
        #region No Category
        
        public static bool RainyB;
        public static bool YumShiza;
        public bool DarkEnergy;
        public bool GracingEnergy;
        public bool DarkSamurai;
        public static int TextShowUpCD;
        public static int MedicineJokeUseCD;
        public bool MedicineJokeBuffActive;
        public static bool StarPickaxeAvailable;
        public static int StarPickaxeHoldTime;
        public static int StarPickaxeCD;
        public static int JumpAvailability;
        public static bool IsLastRealFrame;
        public int StarTrainTicketCD;
        
        #endregion
        #region ResetEffects

        public override void ResetEffects()
        {
            RainyB = false;
            YumShiza = false;
            DarkEnergy = false;
            GracingEnergy = false;
            DarkSamurai = false;
            MedicineJokeBuffActive = false;
        }
        #endregion
        #endregion
        
        public Item FindAccessory(int itemID)
        {
            for (int i = 0; i < 10; i++)
            {
                if (Player.armor[i].type == itemID)
                    return Player.armor[i];
            }
            return new Item();
        }

        public override void OnHurt(Player.HurtInfo info)
        {
            if (StarTrainTicket.DamageReceiveTime > 0)
            {
                StarTrainTicket.TicketTeleport(Main.LocalPlayer);
                StarTrainTicket.DamageReceiveTime = 600;
            }
        }
        

        public override void PreUpdate()
        {
            if (Player.whoAmI == Main.myPlayer)
            {
                if (StarTrainTicket.DamageReceiveTime > 0)
                {
                    StarTrainTicket.DamageReceiveTime--;
                }

                if (StarTrainTicketCD > 0)
                {
                    StarTrainTicketCD--;
                }

                if (Filters.Scene["Shockwave"].IsActive())
                {
                    ShockwaveSword.ShockwaveTimeLeft--;
                    if (ShockwaveSword.ShockwaveTimeLeft == 0)
                    {
                        ShockwaveSword.ShockwaveTimeLeft = 180;
                        Filters.Scene["Shockwave"].Deactivate();
                    }
                }

                if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
                {
                    float progress = (180f - ShockwaveSword.ShockwaveTimeLeft) / 60f;
                    Filters.Scene["Shockwave"].GetShader().UseProgress(progress)
                        .UseOpacity(ShockwaveSword.distortStrength * (1 - progress / 3f));
                }

                if (StarPickaxeAvailable)
                {
                    if (Player.HeldItem.type == ModContent.ItemType<StarPickaxe>() &&
                        PlayerInput.Triggers.Current.MouseRight)
                    {
                        StarPickaxeHoldTime--;
                        if (StarPickaxeHoldTime == 0)
                        {
                            StarPickaxeAvailable = false;
                            StarPickaxe.PickaxeShoot(Player);
                        }
                    }
                    else
                    {
                        StarPickaxeAvailable = false;
                        StarPickaxeHoldTime = 0;

                    }
                }

                // mouseRight = PlayerInput.Triggers.Current.MouseRight;
                if (TextShowUpCD > 0)
                {
                    TextShowUpCD--;
                }
                else
                {
                    Aut.TextShowUp = false;
                }

                if (MedicineJokeUseCD > 0)
                {
                    MedicineJokeUseCD--;
                }

                if (StarPickaxeCD > 0)
                {
                    StarPickaxeCD--;
                }
                else if (StarPickaxeCD == 0)
                {
                    StarPickaxeCD--;
                    if (Player.whoAmI == Main.myPlayer)
                    {
                        SoundStyle style = new SoundStyle("Terraria/Sounds/Item_25") with { Pitch = -.29f, };
                        SoundEngine.PlaySound(style);
                        for (int i = 0; i < 30; i++)
                        {
                            Vector2 position = Main.LocalPlayer.Center;
                            Dust dust =
                            Main.dust[
                                Dust.NewDust(position, 10, 10, DustID.TintableDustLighted, 0f, 0f, 0,
                                    new Color(255, 255, 255), 3f)]; // You need to set position depending on what you are doing. You may need to subtract width/2 and height/2 as well to center the spawn rectangle.
                        }

                    }
                }

                if (Player.HeldItem.type == ModContent.ItemType<StarPickaxe>())
                {
                    StarPickaxeCrystal.IsStarPickaxeHeld = true;
                    if (StarPickaxeCD > 0)
                    {
                        StarPickaxe.CrystalikState = 1;
                    }
                    else
                    {
                        StarPickaxe.CrystalikState = 0;
                    }

                }
                else if (StarPickaxeCrystal.IsStarPickaxeHeld)
                {
                    StarPickaxeCrystal.IsStarPickaxeHeld = false;
                }
            }
        }
    }
}

