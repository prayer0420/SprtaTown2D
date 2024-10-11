using UnityEngine;
using UnityEngine.UI; 

[RequireComponent(typeof(BoxCollider2D))]
public class NPC : Creature
{
    public NPCDataSO npcData;
    private bool playerInRange = false;

    public GameObject interactionUIPrefab; 
    private GameObject interactionUIInstance;

    // 플레이어 레이어를 감지하기 위한 LayerMask
    public LayerMask playerLayer;
    

    //초기 설정
    private void Start()
    {
        CharacterManager.Instance.AddCharacter(this);

        // BoxCollider2D를 트리거로 설정하여 플레이어와의 충돌을 감지
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;

        // NPC의 외형 설정
        if (npcData != null)
        {
            // NPC 이름 설정
            creatureName = npcData.npcName;

            // 스프라이트 설정
            if (npcData.npcSprite != null)
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = npcData.npcSprite;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어 레이어인지 확인
        if (playerLayer == (playerLayer | (1 << other.gameObject.layer)))
        {
            playerInRange = true;
            Debug.Log($"{npcData.npcName}과 상호작용할 수 있습니다.");
            ShowInteractionUI(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (playerLayer == (playerLayer | (1 << other.gameObject.layer)))
        {
            playerInRange = false;
            Debug.Log($"{npcData.npcName}과의 상호작용 범위를 벗어났습니다.");
            ShowInteractionUI(false);

            // 대화 중이라면 대화 종료
            if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive)
            {
                DialogueManager.Instance.EndDialogue();
            }
            QuestUIManager.Instance.HideQuestList(); //NPC퀘스트목록 닫기
            QuestUIManager.Instance.questDetailsPanel.SetActive(false); //디테일 패널 닫기
        }
    }

    private void Update()
    {
        // 플레이어가 범위 내에 있을 때 E 키로 상호작용 UI를 토글
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (interactionUIInstance != null && interactionUIInstance.activeSelf)
            {
                ShowInteractionUI(false);

            }
            else
            {
                ShowInteractionUI(true);

            }
        }
    }

    // 상호작용 UI를 열거나 닫기
    private void ShowInteractionUI(bool show)
    {
        //열기
        if (show)
        {
            if (interactionUIInstance == null)
            {
                // InteractionUI 프리팹 인스턴스 생성
                interactionUIInstance = Instantiate(interactionUIPrefab, transform);
                interactionUIInstance.transform.localPosition = Vector3.zero;

                // 버튼 이벤트 설정
                Button exitButton = interactionUIInstance.transform.Find("Panel/ExitButton").GetComponent<Button>();
                Button talkButton = interactionUIInstance.transform.Find("Panel/TalkButton").GetComponent<Button>();
                Button questButton = interactionUIInstance.transform.Find("Panel/QuestButton").GetComponent<Button>();

                exitButton.onClick.AddListener(() => ExitInteraction());
                talkButton.onClick.AddListener(() => StartTalking());
                questButton.onClick.AddListener(() => ShowQuestList());
            }
            else
            {
                interactionUIInstance.SetActive(true);
            }
        }
        //닫기
        else
        {
            if (interactionUIInstance != null)
            {
                interactionUIInstance.SetActive(false);
            }
        }
    }

    // 대화 나가기
    private void ExitInteraction()
    {
        ShowInteractionUI(false);
    }

    // 대화하기를 선택하면 대화를 시작
    private void StartTalking()
    {
        // 대화 시작
        if (npcData.dialogue != null)
        {
            DialogueManager.Instance.StartDialogue(npcData.dialogue);

            Debug.Log($"{npcData.npcName}과 대화합니다.");
            ShowInteractionUI(false); // 상호작용 UI 숨기기
        }
    }


    // 퀘스트 받기를 선택하면 퀘스트 목록 표시
    private void ShowQuestList()
    {
        // 퀘스트 목록 표시
        QuestUIManager.Instance.ShowQuestList(npcData);
        Debug.Log($"{npcData.npcName}의 퀘스트 목록을 표시합니다.");
        ShowInteractionUI(false); // 상호작용 UI 숨기기
    }
}
