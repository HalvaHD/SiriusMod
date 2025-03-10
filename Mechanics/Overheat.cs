using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.Items.SummonItems;



namespace SiriusMod.Mechanics
{
    public abstract class Overheat : ModItem
    {
        private int aprilFoolsTimer = 0; // смешно не правда ли?

        private int OverheatTimer = 0;
        private int CooldownTimer = 0;
        public int OverheatLevel => OverheatTimer;
        public int CooldownLevel => CooldownTimer;
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
                
                int maxOverheat = player.GetModPlayer<SiriusModPlayer>().MaxOverheat;
                if (OverheatTimer >= maxOverheat)
                {
                    if (!SoundEngine.TryGetActiveSound(OverheatBrokeID, out var sound3))
                    {
                        OverheatBrokeID = SoundEngine.PlaySound(OverheatBroke);
                    }
                    
                    player.AddBuff(BuffID.OnFire, 300);
                    CooldownTimer = 300;
                    OverheatTimer = 0;
                    aprilFoolsTimer = 1;
                }
            }
            //Main.NewText(OverheatLevel);
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


            if (aprilFoolsTimer > 0)
            {
                aprilFoolsTimer++;

                if (aprilFoolsTimer == 30)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        Vector2 pos = player.Center + new Vector2(Main.rand.Next(-50, 50), Main.rand.Next(-50, 50));
                        Projectile.NewProjectile(null, pos, Vector2.Zero, ProjectileID.FireworkFountainBlue + Main.rand.Next(4), 0, 0, Main.myPlayer);
                    }
                }

                if (aprilFoolsTimer > 60 && aprilFoolsTimer % 60 == 0 && aprilFoolsTimer <= 660)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Vector2 pos = player.Center + new Vector2(Main.rand.Next(-40, 40), Main.rand.Next(-40, 40));
                        Projectile.NewProjectile(null, pos, Vector2.Zero, ProjectileID.Bomb, 100, 10, Main.myPlayer);
                    }
                }
                
                if (aprilFoolsTimer == 120)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        int head = NPC.NewNPC(null, (int)player.position.X, (int)player.position.Y - 200, ModContent.NPCType<DevourerofGodsHead>());

                        if (head >= 0 && head < Main.maxNPCs)
                        {
                            NPC npc = Main.npc[head];
                            npc.ai[0] = 1; // СРАЗУ запускаем вторую фазу
                            npc.netUpdate = true;

                            // Спавним тело
                            int lastSegment = head;
                            for (int i = 0; i < 50; i++) // 50 сегментов тела
                            {
                                lastSegment = NPC.NewNPC(null, (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<DevourerofGodsBody>(), lastSegment);
                            }

                            // Спавним хвост
                            int tail = NPC.NewNPC(null, (int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<DevourerofGodsTail>(), lastSegment);

                            Main.NewText("Ты перегрел кирку... Теперь тебя выебет админ", Color.Crimson);
                        }
                    }
                    aprilFoolsTimer = 0; // сбрасываем таймер
                }
            }
        }
    }
}










// старая механика

/*public override bool CanUseItem(Player player)
{
    if (CooldownTimer > 0)
    {
        return false;
    }

    return true;
}*/




/*ДЛЯ ШЛЕМА СЕТ БОНУС
    
public override void UpdateArmorSet(Player player)
{
    player.setBonus = "Перегрев теперь 20 секунд вместо 10";
    player.GetModPlayer<SiriusModPlayer>().MaxOverheat = 1200; // увеличиваем до 20 сек
}*/
