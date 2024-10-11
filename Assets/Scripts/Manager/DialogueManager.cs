using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;  // �̱��� �������� �ν��Ͻ��� ����

    public GameObject dialogueUIPrefab;  // ��ȭ UI ������ (�����Ϳ��� �Ҵ�)
    private GameObject currentDialogueUI;  // ���� ������ ��ȭ UI �ν��Ͻ�

    private DialogueSO currentDialogue;  // ���� ���� ���� ��ȭ ������
    private int currentLineIndex = 0;  // ���� ��ȭ ���� �ε���

    public bool IsDialogueActive { get; private set; }  // ��ȭ Ȱ��ȭ ���θ� �����ϴ� �÷���

    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            // �� ��ȯ �ÿ��� ����
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ��ȭ ����
    public void StartDialogue(DialogueSO dialogue)
    {
        if (IsDialogueActive)
        {
            Debug.LogWarning("�̹� ��ȭ ���Դϴ�!");
            return;
        }

        // ��ȭ Ȱ��ȭ �÷��� ����
        IsDialogueActive = true;

        // ��ȭ �����͸� �����ϰ� �ε����� �ʱ�ȭ
        currentDialogue = dialogue;
        currentLineIndex = 0;

        // ��ȭ UI �������� �������� ����
        currentDialogueUI = Instantiate(dialogueUIPrefab, transform);
        DialogueUI dialogueUI = currentDialogueUI.GetComponent<DialogueUI>();
        dialogueUI.ShowDialogue();
        Debug.Log("��ȭ����");


        // ù ��° ��ȭ ���� ȭ�鿡 ǥ��
        DisplayCurrentLine(dialogueUI);
    }

    // ���� ��ȭ �ٷ� ����
    public void DisplayNextSentence()
    {
        if (!IsDialogueActive)
        {
            Debug.LogWarning("��ȭ�� Ȱ��ȭ�Ǿ� ���� �ʽ��ϴ�.");
            return;
        }

        currentLineIndex++;

        if (currentLineIndex >= currentDialogue.dialogueLines.Length)
        {
            EndDialogue();  // ��ȭ�� ������ ����
        }
        else
        {
            // ���� ��ȭ ���� ǥ��
            DialogueUI dialogueUI = currentDialogueUI.GetComponent<DialogueUI>();
            DisplayCurrentLine(dialogueUI);
        }
    }

    // ���� ��ȭ ���� ��ȭâ�� ǥ���ϴ� �Լ�
    private void DisplayCurrentLine(DialogueUI dialogueUI)
    {
        DialogueLine currentLine = currentDialogue.dialogueLines[currentLineIndex];
        dialogueUI.DisplaySentence(currentLine.dialogueText);
    }

    // ��ȭ ����
    public void EndDialogue()
    {
        if (currentDialogueUI != null)
        {
            // ��ȭ UI�� ��Ȱ��ȭ (HideDialogue() ȣ��)
            DialogueUI dialogueUI = currentDialogueUI.GetComponent<DialogueUI>();
            dialogueUI.HideDialogue();

            // ��ȭ UI�� �ı��Ͽ� ����
            Destroy(currentDialogueUI);
            currentDialogueUI = null;
        }
        currentDialogue = null;  // ��ȭ ������ �ʱ�ȭ
        currentLineIndex = 0;  // ��ȭ �� �ε��� �ʱ�ȭ

        // ��ȭ ���� �� ��ȭ Ȱ��ȭ �÷��� ����
        IsDialogueActive = false;
    }

}
