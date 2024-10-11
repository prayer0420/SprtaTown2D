using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCData", menuName = "NPC/NPC Data")]
public class NPCDataSO : ScriptableObject
{
    public string npcName;
    public Sprite npcSprite;
    public DialogueSO dialogue;

    // NPC가 제공하는 퀘스트 목록
    public QuestSO[] quests;
}
