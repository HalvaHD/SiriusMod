using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProtoMod.Common.Players;
using ProtoMod.Content.NPC;
using ProtoMod.Content.Projectiles.AutAnimation;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items
{
    public class Aut : ModItem
    {
        public static bool TextShowUp = false;
        public override void SetStaticDefaults()
        {

            // Registers a vertical animation with 4 frames and each one will last 5 ticks (1/12 second)
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 5));
            ItemID.Sets.AnimatesAsSoul[Item.type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation
            ItemID.Sets.ItemNoGravity[Item.type] = true; // Makes the item have no gravity
            NPCID.Sets.MPAllowedEnemies[ModContent.NPCType<KORRO>()] = true;
            Item.ResearchUnlockCount = 25; // Configure the amount of this item that's needed to research it in Journey mode.
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 17;
            Item.maxStack = 1;
            Item.scale = 2f;
            Item.value = Item.sellPrice(0,0,50);
            Item.rare = ItemRarityID.Orange;
            Item.consumable = false;
            Item.useTurn = false;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        public override bool CanUseItem(Player player)
        {
            if (!AutAnimation.AutCanSpawn)
            {
                return false;
            }

            return true;
        }


        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.X = (int)player.Center.X - 1.5f * 16 * player.direction;
            player.itemLocation.Y = (int)player.Center.Y + 1.5f*16;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor,
            Vector2 origin, float scale)
        {
            return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, 5f);
        }


        public override bool? UseItem(Player player)
        {
            if (Terraria.NPC.FindFirstNPC(ModContent.NPCType<KORRO>()) == -1 ||
                Terraria.NPC.FindFirstNPC(ModContent.NPCType<HALVA>()) == -1)
            {
                if (AutAnimation.AutCanSpawn)
                {
                    Main.NewText((object)Language.GetTextValue("Mods.ProtoMod.ItemChat.AutSpawn"));
                    Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), Main.LocalPlayer.position.X,
                        Main.LocalPlayer.position.Y, 0f, -3f, ModContent.ProjectileType<AutAnimation>(), 0, 0);
                    SoundStyle style = new SoundStyle("ProtoMod/Assets/Sounds/AncientRoar") { Volume = 10f };
                    SoundEngine.PlaySound(style);
                    return true;
                }
            }
            else
            {
                if (TextShowUp == false)
                {
                    TextShowUps();
                }
            }
            return false;
        }

        public void TextShowUps()
        {
            Main.NewText((object)Language.GetTextValue("Mods.ProtoMod.ItemChat.AutExist"));
            TextShowUp = true;
            ProtoModPlayer.TextShowUpCD = 18;

        }
    }
}

    
