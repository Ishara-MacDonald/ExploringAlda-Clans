
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerQuestList : MonoBehaviour
{
    [SerializeField] private QuestSystem questSystem;
    private InputAction questListAction;

    private bool isQuestListOpen;

    void Awake()
    {
        questSystem = new();
        isQuestListOpen = false;
        questListAction = InputSystem.actions.FindAction("Quests");
    }

    public QuestSystem GetQuestSystem => questSystem;

    private void OnEnable()
    {
        questListAction.Enable();
        questListAction.started += OnQuestListOpen;
    }

    private void OnDisable()
    {
        questListAction.started -= OnQuestListOpen;
        questListAction.Disable();
    }
    private void OnQuestListOpen(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isQuestListOpen) GameManager.manager.CloseQuestList();
            else GameManager.manager.OpenQuestList(questSystem);

            isQuestListOpen = !isQuestListOpen;
        }
    }
}