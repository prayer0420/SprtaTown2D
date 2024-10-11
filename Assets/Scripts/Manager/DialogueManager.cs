using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;  // 싱글톤 패턴으로 인스턴스를 유지

    public GameObject dialogueUIPrefab;  // 대화 UI 프리팹 (에디터에서 할당)
    private GameObject currentDialogueUI;  // 현재 생성된 대화 UI 인스턴스

    private DialogueSO currentDialogue;  // 현재 진행 중인 대화 데이터
    private int currentLineIndex = 0;  // 현재 대화 줄의 인덱스

    public bool IsDialogueActive { get; private set; }  // 대화 활성화 여부를 저장하는 플래그

    private void Awake()
    {
        // 싱글톤 패턴 적용
        if (Instance == null)
        {
            Instance = this;
            // 씬 전환 시에도 유지
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 대화 시작
    public void StartDialogue(DialogueSO dialogue)
    {
        if (IsDialogueActive)
        {
            Debug.LogWarning("이미 대화 중입니다!");
            return;
        }

        // 대화 활성화 플래그 설정
        IsDialogueActive = true;

        // 대화 데이터를 저장하고 인덱스를 초기화
        currentDialogue = dialogue;
        currentLineIndex = 0;

        // 대화 UI 프리팹을 동적으로 생성
        currentDialogueUI = Instantiate(dialogueUIPrefab, transform);
        DialogueUI dialogueUI = currentDialogueUI.GetComponent<DialogueUI>();
        dialogueUI.ShowDialogue();
        Debug.Log("대화시작");


        // 첫 번째 대화 줄을 화면에 표시
        DisplayCurrentLine(dialogueUI);
    }

    // 다음 대화 줄로 진행
    public void DisplayNextSentence()
    {
        if (!IsDialogueActive)
        {
            Debug.LogWarning("대화가 활성화되어 있지 않습니다.");
            return;
        }

        currentLineIndex++;

        if (currentLineIndex >= currentDialogue.dialogueLines.Length)
        {
            EndDialogue();  // 대화가 끝나면 종료
        }
        else
        {
            // 다음 대화 줄을 표시
            DialogueUI dialogueUI = currentDialogueUI.GetComponent<DialogueUI>();
            DisplayCurrentLine(dialogueUI);
        }
    }

    // 현재 대화 줄을 대화창에 표시하는 함수
    private void DisplayCurrentLine(DialogueUI dialogueUI)
    {
        DialogueLine currentLine = currentDialogue.dialogueLines[currentLineIndex];
        dialogueUI.DisplaySentence(currentLine.dialogueText);
    }

    // 대화 종료
    public void EndDialogue()
    {
        if (currentDialogueUI != null)
        {
            // 대화 UI를 비활성화 (HideDialogue() 호출)
            DialogueUI dialogueUI = currentDialogueUI.GetComponent<DialogueUI>();
            dialogueUI.HideDialogue();

            // 대화 UI를 파괴하여 제거
            Destroy(currentDialogueUI);
            currentDialogueUI = null;
        }
        currentDialogue = null;  // 대화 데이터 초기화
        currentLineIndex = 0;  // 대화 줄 인덱스 초기화

        // 대화 종료 시 대화 활성화 플래그 해제
        IsDialogueActive = false;
    }

}
