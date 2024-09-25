using Microsoft.Xna.Framework;
using ProtoMod.Content.Projectiles.Typeless;
using ProtoMod.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ProtoMod.Content.Items.Placeable
{
    public class House1 : ModItem
    {
        public override void SetStaticDefaults()
        {
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 32;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.value = Item.buyPrice(0, 0, 3);
            Item.createTile = ModContent.TileType<StartingHouseTile>();
        } 
        public override void HoldItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[ModContent.ProjectileType<StartingHouseVisual>()] < 1)
            {
                Vector2 mouse = new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y);
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), mouse, Vector2.Zero, ModContent.ProjectileType<StartingHouseVisual>(), 0, 0, player.whoAmI);
            }
        }
        
    }
}