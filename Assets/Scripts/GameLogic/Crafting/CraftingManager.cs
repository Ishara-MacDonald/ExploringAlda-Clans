using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Can craft or not?
// Has recipe or not?

// To figure this out: need materials and method.


public class CraftingManager
{
    public static CraftingManager craftingManager;
    private RecipeBook recipeBook;

    public CraftingManager()
    {
        if (craftingManager != null) Debug.LogError("Can only have one Crafting Manager");
        craftingManager = this;
        recipeBook = new();
    }

    public void AddRecipe(Recipe recipe)
    {
        recipeBook.AddRecipe(recipe);
    }

    public bool ProcessItem(List<CraftingMaterial> materials, CraftingMethod method)
    {
        if (recipeBook == null) return false;
        List<ItemDataSO> items = materials.Select(item => item.ItemData).ToList();
        Recipe recipe = recipeBook.GetLinkedRecipe(materials, method);
        if (recipe == null) return false;

        InventoryManager.inventoryManager.AddItems(recipe.CraftedItems);

        return true;
    }

    public void OnAddItem()
    {

    }

    public void OnRemoveItem()
    {

    }

    public void OnGetItems()
    {

    }

    public void OnStartCrafting()
    {

    }

    public void OnStopCrafting()
    {

    }

    public void OnProcessItems()
    {

    }
}