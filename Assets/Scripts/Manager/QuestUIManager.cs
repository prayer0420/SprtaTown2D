using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestUIManager : MonoBehaviour
{
    public static QuestUIManager Instance;

    // NPC 퀘스트 목록을 표시하는 캔버스
    public GameObject npcQuestCanvas;
    public Transform questListContent_NPC;  // NPC 퀘스트 목록 콘텐츠

    // 플레이어 퀘스트 목록을 표시하는 캔버스 및 ScrollView
    public GameObject playerQuestCanvas;
    public GameObject activeQuestScrollView;  // 진행 중인 퀘스트 ScrollView
    public GameObject completedQuestScrollView;  // 완료된 퀘스트 ScrollView

    public Transform activeQuestListContent;  // 진행 중인 퀘스트 콘텐츠
    public Transform completedQuestListContent;  // 완료된 퀘스트 콘텐츠

    // 퀘스트 항목을 표시할 패널
    public GameObject QuestUI;
    public GameObject activeQuestPanelPrefab;  // 진행 중인 퀘스트 패널 프리팹
    public GameObject completedQuestPanelPrefab;  // 완료된 퀘스트 패널 프리팹

    // 퀘스트 목록을 필터링하는 토글 버튼
    public Toggle activeQuestsToggle;  // 진행 중인 퀘스트 토글
    public Toggle completedQuestsToggle;  // 완료된 퀘스트 토글

    // 퀘스트 상세 정보 패널
    public GameObject questDetailsPanel;
    public Text questNameText;
    public Text questDescriptionText;
    public Text questProgressText;  // 진행 상황을 표시하는 텍스트
    public Button acceptButton;
    public Button declineButton;

    // 퀘스트 완료 UI 패널
    public GameObject completeQuestPanel;
    public Text thankYouText;
    public Text itemRewardText;
    public Text goldRewardText;
    public Button completeButton;


    private NPCDataSO currentNPCData;  // 현재 상호작용 중인 NPC 데이터
    private QuestSO currentQuest;  // 현재 선택한 퀘스트

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // ScrollView 초기 비활성화
        activeQuestScrollView.SetActive(false);
        completedQuestScrollView.SetActive(false);

        // 토글 이벤트 설정
        activeQuestsToggle.onValueChanged.AddListener((isOn) => ToggleActiveQuests(isOn));
        completedQuestsToggle.onValueChanged.AddListener((isOn) => ToggleCompletedQuests(isOn));

        // 수락, 거절 버튼 이벤트 설정
        acceptButton.onClick.AddListener(AcceptQuest);
        declineButton.onClick.AddListener(DeclineQuest);

        // 퀘스트 업데이트 이벤트
        QuestManager.Instance.OnQuestUpdated += ShowActiveAndCompletedQuests;
    }

    // NPC와 상호작용할 때 호출되는 함수로 NPC의 퀘스트 목록을 표시
    public void ShowQuestList(NPCDataSO npcData)
    {
        currentNPCData = npcData;
        npcQuestCanvas.SetActive(true);  // NPC 퀘스트 창을 활성화
        UpdateQuestList_NPC();  // NPC 퀘스트 목록을 업데이트
    }

    // NPC의 퀘스트 목록을 업데이트하는 함수
    public void UpdateQuestList_NPC()
    {
        foreach (Transform child in questListContent_NPC)
        {
            Destroy(child.gameObject);
        }

        if (currentNPCData != null)
        {
            foreach (var quest in currentNPCData.quests)
            {
                GameObject questItemObj = Instantiate(QuestUI, questListContent_NPC);  
                QuestUI questUI = questItemObj.GetComponent<QuestUI>();
                questUI.SetQuestSO(quest);
                questUI.questButton.interactable = true;
                //if (QuestManager.Instance.HasQuest(quest))
                //{
                //    questUI.questButton.interactable = false;  // 이미 수락한 퀘스트는 비활성화
                //}
                //else
                //{
                //    questUI.questButton.interactable = true;  // 수락 가능한 퀘스트는 활성화
                //}
            }
        }
    }

    // 플레이어 퀘스트 목록(진행 중/완료된)을 업데이트하는 함수
    public void ShowActiveAndCompletedQuests()
    {
        // 진행 중인 퀘스트 목록 업데이트
        UpdateQuestList(QuestManager.Instance.activeQuests, activeQuestListContent, activeQuestPanelPrefab);

        // 완료된 퀘스트 목록 업데이트
        UpdateQuestList(QuestManager.Instance.completedQuests, completedQuestListContent, completedQuestPanelPrefab);
    }

    // 플레이어의 특정 퀘스트 목록을 ScrollView에 업데이트
    private void UpdateQuestList(List<QuestSO> quests, Transform content, GameObject questPanelPrefab)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach (var quest in quests)
        {
            GameObject questItemObj = Instantiate(questPanelPrefab, content);  // QuestPanelPrefab을 사용하여 퀘스트 항목 생성
            SetQuestPanel(questItemObj, quest);                                // 생성된 패널에 퀘스트 정보를 설정
        }
    }

    // 플레이어 퀘스트 패널에 퀘스트 정보를 설정
    private void SetQuestPanel(GameObject questItemObj, QuestSO quest)
    {
        Text questNameText = questItemObj.transform.Find("QuestNameText").GetComponent<Text>();  // 퀘스트 이름 텍스트
        Text questDescText = questItemObj.transform.Find("QuestDescText").GetComponent<Text>();  // 퀘스트 설명 텍스트
        Text questProgressText = questItemObj.transform.Find("QuestProgressText").GetComponent<Text>();  // 퀘스트 진행 상황 텍스트

        questNameText.text = quest.questName;
        questDescText.text = quest.description;
        questProgressText.text = $"{quest.objective.currentCount}/{quest.objective.targetCount}";  // 진행 상황 표시
    }

    // 진행 중인 퀘스트 토글이 켜졌을 때 ScrollView 활성화
    private void ToggleActiveQuests(bool isOn)
    {
        if (isOn)
        {
            activeQuestScrollView.SetActive(true);
            UpdateQuestList(QuestManager.Instance.activeQuests, activeQuestListContent, activeQuestPanelPrefab);
        }
        else
        {
            activeQuestScrollView.SetActive(false);
        }
    }

    // 완료된 퀘스트 토글이 켜졌을 때 ScrollView 활성화
    private void ToggleCompletedQuests(bool isOn)
    {
        if (isOn)
        {
            completedQuestScrollView.SetActive(true);
            UpdateQuestList(QuestManager.Instance.completedQuests, completedQuestListContent, completedQuestPanelPrefab);
        }
        else
        {
            completedQuestScrollView.SetActive(false);
        }
    }

    // 퀘스트 상세 정보를 표시
    public void ShowQuestDetails(QuestSO quest)
    {
        currentQuest = quest;

        // 퀘스트가 완료되었고, 플레이어가 해당 퀘스트를 가지고 있는 경우
        if (quest.objective.IsComplete && QuestManager.Instance.HasQuest(quest))
        {
            ShowCompleteQuestUI(quest); // 퀘스트 완료 UI 표시
        }
        else
        {
            // 기존 상세 정보 패널 표시 로직
            questNameText.text = quest.questName;
            questDescriptionText.text = quest.description;
            questProgressText.text = $"{quest.objective.currentCount}/{quest.objective.targetCount}";

            questDetailsPanel.SetActive(true);

            if (QuestManager.Instance.HasQuest(quest) || QuestManager.Instance.IsQuestCompleted(quest))
            {
                acceptButton.interactable = false;
            }
            else
            {
                acceptButton.interactable = true;
            }
        }
    }
    public void ShowCompleteQuestUI(QuestSO quest)
    {
        completeQuestPanel.SetActive(true);

        // 감사 인사 메시지 설정
        thankYouText.text = $"{currentNPCData.npcName}: {quest.thankYouMessage}";
        // 보상 정보 표시
        itemRewardText.text += "[아이템 보상]\n";
        foreach (var item in quest.itemRewards)
        {
            itemRewardText.text += $"- {item.itemName}\n";
        }
        goldRewardText.text = $"[골드 보상]\n {quest.goldReward} Gold";

        // 완료 버튼 이벤트 설정
        completeButton.onClick.RemoveAllListeners();
        completeButton.onClick.AddListener(() => CompleteQuest());
    }


    //실제 완료 로직은 QuestManger에서!
    private void CompleteQuest()
    {
        if (currentQuest != null)
        {
            QuestManager.Instance.CompleteQuest(currentQuest);
            completeQuestPanel.SetActive(false);
            HideQuestList();
        }
    }


    // 퀘스트 수락
    private void AcceptQuest()
    {
        if (currentQuest != null)
        {
            QuestManager.Instance.AcceptQuest(currentQuest);
            questDetailsPanel.SetActive(false);
            HideQuestList();
        }
    }

    // 퀘스트 거절
    private void DeclineQuest()
    {
        questDetailsPanel.SetActive(false);
    }

    // NPC 퀘스트 목록을 숨기는 함수
    public void HideQuestList()
    {
        npcQuestCanvas.SetActive(false);
        questDetailsPanel.SetActive(false);
    }

    // Q 버튼으로 플레이어 퀘스트 목록을 토글
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (playerQuestCanvas.activeSelf)
            {
                playerQuestCanvas.SetActive(false);
            }
            else
            {
                playerQuestCanvas.SetActive(true);
                ShowActiveAndCompletedQuests();
            }
        }

        if (Input.GetKey(KeyCode.V))
        {
            currentQuest.objective.IsComplete = true;
            Debug.Log($"{currentQuest} is complete!");
        }
    }
}
