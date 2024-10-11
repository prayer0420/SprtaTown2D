using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;

    private void Start()
    {
    }

    // 대화 UI 표시
    public void ShowDialogue()
    {
        Debug.Log("대화창 표시 중");
        dialoguePanel.SetActive(true);
    }

    // 대화 UI 숨기기
    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("대화꺼짐?");
    }

    // 문장 표시
    public void DisplaySentence(string sentence)
    {
        dialogueText.text = sentence;
    }

    private void Update()
    {
        // Space 키로 다음 문장으로 넘어가기
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager.Instance.DisplayNextSentence();
        }
    }
}
