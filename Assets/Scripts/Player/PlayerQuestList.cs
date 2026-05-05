
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerQuestList : MonoBehaviour
{
    [SerializeField] private QuestSystem questSystem;
    private InputAction questListAction;

    void Awake()
    {
        questSystem = new();
        questListAction = InputSystem.actions.FindAction("Quests");
    }

    public QuestSystem GetQuestSystem => questSystem;

    private void OnEnable()
    {
        questListAction.Enable();
        questListAction.performed += OnQuestListOpen;
    }

    private void OnDisable()
    {
        questListAction.performed -= OnQuestListOpen;
        questListAction.Disable();
    }

    private void OnQuestListOpen(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            GameManager.manager.OnQuestListToggle();
        }
    }
}