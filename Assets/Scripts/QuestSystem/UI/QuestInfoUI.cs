using TMPro;
using UnityEngine;

public class QuestInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTxt;
    [SerializeField] private TextMeshProUGUI descriptionTxt;
    [SerializeField] private TextMeshProUGUI objectiveNameTxt;
    [SerializeField] private TextMeshProUGUI objectiveDescrTxt;

    public void SetQuestInfoUI(QuestProgress line)
    {
        Quest quest = line.GetCurrentQuest();
        titleTxt.SetText(quest.QuestName);
        descriptionTxt.SetText(quest.QuestDescription);
        objectiveNameTxt.SetText(line.GetCurrentObjective().Name);
        objectiveDescrTxt.SetText(line.GetCurrentObjective().Description);
    }
}
