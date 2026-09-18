using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Scriptable Objects/Recipe")]
public class Recipe : ScriptableObject
{
    [SerializeField] private string recipeName;
    [SerializeField] private List<ItemDataSO> input;
    [SerializeField] private List<ItemDataSO> output;

    public string RecipeName => recipeName;

    public List<ItemDataSO> Components => input;
    public List<ItemDataSO> CraftedItems => output;

    public bool IsRecipe(List<ItemDataSO> materials)
    {
        if (materials.Count != input.Count) return false;
        return Helpers.CompareLists(materials, input);
    }
}
