using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Twig.Content.Projectiles;
using Twig.Core.Systems;

namespace Twig.Content.Items
{
    public class Teleporter : ModItem
    {
        public int FrameCounter;

        public int Frame;

        public static bool CanTeleport;

        public static Vector2 OldVelocity;
        

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.rare = ItemRarityID.Cyan;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.scale = 0.45f;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
        }

        #region Drawing
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation.X = player.Center.X + 0.3f * 16 * player.direction;
            player.itemLocation.Y = player.Center.Y - 0.1f*16;
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
        {
            Texture2D texture = ModContent.Request<Texture2D>("Twig/Content/Items/Teleporter_Glowmask", AssetRequestMode.ImmediateLoad).Value;
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.Cyan,
                rotation,
                texture.Size() * 0.5f,
                scale, 
                SpriteEffects.None, 
                0f
            );
        }
        #endregion
        

        public override bool? UseItem(Player player)
        {
            if (KeybindSystem.TeleporterCreateKey.Current && TeleporterInstance.GatesOpen == false && TeleporterInstance.CanBeReOpened)
            {
                TeleporterInstance.GatesOpen = true;
                TeleporterInstance.CanBeReOpened = false;
                WorldSaveSystem.TeleporterLocation = player.Center;
                    Projectile.NewProjectile(Item.GetSource_FromThis(), WorldSaveSystem.TeleporterLocation, Vector2.Zero,
                        ModContent.ProjectileType<TeleporterInstance>(), 0, 0, player.whoAmI);
                    SoundStyle style = new SoundStyle("Terraria/Sounds/Custom/dd2_etherian_portal_open") with { Volume = 1f,  Pitch = 1f};
                    SoundEngine.PlaySound(style);   
            }
            else if (KeybindSystem.TeleporterDestroyKey.Current && TeleporterInstance.GatesOpen)
            {
                TeleporterInstance.GatesOpen = false;
                TeleporterInstance.CanBeReOpened = false;
                SoundStyle style = new SoundStyle("Terraria/Sounds/Custom/dd2_etherian_portal_open") with { Volume = 1f,  Pitch = .07f}; 
                SoundEngine.PlaySound(style);
            }
            else if (WorldSaveSystem.TeleporterLocation != Vector2.Zero && TeleporterInstance.GatesOpen)
            {
                if (Main.projectile.Any((p) => p.active && p.owner == player.whoAmI))
                {
                    OldVelocity = player.velocity;
                    OldVelocity.Normalize();
                    TeleporterInstance.TeleportUse = true;
                }
                

            }
            else
            {
                int text = CombatText.NewText(player.Hitbox, Color.Aqua,
                Language.GetTextValue($"Mods.Twig.Status.NoGates"), true);
            }
            return true;
        }
    }
    public class CombatTextPatch : ILoadable
    {
        public void Load(Mod mod)
        {
            On_CombatText.Update += On_CombatTextUpdate;
        }

        public void Unload()
        {
            On_CombatText.Update -= On_CombatTextUpdate;
        }

        private void On_CombatTextUpdate(On_CombatText.orig_Update orig, CombatText self)
        {
            orig.Invoke(self);

            if (self.text == Language.GetTextValue($"Mods.Twig.Status.NoGates"))
                self.scale = 0.75f;
        }
    }
}
