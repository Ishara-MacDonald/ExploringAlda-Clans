
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(QuestObjective))]
public class QuestLineEditor : PropertyDrawer
{
    #region 
    SerializedProperty quests;
    SerializedProperty objectiveName;
    SerializedProperty objectiveType;
    SerializedProperty objectiveDescription;
    SerializedProperty _property;
    #endregion

    private PropertyField questObjectiveType;

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        SerializedProperty nameProperty = property.FindPropertyRelative("name");
        SerializedProperty descriptionProperty = property.FindPropertyRelative("description");
        objectiveType = property.FindPropertyRelative("type");
        // Create property container element.
        VisualElement root = new();
        // Create property fields.
        var name = new PropertyField(nameProperty);
        var description = new PropertyField(descriptionProperty);
        var type = new PropertyField(objectiveType);
        type.RegisterValueChangeCallback((changeEvent) => CheckForHide());

        questObjectiveType = new PropertyField();

        // Add fields to the container.
        root.Add(name);
        root.Add(description);

        root.Add(type);
        root.Add(questObjectiveType);

        return root;
    }

    private void CheckForHide()
    {
        QuestObjectiveType enumType = (QuestObjectiveType)objectiveType.enumValueIndex;
        switch (enumType)
        {
            case QuestObjectiveType.Collect:
                {
                    TextField textField = new()
                    {
                        label = "ajdaosjiod"
                    };
                    Label label = new()
                    {
                        text = "hi"
                    };
                    questObjectiveType.Add(label);
                    Button button = new();
                    Button aaa = new();
                    questObjectiveType.Add(button);
                    questObjectiveType.Add(textField);
                    questObjectiveType.Add(aaa);
                    break;
                }
            case QuestObjectiveType.Interact:
                {
                    Button button = new();
                    Button aaa = new();
                    questObjectiveType.Add(button);
                    questObjectiveType.Add(aaa);
                    break;
                }
            case QuestObjectiveType.Locate:
                {
                    Button button = new();
                    Button aaa = new();
                    questObjectiveType.Add(button);
                    questObjectiveType.Add(aaa);
                    break;
                }
            default:
                {
                    Debug.Log("aaaaa");
                    break;
                }
        }

    }

}
