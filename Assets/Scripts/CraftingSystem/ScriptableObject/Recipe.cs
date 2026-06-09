using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] private string recipeName;
    [SerializeField] private ItemDataSO[] input;
    [SerializeField] private ItemDataSO[] output;

    public string RecipeName => recipeName;
    public ItemDataSO[] Components => input;
    public ItemDataSO[] CraftedItems => output;
}
