using System;
using System.Collections;
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
    private readonly List<ItemDataSO> craftingItems;
    private List<Recipe> recipeList;
    private PlayerRecipeBook recipeBookRef;
    private CraftingMethod currentMethod;

    public CraftingSystem()
    {
        if (craftingSystem != null) return;
        craftingSystem = this;
        craftingItems = new();
        currentMethod = CraftingMethod.Picking;

        CraftingTable.ContextOpened += OnCraftingTableOpen;
        CraftingSystemUI.CloseCraftingSystem += OnCraftingTableClose;
        recipeList = new();
    }

    private void OnCraftingTableOpen(CraftingTable craftingTable)
    {
        InventorySlotUI.ItemInventoryInteracted += AddItem;
        currentTable = craftingTable;
        recipeBookRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerRecipeBook>();
        recipeList = recipeBookRef.GetRecipes(CraftingMethod.Picking);
    }

    private void OnCraftingTableClose()
    {
        InventorySlotUI.ItemInventoryInteracted -= AddItem;
        currentTable.OnClose();
    }

    public void SetCurrentMethod(CraftingMethod newMethod)
    {
        if (currentMethod != newMethod)
        {
            currentMethod = newMethod;
            recipeList = recipeBookRef.GetRecipes(currentMethod);
        }
    }
    public void AddItem(ItemDataSO newItem, int hasAmount)
    {
        if (craftingItems.Count > 0)
        {
            int currentAmount = craftingItems.FindAll((item) => item.Equals(newItem)).Count;
            if (hasAmount > currentAmount)
            {
                craftingItems.Add(newItem);
                currentTable.AddMaterial(newItem);
            }
        }
        else if (craftingItems.Count == 0)
        {
            craftingItems.Add(newItem);
            currentTable.AddMaterial(newItem);
        }
    }

    public bool ProcessItem(List<CraftingMaterial> materials)
    {
        List<ItemDataSO> items = materials.Select(item => item.ItemData).ToList();
        Recipe recipe = recipeList.Find(recipe => recipe.IsRecipe(items) == true);
        if (recipe == null) return false;

        foreach (ItemDataSO item in recipe.CraftedItems)
        {
            currentTable.AddMaterial(item);
        }

        return true;
    }
}
