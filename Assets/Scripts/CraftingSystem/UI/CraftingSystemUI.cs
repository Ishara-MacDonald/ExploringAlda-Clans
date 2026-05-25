using System;
using UnityEngine;

public class CraftingSystemUI : MonoBehaviour
{
    [SerializeField] private RecipeInfoUI recipeInfoUI;
    [SerializeField] private InventoryUI simpleInventory;
    public static event Action CloseCraftingSystem;

    public void OnOpenCraftingSystem(InventorySystem inventorySystem)
    {
        simpleInventory.gameObject.SetActive(true);
        simpleInventory.OnOpenInventory(inventorySystem);
        recipeInfoUI.OpenRecipeInfo();
    }

    public void OnCloseCraftingSystem()
    {
        CloseCraftingSystem?.Invoke();
        simpleInventory.OnCloseInventory();
        gameObject.SetActive(false);
    }

    public void ShowRecipeInfo(Recipe recipe)
    {
        recipeInfoUI.SetRecipeInfo(recipe);
    }
}