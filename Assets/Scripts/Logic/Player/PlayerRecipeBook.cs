using System.Collections.Generic;
using UnityEngine;

public class PlayerRecipeBook : MonoBehaviour
{
    [SerializeField] private DictionarySerializer<CraftingMethod, List<Recipe>> serializedRecipeBook;
    private Dictionary<CraftingMethod, List<Recipe>> recipeBook;

    void Awake()
    {
        recipeBook = serializedRecipeBook.ToDictionary();
    }

    public List<Recipe> GetRecipes(CraftingMethod method)
    {
        if (recipeBook.TryGetValue(method, out List<Recipe> recipes)) return recipes;

        Debug.LogWarning($"No recipe book entry for {method} — check the recipe book Inspector setup.");
        return new List<Recipe>();
    }
}
