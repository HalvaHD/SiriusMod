using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using System.Collections.Generic;
using CalamityMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.Localization;
using Twig.Content.Items;
using Twig.Content.Items.Healing;
using Twig.Content.Items.Placeable;
using Twig.Content.Projectiles.HALVAMinions;

namespace Twig.Content.NPC
{
    // [AutoloadHead] and NPC.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
    [AutoloadHead]
    public class HALVA : ModNPC
    {
        public const string FirstShop = "LostThings";
        public const string SecondShop = "ShopPH";
        public static int WorldComplexity;
        public static Asset<Texture2D> Texture_Glow;
        [JITWhenModsEnabled("CalamityMod")]
        public Condition DownedCalamitasClone => CalamityConditions.DownedCalamitasClone;
        [JITWhenModsEnabled("CalamityMod")]
        public Condition DownedRavager => CalamityConditions.DownedRavager;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 23; // The total amount of frames the NPC has|
            Texture_Glow = ModContent.Request<Texture2D>("Twig/Content/NPC/HALVA_Glowmask", AssetRequestMode.ImmediateLoad);
            NPCID.Sets.ExtraFramesCount[Type] = 7; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs. This is the remaining frames after the walking frames.
            NPCID.Sets.AttackFrameCount[Type] = 2; // The amount of frames in the attacking animation.
            NPCID.Sets.DangerDetectRange[Type] = 400; // The amount of pixels away from the center of the NPC that it tries to attack enemies.
            NPCID.Sets.AttackType[Type] = 2; // The type of attack the Town NPC performs. 0 = throwing, 1 = shooting, 2 = magic, 3 = melee
            NPCID.Sets.AttackTime[Type] = 60; // The amount of time it takes for the NPC's attack animation to be over once it starts.
            NPCID.Sets.AttackAverageChance[Type] = 15; // The denominator for the chance for a Town NPC to attack. Lower numbers make the Town NPC appear more aggressive.
            NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset
            // Influences how the NPC looks in the Bestiary
            
            NPC.Happiness
                .SetBiomeAffection<OceanBiome>(AffectionLevel.Hate)
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Like) // HALVA likes the snow.
                .SetBiomeAffection<HallowBiome>(AffectionLevel.Love) // HALVA loves the hallow
                .SetNPCAffection(NPCID.TaxCollector, AffectionLevel.Hate) // Hates living near the taxcollector.
                .SetNPCAffection(NPCID.Nurse, AffectionLevel.Dislike) // Dislikes living near the nurse.
                .SetNPCAffection(NPCID.Guide, AffectionLevel.Like) // Likes living near the guide.
                .SetNPCAffection(ModContent.NPCType<KORRO>(), AffectionLevel.Love) // Loves living near the KOPPO.
            ; // < Mind the semicolon!
            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers() {
                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
                Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
                // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
                // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        }

        public override void SetDefaults()
        {
            
            NPC.townNPC = true; // Sets NPC to be a Town NPC
            NPC.friendly = true; // NPC Will not attack player
            NPC.lavaImmune = true;
            NPC.width = 32;
            NPC.height = 42;
            NPC.aiStyle = 7;
            NPC.damage = 40;
            NPC.defense = 400;
            NPC.lifeMax = 200608;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath46;
            NPC.knockBackResist = 0.0001f;
            AnimationType = NPCID.Wizard;
        }

       public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture2D13 = TextureAssets.Npc[NPC.type].Value;
            Rectangle rectangle = NPC.frame;//new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            Color color26 = drawColor;
            NPC.GetAlpha(color26);

            SpriteEffects effects = NPC.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            
            Texture2D glowmask = Texture_Glow.Value;
            Main.EntitySpriteDraw(texture2D13, NPC.Center - screenPos + new Vector2(0f, NPC.gfxOffY - 1), rectangle, NPC.GetAlpha(drawColor), NPC.rotation, origin2, NPC.scale, effects);
            Main.EntitySpriteDraw(glowmask, NPC.Center - screenPos + new Vector2(0f, NPC.gfxOffY - 1), rectangle, Color.White * NPC.Opacity, NPC.rotation, origin2, NPC.scale, effects);
            return false;
        }
        

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the preferred biomes of this town NPC listed in the bestiary.
				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Visuals.Rain,
                
                new NamePlateInfoElement(this.GetLocalizedValue("NameHALVA"), -1),

