using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Pets.LitosObliterator
{
    internal class LitosObliteratorItem : ModItem
    {
        // Names and descriptions of all ExamplePetX classes are defined using .hjson files in the Localization folder
        public override void SetDefaults()
        {
            Item.value = Item.buyPrice(0, 0, 20, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.shoot = ModContent.ProjectileType<LitosObliteratorProjectile>(); // "Shoot" your pet projectile.
            Item.buffType = ModContent.BuffType<LitosObliteratorBuff>(); // Apply buff upon usage of the Item.
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
        {
            Texture2D texture = ModContent.Request<Texture2D>("Twig/Content/Pets/LitosObliterator/LitosObliteratorItem_Glowmask", AssetRequestMode.ImmediateLoad).Value;
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

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 3600);
            }
        }
    }
}