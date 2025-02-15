using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SiriusMod.Content.Items
{
    public class ControlPanelKey_Green : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 15;
            Item.height = 10;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
        }

        public override void AddRecipes()
        {
        }
        
        // public override void PostDrawInWorld(SpriteBatch spriteBatch, Microsoft.Xna.Framework.Color lightColor, Microsoft.Xna.Framework.Color alphaColor, float rotation, float  scale, int whoAmI) 	
        // {
        //     Texture2D texture = ModContent.Request<Texture2D>($"{Texture}_Glow", AssetRequestMode.ImmediateLoad).Value;
        //     spriteBatch.Draw
        //     (
        //         texture,
        //         new Vector2
        //         (
        //             Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
        //             Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
        //         ),
        //         new Rectangle(0, 0, texture.Width, texture.Height),
        //         Color.White,
        //         rotation,
        //         texture.Size() * 0.5f,
        //         scale, 
        //         SpriteEffects.None, 
        //         0f
        //     );
        // }
    }
}