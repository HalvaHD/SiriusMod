using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;


namespace SiriusMod.Mechanics
{
    public abstract class Overheat : ModItem
    {
        private int OverheatTimer = 0;
        private int CooldownTimer = 0;
        private SlotId OverheatBrokeID;
        public SoundStyle OverheatBroke = new SoundStyle("SiriusMod/Assets/Sounds/OverheatBroke");
        
        public override void HoldItem(Player player)
        {
            if (player.controlUseItem)
            {
                if (CooldownTimer <= 0)
                {
                    OverheatTimer++;
                }

                if (OverheatTimer >= 420)
                {
                    if (!SoundEngine.TryGetActiveSound(OverheatBrokeID, out var sound3))
                    {
                        OverheatBrokeID = SoundEngine.PlaySound(OverheatBroke);
                    }
                    
                    player.AddBuff(BuffID.OnFire, 300);
                    CooldownTimer = 540;
                    OverheatTimer = 0;
                }
            }
        }

        public override bool CanUseItem(Player player)
        {
            if (CooldownTimer > 0)
            {
                return false;
            }
            
            return true;
        }

        public override void UpdateInventory(Player player)
        {
            if (!player.controlUseItem)
            {
                if (OverheatTimer > 0)
                {
                    OverheatTimer--;
                }
            }

            if (CooldownTimer > 0)
            {
                CooldownTimer--;
            }
        }
    }
}

