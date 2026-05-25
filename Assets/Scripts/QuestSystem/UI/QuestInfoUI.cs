using TMPro;
using UnityEngine;

public class QuestInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTxt;
    [SerializeField] private TextMeshProUGUI descriptionTxt;
    [SerializeField] private TextMeshProUGUI objectiveNameTxt;
    [SerializeField] private TextMeshProUGUI objectiveDescrTxt;
    [SerializeField] private TextMeshProUGUI amountTxt;

    public void ShowDefault()
    {
        titleTxt.SetText("No Active Quests");
        descriptionTxt.SetText("");

        objectiveNameTxt.SetText("");
        objectiveDescrTxt.SetText("Once you accept a quest, it'll show here.");

        amountTxt.gameObject.SetActive(false);
    }

    public void SetQuestInfoUI(QuestProgress line)
    {
        if (line.IsCompleted)
        {
            ShowCompletedText();
        }
        Quest quest = line.GetCurrentQuest();
        titleTxt.SetText(quest.QuestName);
        descriptionTxt.SetText(quest.QuestDescription);

        QuestObjective objective = line.CurrentObjective;
        objectiveNameTxt.SetText(objective.Name);
        objectiveDescrTxt.SetText(objective.Description);
        if (objective.Type == QuestObjectiveType.Collect)
        {
            amountTxt.gameObject.SetActive(true);
            string amountText = line.CurrentAmount + "/" + objective.Amount;
            amountTxt.SetText(amountText);
        }
        else
            amountTxt.gameObject.SetActive(false);
    }

    public void ShowCompletedText()
    {

    }
}
