
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
    [SerializeField] private string name;
    [SerializeField] private QuestObjectiveType type;
    [SerializeField][TextArea] private string description;

    public string Name => name;
    public string Description => description;
}
