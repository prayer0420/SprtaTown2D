using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest/Quest")]
public class QuestSO : ScriptableObject
{
    public string questName;
    public string description;
    public int experienceReward;
    public List<ItemSO> itemRewards;
    public QuestObjective objective;
    public string thankYouMessage; // NPC의 감사 인사 메시지
    public int goldReward; // 골드 보상

}

[System.Serializable]
public class QuestObjective
{
    public ObjectiveType objectiveType;
    public string targetName;
    public int targetCount;
    public int currentCount;
    public bool IsComplete; // 퀘스트 완료 여부를 나타내는 변수 추가
}
public enum ObjectiveType
{
    Kill,
    Collect,
    Explore
}
