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
    public string thankYouMessage; // NPC�� ���� �λ� �޽���
    public int goldReward; // ��� ����

}

[System.Serializable]
public class QuestObjective
{
    public ObjectiveType objectiveType;
    public string targetName;
    public int targetCount;
    public int currentCount;
    public bool IsComplete; // ����Ʈ �Ϸ� ���θ� ��Ÿ���� ���� �߰�
}
public enum ObjectiveType
{
    Kill,
    Collect,
    Explore
}
