using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using SiriusMod.Core;
using SiriusMod.Core.PlayerLayers;
using SiriusMod.Helpers;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class PathfinderShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 20;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<PathfinderDashPlayer>().DashAccEquipped = true;
        }
    }
    

    public class PathfinderDashPlayer : ModPlayer
    {
        public bool IsHit = false;
        public const int DashUp = 1;
        public const int DashRight = 2;
        public const int DashLeft = 3;
        public const int DashDown = 0;

        public const int DashCD = 50;
        public const int DashDuration = 30;

        public const float DashVelocity = 10f;

        public int DashDir = -1;

        public bool DashAccEquipped;
        public int DashDelay = 0;
        public int DashTimer = 0;

        public override void ResetEffects()
        {
            DashAccEquipped = false;

            if (Player.controlDown && Player.releaseDown && Player.doubleTapCardinalTimer[DashDown] < 15)
            {
                DashDir = DashDown;
            }

            else if (Player.controlRight && Player.releaseRight && Player.doubleTapCardinalTimer[DashRight] < 15 && Player.doubleTapCardinalTimer[DashLeft] == 0)
            {
                DashDir = DashRight;
            }

            else if (Player.controlLeft && Player.releaseLeft && Player.doubleTapCardinalTimer[DashLeft] < 15 &&  Player.doubleTapCardinalTimer[DashRight] == 0)
            {
                DashDir = DashLeft;
            }
            else
            {
                DashDir = -1;
            }
        }

        public override void PreUpdateMovement()
        {
            if (DashAvailable() && DashDir != -1 && DashDelay == 0)
            {
                Vector2 newVelocity = Player.velocity;

                switch (DashDir)
                {
                    case DashUp when Player.velocity.Y > -DashVelocity:
                    case DashDown when Player.velocity.Y < DashVelocity:
                    {
                        float dashDirection = DashDir == DashDown ? 1 : -1.3f;
                        newVelocity.Y = dashDirection * DashVelocity;
                        break;
                    }
                    case DashRight when Player.velocity.X > -DashVelocity:
                    case DashLeft when Player.velocity.X < DashVelocity:
                    {
                        float dashDirection = DashDir == DashRight ? 1 : -1;
                        newVelocity.X = dashDirection * DashVelocity;
                        break;
                    }
                    default:
                        return;
                }
                DashDelay = DashCD;
                DashTimer = DashDuration;
                Player.velocity = newVelocity;
            }

            if (DashDelay > 0)
            {
                DashDelay--;
                IsHit = false;
            }

            if (DashTimer > 0)
            {
                Player.eocDash = DashTimer;
                Player.armorEffectDrawShadowEOCShield = true;

                DashTimer--;
            }
        }

        public override void OnHitByNPC(Terraria.NPC npc, Player.HurtInfo hurtInfo)
        {
            if (DashTimer > 0 && IsHit == false)
            {
                npc.SimpleStrikeNPC(0, -hurtInfo.HitDirection, false, 500f);
                IsHit = true;
            }
        }

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (DashTimer > 0)
            {
                modifiers.FinalDamage *= 0.0001f;
            }
        }


        private bool DashAvailable()
        {
            return DashAccEquipped
                   && Player.dashType == DashID.None
                   && !Player.setSolar
                   && !Player.mount.Active;
        }
    }
    public class DrawAccPlayer : ModPlayer
    {
        public override void ResetEffects()
        {
            base.ResetEffects();
        }
    }
}

