using UnityEngine;

[CreateAssetMenu(fileName = "Component", menuName = "Scriptable Objects/Component")]
public class CraftingComponent : ScriptableObject
{
    [SerializeField] private ItemDataSO material;
    [SerializeField] private int amount;
    [SerializeField] private bool consumable;

    public ItemDataSO Material => material;
    public int Amount => amount;
    public bool IsConsumable => consumable;
}