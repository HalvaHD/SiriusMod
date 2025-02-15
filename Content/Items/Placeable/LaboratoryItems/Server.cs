// using Terraria.ID;
// using Terraria.ModLoader;
// using Terraria;
//
// namespace SiriusMod.Content.Items.Placeable.Lab
// {
//     public class Server : ModItem
//     {
//         public override void SetDefaults()
//         {
//             Item.width = 16;
//             Item.height = 28;
//             Item.maxStack = Item.CommonMaxStack;
//             Item.useTurn = true;
//             Item.autoReuse = true;
//             Item.useAnimation = 15;
//             Item.useTime = 10;
//             Item.useStyle = ItemUseStyleID.Swing;
//             Item.consumable = true;
//             Item.createTile = Mod.Find<ModTile>(GetType().Name).Type;
//         }
//
//         public override void AddRecipes()
//         {
//             CreateRecipe()
//                 .AddRecipeGroup("IronBar", 2)
//                 .AddTile(TileID.Anvils)
//                 .Register();
//         }
//     }
// }