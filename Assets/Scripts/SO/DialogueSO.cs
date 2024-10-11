using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue/Dialogue")]
public class DialogueSO : ScriptableObject
{
    public DialogueLine[] dialogueLines;
}

[System.Serializable]
public class DialogueLine
{
    [TextArea]
    public string dialogueText;
}
