using TMPro;
using UnityEngine;

public class QuestInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTxt;
    [SerializeField] private TextMeshProUGUI descriptionTxt;
    [SerializeField] private TextMeshProUGUI objectiveNameTxt;
    [SerializeField] private TextMeshProUGUI objectiveDescrTxt;
    [SerializeField] private TextMeshProUGUI amountTxt;

    public void SetQuestInfoUI(QuestProgress line)
    {
        Quest quest = line.GetCurrentQuest();
        titleTxt.SetText(quest.QuestName);
        descriptionTxt.SetText(quest.QuestDescription);

        QuestObjective objective = line.GetCurrentObjective();
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
}
