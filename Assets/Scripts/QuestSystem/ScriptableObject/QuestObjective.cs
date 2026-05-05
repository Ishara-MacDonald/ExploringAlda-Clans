
using System;
using UnityEngine;

public enum QuestObjectiveType
{
    Collect,
    Locate,
    Interact
}

[Serializable]
public class QuestObjective
{
    #region General Info
    [SerializeField] private string name;
    [SerializeField] private QuestObjectiveType type;
    [SerializeField][TextArea] private string description;
    #endregion

    #region Gather
    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private int gatherAmount;
    #endregion

    #region Locate Quest
    [SerializeField] private Transform position;
    [SerializeField] private int range;
    #endregion

    #region Interact Quest
    [SerializeField] private Interactable interactable;
    #endregion

    public string Name => name;
    public string Description => description;
    public InventoryItemData Item => itemData;
    public QuestObjectiveType Type => type;
    public int Amount => gatherAmount;
}
