using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CraftingMethod
{
    Picking,
    Grinding
}

[Serializable]
public class CraftingSystem
{
    public static CraftingSystem craftingSystem;

    private CraftingTable currentTable;
    private List<ItemDataSO> craftingItems;
    private List<Recipe> recipeList;
    private PlayerRecipeBook recipeBookRef;
    private CraftingMethod currentMethod;

    public CraftingSystem()
    {
        if (craftingSystem != null) return;
        craftingSystem = this;
        craftingItems = new();
        currentMethod = CraftingMethod.Picking;
        recipeList = new();
    }

    public void OnCraftingTableOpen(CraftingTable craftingTable)
    {
        currentTable = craftingTable;
        recipeBookRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRecipeBook>();
        recipeList = recipeBookRef.GetRecipes(CraftingMethod.Picking);
    }

    public void OnCraftingTableClose()
    {
        craftingItems = new();
        currentTable.OnClose();
        currentTable = null;
    }

    public void OnResetItems()
    {
        craftingItems = new();
        currentTable.ResetItems();
    }

    public void SetCurrentMethod(CraftingMethod newMethod)
    {
        if (currentMethod != newMethod)
        {
            currentMethod = newMethod;
            recipeList = recipeBookRef.GetRecipes(currentMethod);
        }
    }

    // availableAmount is how many of this item are still available to stage right now
    // (i.e. real amount minus whatever's already staged) — not the player's total.
    public void AddItem(ItemDataSO newItem, int availableAmount)
    {
        if (currentTable == null) return;
        if (availableAmount <= 0) return;

        craftingItems.Add(newItem);
        currentTable.AddMaterial(newItem);
        LogicManager.manager.OnCraftingItemsStaged();
    }

    // How many of this item are currently pulled out onto the table (staged for
    // crafting) but not yet actually removed from the player's real inventory.
    public int GetStagedAmount(ItemDataSO item) => craftingItems.Count(i => i.Equals(item));

    // Called once a staged item is actually consumed by a successful craft, so
    // staged bookkeeping doesn't outlive the items it was tracking.
    public void ReleaseStagedItem(ItemDataSO item)
    {
        int index = craftingItems.FindIndex(i => i.Equals(item));
        if (index >= 0) craftingItems.RemoveAt(index);
    }

    public bool ProcessItem(List<CraftingMaterial> materials)
    {
        List<ItemDataSO> items = materials.Select(item => item.ItemData).ToList();
        Recipe recipe = recipeList.Find(recipe => recipe.IsRecipe(items) == true);
        if (recipe == null)
        {
            Debug.LogWarning($"No matching {currentMethod} recipe for: {string.Join(", ", items.Select(item => item.itemName))}");
            return false;
        }

        foreach (ItemDataSO item in recipe.CraftedItems)
        {
            LogicManager.manager.OnAddProcessItem(item);
        }
        return true;
    }
}
