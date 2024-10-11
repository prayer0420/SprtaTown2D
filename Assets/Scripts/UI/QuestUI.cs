using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    public Text questNameText;
    public Button questButton; 
    private QuestSO questSO;

    public void Start()
    {
        questButton.onClick.AddListener(OnQuestItemClicked);

    }
    // 퀘스트 정보를 받아 UI에 설정
    public void SetQuestSO(QuestSO quest)
    {
        questSO = quest;
        questNameText.text = quest.questName;

        questButton.onClick.AddListener(OnQuestItemClicked); 
    }

    // 버튼 클릭 시 호출됨
    public void OnQuestItemClicked()
    {
        QuestUIManager.Instance.ShowQuestDetails(questSO);
    }
}
