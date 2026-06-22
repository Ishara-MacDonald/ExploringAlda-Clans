

using System.Collections.Generic;
using UnityEngine;

public class RecipeBook
{

    private List<Recipe> recipes;

    public RecipeBook()
    {
        recipes = new();
    }

    public void AddRecipe(Recipe recipe)
    {
        recipes.Add(recipe);
    }

    public Recipe GetLinkedRecipe(List<OCraftingMaterial> materials, CraftingMethod method)
    {
        // Find(recipe => recipe.IsRecipe(items) == true)
        return ScriptableObject.CreateInstance<Recipe>();
    }

}