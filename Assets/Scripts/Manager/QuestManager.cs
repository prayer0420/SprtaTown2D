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



    // ����Ʈ ����
    public void AcceptQuest(QuestSO quest)
    {
        //�������̰ų� �Ϸ�� ����Ʈ�� �ƴ϶��(�ƿ�ó��)
        if (!activeQuests.Contains(quest) && !completedQuests.Contains(quest))
        {
            //������ ����Ʈ��Ͽ� �߰�
            activeQuests.Add(quest);
            //����Ʈ ������Ʈ�� �߻��� �̺�Ʈ ȣ��
            OnQuestUpdated?.Invoke();
            Debug.Log($"{quest.questName} ����Ʈ�� �����߽��ϴ�.");
        }
    }

    public void CompleteQuest(QuestSO quest)
    {
        if (activeQuests.Contains(quest))
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
            quest.objective.IsComplete = false; // �Ϸ� ���� �ʱ�ȭ
            OnQuestUpdated?.Invoke();

            GameManager.Instance.Player.Gold += quest.goldReward;
            // �κ��丮�� ���� quest.itemRewards
            Debug.Log($"{quest.questName} ����Ʈ�� �Ϸ��߽��ϴ�.");
            
        }
    }



    // ����Ʈ ���� ��Ȳ ������Ʈ
    public void UpdateQuestProgress(ObjectiveType type, string targetName, int amount)
    {
        //�������� ����Ʈ���� ��ȸ
        foreach (var quest in activeQuests)
        {
            //������Ʈ�� ����Ʈ�� �������� ���
            if (quest.objective.objectiveType == type && quest.objective.targetName == targetName)
            {
                //���� �÷���
                quest.objective.currentCount += amount;
                //���� ���� ��ǥ���� �Ѿ�ٸ� ��ǥ �޼�, ����Ʈ �Ϸ�!
                if (quest.objective.currentCount >= quest.objective.targetCount)
                {
                    quest.objective.currentCount = quest.objective.targetCount;
                    quest.objective.IsComplete = true; // ����Ʈ�� �Ϸ��� �� ������ ǥ��
                    Debug.Log($"{quest.questName} ����Ʈ�� ��ǥ�� �޼��߽��ϴ�. NPC���� ���ư�����.");
                }

                OnQuestUpdated?.Invoke(); // ����Ʈ UI ������Ʈ
            }
        }
    }

    // ����Ʈ ���� ����
    public bool HasQuest(QuestSO quest)
    {
        return activeQuests.Contains(quest);
    }

    // ����Ʈ �Ϸ� ����
    public bool IsQuestCompleted(QuestSO quest)
    {
        return completedQuests.Contains(quest);
    }

    // ����Ʈ ��ǥ �޼� ����
    public bool IsQuestObjectiveCompleted(QuestSO quest)
    {
        return quest.objective.currentCount >= quest.objective.targetCount;
    }
}
