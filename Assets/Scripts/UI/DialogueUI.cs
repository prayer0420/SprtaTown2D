using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;

    private void Start()
    {
    }

    // ��ȭ UI ǥ��
    public void ShowDialogue()
    {
        Debug.Log("��ȭâ ǥ�� ��");
        dialoguePanel.SetActive(true);
    }

    // ��ȭ UI �����
    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        Debug.Log("��ȭ����?");
    }

    // ���� ǥ��
    public void DisplaySentence(string sentence)
    {
        dialogueText.text = sentence;
    }

    private void Update()
    {
        // Space Ű�� ���� �������� �Ѿ��
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            DialogueManager.Instance.DisplayNextSentence();
        }
    }
}
