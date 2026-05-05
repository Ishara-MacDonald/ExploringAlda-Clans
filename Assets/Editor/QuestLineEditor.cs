
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(QuestObjective))]
public class QuestLineEditor : PropertyDrawer
{
    SerializedProperty objectiveType;
    private PropertyField questObjectiveDetails;

    #region QuestType Groups
    VisualElement collectGroup;
    VisualElement interactableGroup;
    VisualElement locateGroup;
    #endregion

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        objectiveType = property.FindPropertyRelative("type");
        PropertyField type = new(objectiveType);

        type.RegisterValueChangeCallback((changeEvent) => CheckForType());

        collectGroup = new();
        collectGroup.Add(new PropertyField(property.FindPropertyRelative("itemData")));
        collectGroup.Add(new PropertyField(property.FindPropertyRelative("gatherAmount")));

        interactableGroup = new();
        interactableGroup.Add(new PropertyField(property.FindPropertyRelative("interactable")));

        locateGroup = new();
        locateGroup.Add(new PropertyField(property.FindPropertyRelative("position")));
        locateGroup.Add(new PropertyField(property.FindPropertyRelative("range")));

        questObjectiveDetails = new PropertyField();
        questObjectiveDetails.Add(collectGroup);
        questObjectiveDetails.Add(interactableGroup);
        questObjectiveDetails.Add(locateGroup);
        VisualElement root = new();

        root.Add(new PropertyField(property.FindPropertyRelative("name")));
        root.Add(new PropertyField(property.FindPropertyRelative("description")));

        root.Add(type);
        root.Add(questObjectiveDetails);

        ToggleButtonGroup grouptest = new();

        return root;
    }

    private void CheckForType()
    {
        QuestObjectiveType enumType = (QuestObjectiveType)objectiveType.enumValueIndex;
        switch (enumType)
        {
            case QuestObjectiveType.Collect:
                {
                    collectGroup.style.display = DisplayStyle.Flex;
                    interactableGroup.style.display = DisplayStyle.None;
                    locateGroup.style.display = DisplayStyle.None;
                    break;
                }
            case QuestObjectiveType.Interact:
                {
                    collectGroup.style.display = DisplayStyle.None;
                    interactableGroup.style.display = DisplayStyle.Flex;
                    locateGroup.style.display = DisplayStyle.None;
                    break;
                }
            case QuestObjectiveType.Locate:
                {
                    collectGroup.style.display = DisplayStyle.None;
                    interactableGroup.style.display = DisplayStyle.None;
                    locateGroup.style.display = DisplayStyle.Flex;
                    break;
                }
        }

    }

}
