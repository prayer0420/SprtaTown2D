using UnityEngine;
using UnityEngine.UI; 

[RequireComponent(typeof(BoxCollider2D))]
public class NPC : Creature
{
    public NPCDataSO npcData;
    private bool playerInRange = false;

    public GameObject interactionUIPrefab; 
    private GameObject interactionUIInstance;

    // �÷��̾� ���̾ �����ϱ� ���� LayerMask
    public LayerMask playerLayer;
    

    //�ʱ� ����
    private void Start()
    {
        CharacterManager.Instance.AddCharacter(this);

        // BoxCollider2D�� Ʈ���ŷ� �����Ͽ� �÷��̾���� �浹�� ����
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        collider.isTrigger = true;

        // NPC�� ���� ����
        if (npcData != null)
        {
            // NPC �̸� ����
            creatureName = npcData.npcName;

            // ��������Ʈ ����
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
        // �÷��̾� ���̾����� Ȯ��
        if (playerLayer == (playerLayer | (1 << other.gameObject.layer)))
        {
            playerInRange = true;
            Debug.Log($"{npcData.npcName}�� ��ȣ�ۿ��� �� �ֽ��ϴ�.");
            ShowInteractionUI(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (playerLayer == (playerLayer | (1 << other.gameObject.layer)))
        {
            playerInRange = false;
            Debug.Log($"{npcData.npcName}���� ��ȣ�ۿ� ������ ������ϴ�.");
            ShowInteractionUI(false);

            // ��ȭ ���̶�� ��ȭ ����
            if (DialogueManager.Instance != null && DialogueManager.Instance.IsDialogueActive)
            {
                DialogueManager.Instance.EndDialogue();
            }
            QuestUIManager.Instance.HideQuestList(); //NPC����Ʈ��� �ݱ�
            QuestUIManager.Instance.questDetailsPanel.SetActive(false); //������ �г� �ݱ�
        }
    }

    private void Update()
    {
        // �÷��̾ ���� ���� ���� �� E Ű�� ��ȣ�ۿ� UI�� ���
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

    // ��ȣ�ۿ� UI�� ���ų� �ݱ�
    private void ShowInteractionUI(bool show)
    {
        //����
        if (show)
        {
            if (interactionUIInstance == null)
            {
                // InteractionUI ������ �ν��Ͻ� ����
                interactionUIInstance = Instantiate(interactionUIPrefab, transform);
                interactionUIInstance.transform.localPosition = Vector3.zero;

                // ��ư �̺�Ʈ ����
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
        //�ݱ�
        else
        {
            if (interactionUIInstance != null)
            {
                interactionUIInstance.SetActive(false);
            }
        }
    }

    // ��ȭ ������
    private void ExitInteraction()
    {
        ShowInteractionUI(false);
    }

    // ��ȭ�ϱ⸦ �����ϸ� ��ȭ�� ����
    private void StartTalking()
    {
        // ��ȭ ����
        if (npcData.dialogue != null)
        {
            DialogueManager.Instance.StartDialogue(npcData.dialogue);

            Debug.Log($"{npcData.npcName}�� ��ȭ�մϴ�.");
            ShowInteractionUI(false); // ��ȣ�ۿ� UI �����
        }
    }


    // ����Ʈ �ޱ⸦ �����ϸ� ����Ʈ ��� ǥ��
    private void ShowQuestList()
    {
        // ����Ʈ ��� ǥ��
        QuestUIManager.Instance.ShowQuestList(npcData);
        Debug.Log($"{npcData.npcName}�� ����Ʈ ����� ǥ���մϴ�.");
        ShowInteractionUI(false); // ��ȣ�ۿ� UI �����
    }
}
