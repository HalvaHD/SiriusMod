using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Twig.Content.Pets.Berserk;
using Twig.Content.Pets.BlackCat;
using Twig.Content.Pets.CatX;
using Twig.Content.Pets.ContractSSS;
using Twig.Content.Pets.Hello;
using Twig.Content.Pets.Keksik;
using Twig.Content.Pets.LightTwig;
using Twig.Content.Pets.LitosObliterator;
using Twig.Content.Pets.OldTwig;
using Twig.Content.Pets.Ror;
using Twig.Content.Pets.Skuf;
using Twig.Content.Pets.SlimeHero;
using Twig.Content.Pets.SlimeR;
using Twig.Content.Pets.Stardy;
using Twig.Content.Pets.Stepasha;
using Twig.Content.Pets.Sum;
using Twig.Content.Pets.Twig;
using Twig.Content.Pets.Vedma;
using Twig.Content.Pets.Yum;
using Twig.Content.Projectiles;

namespace Twig.Content.NPC;

[AutoloadHead]
public class KORRO: ModNPC {
    public static bool FirstShop = false;
    public static bool SecondShop = false;
    public static string Shop = "CustomPets";
    public static Asset<Texture2D> Texture_Glow;
    
    public static bool Shop1 = true;
    public static bool Shop2 = false;
    public override void SetStaticDefaults()
    {
        Texture_Glow = ModContent.Request<Texture2D>("Twig/Content/NPC/KORRO_Glowmask", AssetRequestMode.ImmediateLoad);
        NPC.Happiness
            .SetBiomeAffection<OceanBiome>(AffectionLevel.Like)
            .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)
            .SetNPCAffection(ModContent.NPCType<HALVA>(), AffectionLevel.Love)
            .SetNPCAffection(NPCID.BestiaryGirl, AffectionLevel.Like)
            .SetNPCAffection(NPCID.Pirate, AffectionLevel.Dislike)
            .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Hate); 
        NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers() {
            Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
            Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
            // Rotation = MathHelper.ToRadians(180) // You can also change the rotation of an NPC. Rotation is measured in radians
            // If you want to see an example of manually modifying these when the NPC is drawn, see PreDraw
        };
        NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
        Main.npcFrameCount[Type] = 25;
        NPCID.Sets.ExtraFramesCount[Type] = 9;
        NPCID.Sets.AttackFrameCount[Type] = 4;
        NPCID.Sets.DangerDetectRange[Type] = 700;
        NPCID.Sets.PrettySafe[Type] = 300;
        NPCID.Sets.AttackType[Type] = 2;
        NPCID.Sets.AttackTime[Type] = 60;
        NPCID.Sets.AttackAverageChance[Type] = 30;
        NPCID.Sets.HatOffsetY[Type] = 4;
        NPCID.Sets.TrailCacheLength[Type] = 7;
        NPCID.Sets.TrailingMode[Type] = 0;
    }


    public override void SetDefaults()
    {
            
        NPC.townNPC = true;
        NPC.friendly = true;
        NPC.lavaImmune = true;
        NPC.width = 35;
        NPC.height = 40;
        NPC.aiStyle = 7;
        NPC.damage = 10;
        NPC.defense = 15;
        NPC.lifeMax = 150720;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.knockBackResist = 0.5f;
        AnimationType = NPCID.Guide;
        DrawOffsetY -= 2;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[]
        {
            // Sets the preferred biomes of this town NPC listed in the bestiary.
            // With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,

            // Sets your NPC's flavor text in the bestiary.
            new FlavorTextBestiaryInfoElement("Mods.Twig.Bestiary.KORRO"),
        });
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
        Main.EntitySpriteDraw(texture2D13, NPC.Center - screenPos + new Vector2(0f, NPC.gfxOffY - 2), rectangle, NPC.GetAlpha(drawColor), NPC.rotation, origin2, NPC.scale, effects);
        Main.EntitySpriteDraw(glowmask, NPC.Center - screenPos + new Vector2(0f, NPC.gfxOffY - 2), rectangle, Color.White * NPC.Opacity, NPC.rotation, origin2, NPC.scale, effects);
        return false;
    }

    public override bool CanTownNPCSpawn(int numTownNPCs) { // Requirements for the town NPC to spawn.
            
        return true;
    }
    public override void SetChatButtons(ref string button, ref string button2) {
        button = Language.GetTextValue("LegacyInterface.28");
        button2 = this.GetLocalizedValue("ModItems");
    }
    public override void OnChatButtonClicked(bool firstButton, ref string shopName) {
        if (firstButton)
        {
            shopName = "Shop";
            Main.NewText("Первый магаз");
        }
        else
        {
            shopName = "CustomPets";
            Main.NewText("Второй магаз");
        }
    }
    public override void AddShops()
    {
        NPCShop shop1 = new NPCShop(Type)
            .Add(new Item(ItemID.BlueEgg))
            .Add(new Item(ItemID.BerniePetItem))
            .Add(new Item(ItemID.ParrotCracker))
            .Add(new Item(ItemID.UnluckyYarn))
            .Add(new Item(ItemID.DogWhistle))
            .Add(new Item(ItemID.CompanionCube))
            .Add(new Item(ItemID.DD2PetGato))
            .Add(new Item(ItemID.BambooLeaf))
            .Add(new Item(ItemID.BallOfFuseWire))
            .Add(new Item(ItemID.ExoticEasternChewToy))
            .Add(new Item(ItemID.EucaluptusSap))
            .Add(new Item(ItemID.GlommerPetItem))
            .Add(new Item(ItemID.PigPetItem))
            .Add(new Item(ItemID.LightningCarrot))
            .Add(new Item(ItemID.BirdieRattle))
            .Add(new Item(ItemID.DirtiestBlock))
            .Add(new Item(ItemID.SpiderEgg));
        

        NPCShop shop2 = new NPCShop(Type, Shop)
            .Add(new Item(ModContent.ItemType<TwigPetItem>()))
            .Add(new Item(ModContent.ItemType<ContractSSSItem>()))
            .Add(new Item(ModContent.ItemType<LitosObliteratorItem>()))
            .Add(new Item(ModContent.ItemType<KeksikItem>()))
            .Add(new Item(ModContent.ItemType<RorItem>()))
            .Add(new Item(ModContent.ItemType<HelloItem>()))
            .Add(new Item(ModContent.ItemType<StardyItem>()))
            .Add(new Item(ModContent.ItemType<SlimeRItem>()))
            .Add(new Item(ModContent.ItemType<BlackCatItem>()))
            .Add(new Item(ModContent.ItemType<VedmaItem>()))
            .Add(new Item(ModContent.ItemType<CatXItem>()))
            .Add(new Item(ModContent.ItemType<BerserkItem>()))
            .Add(new Item(ModContent.ItemType<SlimeHeroItem>()))
            .Add(new Item(ModContent.ItemType<OldTwigItem>()))
            .Add(new Item(ModContent.ItemType<LightTwigItem>()))
            .Add(new Item(ModContent.ItemType<YumItem>()))
            .Add(new Item(ModContent.ItemType<StepashaItem>()))
            .Add(new Item(ModContent.ItemType<SumItem>()))
            .Add(new Item(ModContent.ItemType<SkufItem>()));
        shop1.Register();
        shop2.Register();
    }
    public override string GetChat() {
        WeightedRandom<string> weightedRandom = new WeightedRandom<string>();
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal1"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal2"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal3"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal4"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal7"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal8"));
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
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal21"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal22"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal23"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal24"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal25"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal26"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal26"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal27"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal28"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal29"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal30"));
        weightedRandom.Add(this.GetLocalizedValue("Chat.Normal31"));
            
        return weightedRandom;
            
    }
    public override bool CanGoToStatue(bool toKingStatue) => true;

    public override void TownNPCAttackStrength(ref int damage, ref float knockback) {
        damage = 20;
        knockback = 1f;
    }

    public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) {
        cooldown = 400;
        randExtraCooldown = 400 ;
    }

    public override void TownNPCAttackProj(ref int projType, ref int attackDelay) {
        projType = ModContent.ProjectileType<KORROGatling>();
        attackDelay = 1;
    }
}