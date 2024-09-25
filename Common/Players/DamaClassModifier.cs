using System.IO;
using Microsoft.Xna.Framework;
using ProtoMod.Content.Buffs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Common.Players
{
    internal class DamageClassModifier : ModPlayer
    {
        //public float AdditiveCritDamageBonus;
        public static bool MotherOmlett;
        public static bool NanoIssue;
        // These 3 fields relate to the Example Dodge. Example Dodge is modeled after the dodge ability of the Hallowed armor set bonus.
        // exampleDodge indicates if the player actively has the ability to dodge the next attack. This is set by ExampleDodgeBuff, which in this example is applied by the HitModifiersShowcase weapon. The buff is only applied if exampleDodgeCooldown is 0 and will be cleared automatically if an attack is dodged or if the player is no longer holding HitModifiersShowcase.
        public bool NanoDodge; // TODO: Example of custom player render

        public static int NanoDodgeCooldown; // Used to add a delay between Example Dodge being consumed and the next time the dodge buff can be aquired.
        // public int exampleDodgeVisualCounter; // Controls the intensity of the visual effect of the dodge.

        // If this player has an accessory which gives this effect
        // public bool hasAbsorbTeamDamageEffect;
        // If the player is currently in range of a player with hasAbsorbTeamDamageEffect
        // public bool defendedByAbsorbTeamDamageEffect;

        // public bool exampleDefenseDebuff;

        public override void PreUpdate()
        {
            // Timers and cooldowns should be adjusted in PreUpdate
            if (NanoDodgeCooldown > 0)
            {
                NanoDodgeCooldown--;
            }
        }

        public override void ResetEffects()
        {

            NanoDodge = false;
            MotherOmlett = false;
            NanoIssue = false;
        }

        public override bool ConsumableDodge(Player.HurtInfo info)
        {
            if (NanoDodge && (info.Damage >= Player.statLife * 0.5))
            {
                NanoDodgeEffect();
                return true;
            }

            return false;
        }

        // ExampleDodgeEffects() will be called from ConsumableDodge and HandleExampleDodgeMessage to sync the effect.
        public void NanoDodgeEffect()
        {
            Player.SetImmuneTimeForAllTypes(Player.longInvince ? 120 : 80);

            // Some sound and visual effects
            for (int i = 0; i < 50; i++)
            {
                Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                Dust d = Dust.NewDustPerfect(Player.Center + speed * 16, DustID.Silver, speed * 5, Scale: 1.5f);
                d.noGravity = true;
            }
            for (int i = 0; i < 50; i++)
            {
                SoundEngine.PlaySound(SoundID.NPCHit4, Player.Center);
            }
            

            // The visual and sound effects happen on all clients, but the code below only runs for the dodging player 
            if (Player.whoAmI != Main.myPlayer)
            {
                return;
            }

            // Clearing the buff and assigning the cooldown time
            Player.ClearBuff(ModContent.BuffType<MotherOmlett>());
            NanoDodgeCooldown = 7200; // 120 second cooldown before the buff can be given again.
            Main.player[Main.myPlayer].AddBuff(ModContent.BuffType<NanoIssue>(), 1200);
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                SendDodgeMessage(Player.whoAmI);
            }
        }

        public static void HandleSendDodgeMessage(BinaryReader reader, int whoAmI)
        {
            int player = reader.ReadByte();
            if (Main.netMode == NetmodeID.Server)
            {
                player = whoAmI;
            }

            Main.player[player].GetModPlayer<DamageClassModifier>().NanoDodgeEffect();

            if (Main.netMode == NetmodeID.Server)
            {
                // If the server receives this message, it sends it to all other clients to sync the effects.
               SendDodgeMessage(player);
            }
        }

        public static void SendDodgeMessage(int whoAmI)
        {
            // This code is called by both the initial 
            ModPacket packet = ModContent.GetInstance<Twig>().GetPacket();
            // packet.Write((byte)Twig.MessageType.NanoDodge);
            packet.Write((byte)whoAmI);
            packet.Send(ignoreClient: whoAmI);
        }
    }
}
