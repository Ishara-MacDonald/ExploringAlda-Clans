using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

[RequireComponent(typeof(InventorySystem))]
public class CraftingSystemUI : MonoBehaviour
{
    [SerializeField] private RecipeInfoUI recipeInfoUI;
    [SerializeField] private InventoryUI simpleInventory;
    [SerializeField] private InventorySystem craftingInventory;

    void Start()
    {
        craftingInventory = GetComponent<InventorySystem>();
    }

    public void OnOpenCraftingSystem(InventorySystem inventorySystem)
    {
        simpleInventory.gameObject.SetActive(true);
        craftingInventory.SetInventorySlot(inventorySystem.InventorySlots);

        simpleInventory.OnOpenInventory(craftingInventory);
        recipeInfoUI.OpenRecipeInfo();
    }

    public void AddProcessedItem(ItemDataSO item)
    {
        craftingInventory.AddItem(item);
    }

    public void RemoveItem(ItemDataSO item)
    {
        craftingInventory.RemoveItem(item);
        simpleInventory.OnCloseInventory();
        simpleInventory.OnOpenInventory(craftingInventory);
    }

    public void OnCloseCraftingSystem()
    {
        CraftingSystem.craftingSystem.OnCraftingTableClose();
        simpleInventory.OnCloseInventory();
        gameObject.SetActive(false);
    }

    public void ShowRecipeInfo(Recipe recipe)
    {
        recipeInfoUI.SetRecipeInfo(recipe);
    }
}