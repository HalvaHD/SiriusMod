using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Twig.Content.Items.Accessories;

namespace Twig.Core.Systems
{
    public class RecipeSystem : ModSystem
    {
        public RecipeGroup group;
        public override void AddRecipeGroups()
        {
            group = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.AnkletoftheWind)}", ItemID.AnkletoftheWind, ModContent.ItemType<RainyBracelet>());
            RecipeGroup.RegisterGroup(nameof(ItemID.AnkletoftheWind), group);
        }

        public override void PostAddRecipes()
        {
            
            for (int i = 0; i < Recipe.numRecipes; i++) {
                Recipe recipe = Main.recipe[i];
                // Заменяет Браслет ветра в крафте на группу предметов (Браслет из ваниллы + Браслет забытого дождя)
                if (recipe.TryGetResult(ItemID.LightningBoots, out _)) {
                    recipe.RemoveIngredient(ItemID.AnkletoftheWind);
                    recipe.AddRecipeGroup(group);
                }
                if (recipe.TryGetResult(ItemID.FrostsparkBoots, out _)) {
                    recipe.RemoveIngredient(ItemID.AnkletoftheWind);
                    recipe.AddRecipeGroup(group);
                }
            }
            
           
        }
    }
}