using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Content.Items;
using ProtoMod.Content.Projectiles;
using ProtoMod.Content.Projectiles.HALVAMinions;
using ProtoMod.Content.Tiles;
using ProtoMod.Core.Systems;
using ProtoMod.Systems;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.NPC.Bosses.Protector
{
    [AutoloadBossHead]
    public class Protector : ModNPC
    {
        public static int SecondStageHeadSlot = -1;
        
        public ref float AI_State => ref NPC.ai[0];
        public ref float AI_Time => ref NPC.ai[1];
        public ref float AI_Time2 => ref NPC.ai[2];
        public float YPosition;
        public bool YPositionReached = false;
        public static bool ProjectileFirtsPhaseExist;
        public static Vector2 EyePosition;
        public static int ShootCooldDown = 300;
        public float EyeOpacity = 0f;
        public float EyeScale = 0f;
        private SlotId ProtectorSpawnID;
        readonly SoundStyle ProtectorSpawn = new SoundStyle("ProtoMod/Assets/Sounds/ProtectorSpawn");
        public static bool TrashCanOpen = false;
        public static int TrashCansOpened = 0;
        public static int TrashCanCutSceneTime = 0;

        public override void SetStaticDefaults()
        {
            NPCID.Sets.TrailingMode[Type] = 3;
            NPCID.Sets.TrailCacheLength[Type] = 20;
            Main.npcFrameCount[NPC.type] = 1;
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);

            NPCID.Sets.ImmuneToAllBuffs[Type] = true;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new()
            {
                PortraitScale = 1f,
                PortraitPositionYOverride = 0f,
                // Rotation = 0.2f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }
        public override void SetDefaults()
        {
            NPC.width = NPC.height = 70;
            NPC.scale = 0.3f;
            NPC.Opacity = 0f;

            NPC.lifeMax = 5555;
            NPC.damage = 0;
            NPC.defense = 0;
            NPC.knockBackResist = 0f;

            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.noTileCollide = true;

            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCHit4;
            if (!Main.dedServ)
            {
                Music = MusicLoader.GetMusicSlot(Mod, "Assets/Music/PROTECTOR_P1");
            }

            NPC.aiStyle = -1;
            NPC.value = Terraria.Item.buyPrice(0, 5);

            NPC.npcSlots = 10f;
            NPC.boss = true;
            NPC.SpawnWithHigherTime(30);
            
        }
        public override void Load() {
            // string texture = BossHeadTexture + "_SecondStage"; // Our texture is called "ClassName_Head_Boss_SecondStage"
            // SecondStageHeadSlot = Mod.AddBossHeadTexture(texture);
        }
        public override void BossHeadSlot(ref int index) {
            // int slot = SecondStageHeadSlot;
            // if (SecondStage && slot != -1) {
            //     // If the boss is in its second stage, display the other head icon instead
            //     index = slot;
            // }
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry) {
            bestiaryEntry.Info.AddRange(new List<IBestiaryInfoElement> {
                new MoonLordPortraitBackgroundProviderBestiaryInfoElement(),
                new FlavorTextBestiaryInfoElement("")
            });
        }
        public override void OnKill() {
            // The first time this boss is killed, spawn ExampleOre into the world. This code is above SetEventFlagCleared because that will set downedMinionBoss to true.
            // if (!DownedBossSystem.downedProtector) {
            // }

            // This sets downedMinionBoss to true, and if it was false before, it initiates a lantern night
            Terraria.NPC.SetEventFlagCleared(ref DownedBossSystem.downedProtector, -1);

            // Since this hook is only ran in singleplayer and serverside, we would have to sync it manually.
            // Thankfully, vanilla sends the MessageID.WorldData packet if a BOSS was killed automatically, shortly after this hook is ran

            // If your NPC is not a boss and you need to sync the world (which includes ModSystem, check DownedBossSystem), use this code:
            /*
            if (Main.netMode == NetmodeID.Server) {
                NetMessage.SendData(MessageID.WorldData);
            }
            */
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot) {
            cooldownSlot = ImmunityCooldownID.Bosses; // use the boss immunity cooldown counter, to prevent ignoring boss attacks by taking damage from other sources
            return false;
        }


        

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.IsABestiaryIconDummy) return true;
            Texture2D MainTexture = TextureAssets.Npc[NPC.type].Value;
            Texture2D EyeTexture = ModContent.Request<Texture2D>(Texture + "_Eye").Value;
            Texture2D BackTexture = ModContent.Request<Texture2D>(Texture + "_Eye_Background").Value;
            Rectangle rectangle = NPC.frame;//new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            Rectangle rectangle2 = new Rectangle(0, 0, EyeTexture.Width, EyeTexture.Height);//new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin22 = rectangle2.Size() / 2f;
            Color color26 = drawColor;
            NPC.GetAlpha(color26);

            SpriteEffects effects = NPC.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Texture2D glowmask = ModContent.Request<Texture2D>(Texture + "_Glow").Value;;
            Main.EntitySpriteDraw(BackTexture, new Vector2(NPC.Center.X,NPC.Center.Y + 200 * (1 - NPC.scale)) - screenPos + new Vector2(0, 180 * NPC.scale) , rectangle2, new Color(0 + NPC.Opacity * 255f, 0 + NPC.Opacity * 255f, 0 + NPC.Opacity * 255f) * NPC.Opacity, NPC.rotation, origin22, NPC.scale * 2f, effects);
            if (NPC.scale != 1f)
            {
                Vector2 EyeAdjustment = new Vector2(30 * (1f - NPC.scale), +10 * (1f -  NPC.scale));
                EyePosition = new Vector2((NPC.Center - screenPos -
                                           ((((NPC.Center - new Vector2(Main.player[NPC.target].Center.X + 50,
                                                Main.player[NPC.target].Center.Y))) / Main.player[NPC.target].moveSpeed) *
                                            0.3f)).X, (NPC.Center - screenPos).Y + 190) + EyeAdjustment;
            }
            else if (NPC.scale == 1f && NPC.Opacity == 1f)
            {
                EyePosition = new Vector2((NPC.Center - screenPos -
                                           ((((NPC.Center - new Vector2(Main.player[NPC.target].Center.X + 50,
                                                Main.player[NPC.target].Center.Y))) / Main.player[NPC.target].moveSpeed) *
                                            0.3f)).X, (NPC.Center - screenPos).Y + 190);
            }
           
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);
            Main.EntitySpriteDraw(EyeTexture, EyePosition, rectangle2, Color.White * EyeOpacity, NPC.rotation, origin22, EyeScale, effects); 
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, null, Main.GameViewMatrix.ZoomMatrix);
            Main.EntitySpriteDraw(MainTexture, new Vector2(NPC.Center.X,NPC.Center.Y + 200 * (1 - NPC.scale)) - screenPos + new Vector2(0f, NPC.gfxOffY - 2), rectangle, new Color(0 + NPC.Opacity * 255f, 0 + NPC.Opacity * 255f, 0 + NPC.Opacity * 255f) * NPC.Opacity, NPC.rotation, origin2, NPC.scale, effects);
            Main.EntitySpriteDraw(glowmask, new Vector2(NPC.Center.X,NPC.Center.Y + 200 * (1 - NPC.scale)) - screenPos + new Vector2(0f, NPC.gfxOffY - 2), rectangle, new Color(0 + NPC.Opacity * 255f, 0 + NPC.Opacity * 255f, 0 + NPC.Opacity * 255f) * NPC.Opacity, NPC.rotation, origin2, NPC.scale, effects);
            for (int i = 1; i < NPC.oldPos.Length; i += 3)
            {
                Color color = NPC.GetAlpha(drawColor) * 0.4f;
                color *= (NPC.oldPos.Length - i) / 15f;
                Main.EntitySpriteDraw(glowmask, NPC.position - NPC.velocity * i * 0.5f, NPC.frame, color,
                    NPC.oldRot[i] * 0.5f + NPC.oldRot[i] * 0.5f + MathHelper.Pi / 2, origin2,
                    NPC.scale - i / (float)NPC.oldPos.Length / 4f, SpriteEffects.None, 0f);
            }
            
            return false;
        }
        public override void BossLoot(ref string name, ref int potionType)
            => potionType = ItemID.HealingPotion;

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public override void AI()
        {
            Player player = Main.player[NPC.target];
            
            
            if (TrashCanCutSceneTime > 0)
            {
                TrashCanCutSceneTime--;
            }
            if (Protector.TrashCanOpen == true && Protector.TrashCanCutSceneTime == 0)
            {
                Terraria.Item.NewItem(new EntitySource_Misc("Trashcan"), player.position, ModContent.ItemType<YellowCrystalShard>());
                Protector.TrashCanOpen = false;
            }
            if (EyeOpacity >= 1f && !SoundEngine.TryGetActiveSound(ProtectorSpawnID, out var activeSound3) && NPC.scale < 0.5)
            {
                ProtectorSpawnID = SoundEngine.PlaySound(ProtectorSpawn);
            }
            if (ShootCooldDown < 180 && EyeOpacity <= 1f)
            {
                EyeOpacity += 0.05f;
                
            }

            if (ShootCooldDown < 100 && NPC.Opacity != 1f)
            {
                NPC.Opacity += 0.01f;
                
            }

            if (ShootCooldDown < 100 && NPC.scale < 1f)
            {
                NPC.scale += 0.03f;
            }
            if (ShootCooldDown < 100 && EyeScale < 1f)
            {
                EyeScale += 0.02f;
            }
           
            NPC.rotation = NPC.velocity.X * 0.005f;
            
            
            

            NPC.netAlways = true;
            NPC.TargetClosest();

            if (NPC.target < 0 || NPC.target == 255 || player.dead || !player.active || AI_State == -1)
                NPC.TargetClosest();
            if (player.dead)
            {
                NPC.active = false;
                return;
            }

            

            if (AI_State == 0)
            {
                AI_Time++;
                if (ProjectileFirtsPhaseExist == false && ShootCooldDown <= 0)
                {
                    Projectile.NewProjectile(new EntitySource_Parent(Entity.ModNPC.NPC), new Vector2(NPC.Center.X, NPC.Center.Y + 200), Vector2.Zero,  
                        ModContent.ProjectileType<ProtectorBlast>(), 0, 0f);
                    ProjectileFirtsPhaseExist = true;
                }

                if (ShootCooldDown > 0)
                {
                    ShootCooldDown--;
                }
                Vector2 playerPos = new Vector2(player.Center.X + 130, player.Center.Y - 400);  
                Vector2 toPlayerPos = (playerPos - NPC.Center);
                Vector2 toPlayerPosNormalized = toPlayerPos.SafeNormalize(Vector2.UnitY);
                float speed = 10f;
                float inertia = 1f;
                Vector2 moveTo = (toPlayerPosNormalized * speed);
                if (Vector2.Distance(NPC.Center, playerPos) < 32f)
                {
                    inertia = 1f;
                    speed = Vector2.Distance(NPC.Center, playerPos);
                    Vector2 moveToClose = toPlayerPosNormalized * speed;
                    
                        NPC.velocity = ((NPC.velocity * (inertia - 1) + moveToClose) / inertia);
                    

                }
                else
                {
                    speed = 20f;
                    inertia = 1f;
                    NPC.velocity = ((NPC.velocity * (inertia - 1) + moveTo) / inertia);
                    
                    
                }
                
            }
        }

        public override void OnSpawn(IEntitySource source)
        {
            TrashCanCutSceneTime = 0;
            TrashCansOpened = 0;
            TrashCanOpen = false;
            YPositionReached = false;
            ProjectileFirtsPhaseExist = false;
            ShootCooldDown = 360;
            EyeOpacity = 0f;
            EyeScale = 0.3f;


        }
    }
}



