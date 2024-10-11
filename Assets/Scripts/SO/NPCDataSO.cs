using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCData", menuName = "NPC/NPC Data")]
public class NPCDataSO : ScriptableObject
{
    public string npcName;
    public Sprite npcSprite;
    public DialogueSO dialogue;

    // NPC�� �����ϴ� ����Ʈ ���
    public QuestSO[] quests;
}