				// Sets your NPC's flavor text in the bestiary.
				new FlavorTextBestiaryInfoElement("Mods.Twig.Bestiary.HALVA"),
            });
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            return true;
        }


        public override List<string> SetNPCNameList() => new()
        {
        this.GetLocalizedValue("NameHALVA")
        };

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = this.GetLocalizedValue("LostThings");
            button2 = Language.GetTextValue("LegacyInterface.28");
        }
        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
        {
            shopName = firstButton ? FirstShop : SecondShop;
        }
        public override string GetChat()
        {
            WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
            if (Main.specialSeedWorld == false)
            {
                WorldComplexity = 5;
            }
            else if (Main.drunkWorld)
            {
                WorldComplexity = 25;
            }
            else if (Main.notTheBeesWorld)
            {
                WorldComplexity = 37;
            }
            else if (Main.getGoodWorld)
            {
                WorldComplexity = 60;
            }
            else if (Main.tenthAnniversaryWorld)
            {
                WorldComplexity = 10;
            }
            else if (Main.dontStarveWorld)
            {
                WorldComplexity = 31;
            }
            else if (Main.remixWorld)
            {
                WorldComplexity = 51;
            }
            else if (Main.zenithWorld)
            {
                WorldComplexity = 77;
            }

            if (Main.expertMode)
            {
                WorldComplexity += 4;
            }
            else if (Main.masterMode)
            {
                WorldComplexity += 6;
            }

            if (Main.hardMode)
            {
                WorldComplexity += 4;
            }
            if (Main.rand.NextBool(608))
            {
                weightedRandom.Add(this.GetLocalizedValue("Chat.EasterEgg"));
            }
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal1"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal2"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal3"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal4"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal5"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal6"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal7"));
            weightedRandom.Add(this.GetLocalization("Chat.Normal8").Format(WorldComplexity), 4.0);
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal9"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal10"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal11"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal12"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal13"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal14"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal15"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal16"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal17"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal18"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal19"));
            weightedRandom.Add(this.GetLocalizedValue("Chat.Normal20"));
            if (!Main.dayTime)
            {
                if (Main.bloodMoon)
                {
                    weightedRandom.Add(this.GetLocalizedValue("Chat.BloodMoon1"), 5.0);
                    weightedRandom.Add(this.GetLocalizedValue("Chat.BloodMoon1"), 5.0);
                }
                else
                {
                    weightedRandom.Add(this.GetLocalizedValue("Chat.Night1"), 3.0);
                    weightedRandom.Add(this.GetLocalizedValue("Chat.Night2"), 3.0);
                }
            }

            if (Main.eclipse)
            {
                weightedRandom.Add(this.GetLocalizedValue("Chat.Eclipse1"), 6.0);
                weightedRandom.Add(this.GetLocalizedValue("Chat.Eclipse2"), 6.0);
            }
            int FirstNPC = Terraria.NPC.FindFirstNPC(ModContent.NPCType<KORRO>());
            if (FirstNPC != -1)
            {
                weightedRandom.Add(this.GetLocalization("Chat.KORRO1").Format(Main.npc[FirstNPC].FullName));
                weightedRandom.Add(this.GetLocalization("Chat.KORRO2").Format(Main.npc[FirstNPC].FullName));
            }
            int FirstNPC1 = Terraria.NPC.FindFirstNPC(NPCID.BestiaryGirl);
            if (FirstNPC1 != -1)
            {
                weightedRandom.Add(this.GetLocalization("Chat.BestiaryGirl1").Format(Main.npc[FirstNPC1].GivenName),
                    0.45);
                weightedRandom.Add(this.GetLocalization("Chat.BestiaryGirl2").Format(Main.npc[FirstNPC1].GivenName),
                    0.45);
            }
            int FirstNPC2 = Terraria.NPC.FindFirstNPC(NPCID.Guide);
            if (FirstNPC2 != -1)
            {
                weightedRandom.Add(this.GetLocalization("Chat.Guide1").Format(Main.npc[FirstNPC2].GivenName), 0.7);
                weightedRandom.Add(this.GetLocalization("Chat.Guide2").Format(Main.npc[FirstNPC2].GivenName), 0.7);
            }
            return weightedRandom;
        }
        
        [JITWhenModsEnabled("CalamityMod")]
        public override void AddShops()
        {
            NPCShop LostThings = new NPCShop(Type, "LostThings")
                .Add(new Item(ModContent.ItemType<MedicinePipe>()))
                .Add(new Item(ModContent.ItemType<Teleporter>()));

            NPCShop CoinsShop = new NPCShop(Type, "ShopPH")
                // Pre-Hardmode items (they disappear after Wall of Flesh)
                .Add(new Item(ItemID.HealingPotion) { shopCustomPrice = Item.buyPrice(silver: 30) }, Condition.PreHardmode)
                .Add(new Item(ItemID.ManaPotion) { shopCustomPrice = Item.buyPrice(silver: 15) }, Condition.PreHardmode)
                .Add(new Item(ItemID.LifeCrystal) { shopCustomPrice = Item.buyPrice(gold: 5) }, Condition.PreHardmode)
                .Add(new Item(ItemID.ManaCrystal) { shopCustomPrice = Item.buyPrice(silver: 50) }, Condition.PreHardmode)
                .Add(new Item(ItemID.FallenStar, 5) { shopCustomPrice = Item.buyPrice(gold: 1) }, Condition.PreHardmode)
                .Add(new Item(ItemID.CreativeWings) { shopCustomPrice = Item.buyPrice(gold: 8) },
                    Condition.DownedEyeOfCthulhu, Condition.PreHardmode)
                .Add(new Item(ItemID.WaterCandle) { shopCustomPrice = Item.buyPrice(gold: 1, silver: 50) },
                    Condition.DownedSkeletron, Condition.PreHardmode)
                .Add(new Item(ItemID.StrangePlant1) { shopCustomPrice = Item.buyPrice(silver: 50) }, Condition.PreHardmode)
                // Hardmode items (they appear after WOF)
                .Add(new Item(ItemID.GreaterHealingPotion) { shopCustomPrice = Item.buyPrice(silver: 60) }, Condition.Hardmode)
                .Add(new Item(ItemID.GreaterManaPotion) { shopCustomPrice = Item.buyPrice(silver: 30) }, Condition.Hardmode)
                .Add(new Item(ItemID.FallenStar, 10) { shopCustomPrice = Item.buyPrice(gold: 2) }, Condition.Hardmode)
                .Add(new Item(ModContent.ItemType<StarOre>(), 5) { shopCustomPrice = Item.buyPrice(silver: 50) }, Condition.Hardmode)
                .Add(new Item(ItemID.LeafWings) { shopCustomPrice = Item.buyPrice(silver: 30) }, Condition.Hardmode)
                .Add(new Item(ItemID.WaterCandle) { shopCustomPrice = Item.buyPrice(gold: 2) },
                    Condition.DownedSkeletron, Condition.Hardmode)
                .Add(new Item(ItemID.PlaguebringerChestplate) { shopCustomPrice = Item.buyPrice(gold: 10) }, Condition.Hardmode)
            // Independent items (always presented)
                .Add(new Item(ItemID.VanityTreeYellowWillowSeed) { shopCustomPrice = Item.buyPrice(silver: 5) })
                .Add(new Item(ItemID.VanityTreeSakuraSeed) { shopCustomPrice = Item.buyPrice(silver: 5) })
                .Add(new Item(ItemID.PotSuspended) { shopCustomPrice = Item.buyPrice(silver: 5) })
                .Add(new Item(ItemID.PottedForestCedar) { shopCustomPrice = Item.buyPrice(silver: 15) })
                .Add(new Item(ItemID.PottedForestTree) { shopCustomPrice = Item.buyPrice(silver: 15) })
                .Add(new Item(ItemID.PottedForestPalm) { shopCustomPrice = Item.buyPrice(silver: 15) })
                .Add(new Item(ItemID.PottedForestBamboo) { shopCustomPrice = Item.buyPrice(silver: 15) })
                .Add(new Item(ItemID.OasisFountain) { shopCustomPrice = Item.buyPrice(silver: 30) })
                .Add(new Item(ItemID.MysteriousCape) { shopCustomPrice = Item.buyPrice(gold: 1) })
                .Add(new Item(ItemID.RedCape) { shopCustomPrice = Item.buyPrice(gold: 1) })
                .Add(new Item(ItemID.WinterCape) { shopCustomPrice = Item.buyPrice(gold: 1) })
                .Add(new Item(ItemID.LavaMoss) { shopCustomPrice = Item.buyPrice(copper: 30) })
                .Add(new Item(ItemID.KryptonMoss) { shopCustomPrice = Item.buyPrice(copper: 30) })
                .Add(new Item(ItemID.XenonMoss) { shopCustomPrice = Item.buyPrice(copper: 30) })
                .Add(new Item(ItemID.ArgonMoss) { shopCustomPrice = Item.buyPrice(copper: 30) })
                .Add(new Item(ItemID.VioletMoss) { shopCustomPrice = Item.buyPrice(copper: 30) })
                .Add(new Item(ItemID.RainbowMoss) { shopCustomPrice = Item.buyPrice(copper: 30) });
            
            // Calamtiy collaboration items (shit code i suppose)
            if (ModContent.TryFind("CalamityMod", "DubiousPlating", out ModItem DubiousPlating)
                && ModContent.TryFind("CalamityMod", "MysteriousCircuitry", out ModItem MysteriousCircuitry) 
                && ModContent.TryFind("CalamityMod", "AshesofCalamity", out ModItem AshesofCalamity)
                && ModContent.TryFind("CalamityMod", "LifeAlloy", out ModItem LifeAlloy)) 
            {
                CoinsShop.Add(new Item(DubiousPlating.Type) { shopCustomPrice = Item.buyPrice(silver: 1) });
                CoinsShop.Add(new Item(MysteriousCircuitry.Type) { shopCustomPrice = Item.buyPrice(silver: 1) });
                CoinsShop.Add(new Item(AshesofCalamity.Type) { shopCustomPrice = Item.buyPrice(gold: 1) }, DownedCalamitasClone);
                CoinsShop.Add(new Item(LifeAlloy.Type) { shopCustomPrice = Item.buyPrice(gold: 8) }, DownedRavager);
            }
            CoinsShop.Register();
            LostThings.Register();
        }
       
        
        public override bool CanGoToStatue(bool toKingStatue) => true;
        
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            if (Condition.Hardmode.IsMet())
            {
                damage = 700;
            }
            else if (Condition.DownedMoonLord.IsMet())
            {
                damage = 1200;
            }
            else
            {
                damage = 15;
            }
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 600;
            randExtraCooldown = 600;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = ModContent.ProjectileType<WarriorMinion>();
            attackDelay = 1;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 5f;
            randomOffset = 2f;
         
        }
    }
}
