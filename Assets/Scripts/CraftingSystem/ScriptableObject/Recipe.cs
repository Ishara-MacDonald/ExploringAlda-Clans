using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] private string recipeName;
    [SerializeField] private CraftingComponent[] components;
    [SerializeField] private ItemDataSO[] craftedItems;

    public string RecipeName => recipeName;
    public CraftingComponent[] Components => components;
    public ItemDataSO[] CraftedItems => craftedItems;
}
