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
        return recipeBook[method];
    }
}
