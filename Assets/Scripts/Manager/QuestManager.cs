using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public List<QuestSO> activeQuests = new List<QuestSO>();
    public List<QuestSO> completedQuests = new List<QuestSO>();

    public event Action OnQuestUpdated;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    // 퀘스트 수락
    public void AcceptQuest(QuestSO quest)
    {
        //진행중이거나 완료된 퀘스트가 아니라면(아예처음)
        if (!activeQuests.Contains(quest) && !completedQuests.Contains(quest))
        {
            //진행중 퀘스트목록에 추가
            activeQuests.Add(quest);
            //퀘스트 업데이트시 발생할 이벤트 호출
            OnQuestUpdated?.Invoke();
            Debug.Log($"{quest.questName} 퀘스트를 수락했습니다.");
        }
    }

    public void CompleteQuest(QuestSO quest)
    {
        if (activeQuests.Contains(quest))
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
            quest.objective.IsComplete = false; // 완료 상태 초기화
            OnQuestUpdated?.Invoke();

            GameManager.Instance.Player.Gold += quest.goldReward;
            // 인벤토리에 지급 quest.itemRewards
            Debug.Log($"{quest.questName} 퀘스트를 완료했습니다.");
            
        }
    }



    // 퀘스트 진행 상황 업데이트
    public void UpdateQuestProgress(ObjectiveType type, string targetName, int amount)
    {
        //진행중인 퀘스트들을 순회
        foreach (var quest in activeQuests)
        {
            //업데이트할 퀘스트와 같은것을 골라서
            if (quest.objective.objectiveType == type && quest.objective.targetName == targetName)
            {
                //값을 올려줌
                quest.objective.currentCount += amount;
                //현재 값이 목표값을 넘어섰다면 목표 달성, 퀘스트 완료!
                if (quest.objective.currentCount >= quest.objective.targetCount)
                {
                    quest.objective.currentCount = quest.objective.targetCount;
                    quest.objective.IsComplete = true; // 퀘스트를 완료할 수 있음을 표시
                    Debug.Log($"{quest.questName} 퀘스트의 목표를 달성했습니다. NPC에게 돌아가세요.");
                }

                OnQuestUpdated?.Invoke(); // 퀘스트 UI 업데이트
            }
        }
    }

    // 퀘스트 소유 여부
    public bool HasQuest(QuestSO quest)
    {
        return activeQuests.Contains(quest);
    }

    // 퀘스트 완료 여부
    public bool IsQuestCompleted(QuestSO quest)
    {
        return completedQuests.Contains(quest);
    }

    // 퀘스트 목표 달성 여부
    public bool IsQuestObjectiveCompleted(QuestSO quest)
    {
        return quest.objective.currentCount >= quest.objective.targetCount;
    }
}
