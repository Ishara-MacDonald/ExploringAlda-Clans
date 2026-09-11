using UnityEngine;

[RequireComponent(typeof(InventorySystem))]
public class CraftingSystemUI : MonoBehaviour
{
    [SerializeField] private RecipeInfoUI recipeInfoUI;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] private InventorySystem craftingInventory;

    void Start()
    {
        craftingInventory = GetComponent<InventorySystem>();
    }

    public void OnOpenCraftingSystem(InventorySystem inventorySystem)
    {
        inventoryUI.gameObject.SetActive(true);
        craftingInventory.SetInventorySlot(inventorySystem.InventorySlots);

        inventoryUI.OnOpenInventory(craftingInventory);
        recipeInfoUI.OpenRecipeInfo();
    }

    public void AddProcessedItem(ItemDataSO item)
    {
        craftingInventory.AddItem(item);
    }

    public void OnResetItems()
    {
        CraftingVisualManager.Instance.OnCraftingReset();
        RefreshInventoryDisplay();
    }

    public void RemoveItem(ItemDataSO item)
    {
        craftingInventory.RemoveItem(item);
        inventoryUI.UpdateUI();
    }

    public void RefreshInventoryDisplay() => inventoryUI.UpdateUI();

    public void OnCloseCraftingSystem()
    {
        CraftingVisualManager.Instance.OnCraftingTableClosed();
        inventoryUI.OnCloseInventory();
        gameObject.SetActive(false);
    }

    public void ShowRecipeInfo(Recipe recipe)
    {
        recipeInfoUI.SetRecipeInfo(recipe);
    }
}