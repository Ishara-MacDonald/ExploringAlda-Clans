using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Can craft or not?
// Has recipe or not?

// To figure this out: need materials and method.

public class CraftingManager
{
    public static CraftingManager craftingManager;
    public InventoryManager inventoryManager;
    private RecipeBook recipeBook;

    public CraftingManager(InventoryManager _inventoryManager)
    {
        if (craftingManager != null) Debug.LogError("Can only have one Crafting Manager");
        craftingManager = this;
        recipeBook = new();
        inventoryManager = _inventoryManager;
    }

    public void AddRecipe(Recipe recipe)
    {
        recipeBook.AddRecipe(recipe);
    }

    public bool ProcessItem(List<OCraftingMaterial> materials, CraftingMethod method)
    {
        if (recipeBook == null) return false;
        List<ItemDataSO> items = materials.Select(item => item.ItemData).ToList();
        Recipe recipe = recipeBook.GetLinkedRecipe(materials, method);
        if (recipe == null) return false;

        inventoryManager.RemoveItems(items);
        inventoryManager.AddItems(recipe.CraftedItems);

        return true;
    }
}