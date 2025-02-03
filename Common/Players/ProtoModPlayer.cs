using Microsoft.Xna.Framework;
using ProtoMod.Content.Items;
using ProtoMod.Content.NPC;
using ProtoMod.Content.Projectiles;
using ProtoMod.Systems;
using Terraria;
using Terraria.Audio;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Common.Players
{
    public partial class ProtoModPlayer : ModPlayer
    {
        
        #region Variables
        
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
        public bool DialogueShown;
        
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

        public int Counter;
        public static string textt = "1";
        public static int i = 1;
        
        
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
            
        }
        

        public override void PreUpdate()
        {
            // DialougeSystem.WriteDialogue("HALVAVahue", "Test2", Color.Aqua);
            if (Player.whoAmI == Main.myPlayer)
            {

                if (StarTrainTicketCD > 0)
                {
                    StarTrainTicketCD--;
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
                                    new Color(255, 255, 255), 3f)];
                        }

                    }
                }
            }
        }
    }
}

