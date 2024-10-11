using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public InventoryUI inventoryUI;
    public StatusUI statusUI;
    public QuestUIManager questUIManager; 
    public UserUI userUI;
    private bool isUpdatingUserUI = false;


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

    private void Start()
    {
        // �÷��̾��� �̺�Ʈ�� �����Ͽ� UI ������Ʈ�� �̺�Ʈ ������� ó��
        if (GameManager.Instance.Player != null)
        {
            GameManager.Instance.Player.OnStatusChanged += UpdateStatusUI;
            GameManager.Instance.Player.OnExperienceGained += UpdateStatusUI;
            GameManager.Instance.Player.inventorySystem.OnInventoryChanged += UpdateInventoryUI;
            QuestManager.Instance.OnQuestUpdated += UpdateQuestUI; 
        }

        // �ʱ� UI ������Ʈ
        //UpdateAllUI();
    }

    // ��� UI ������Ʈ �Լ�
    public void UpdateAllUI()
    {
        UpdateInventoryUI();
        UpdateStatusUI();
        UpdateQuestUI();
        UpdateUserUI();
    }

    // ���� UI ������Ʈ �Լ���
    public void UpdateInventoryUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.UpdateInventoryUI();
        }
    }

    public void UpdateStatusUI()
    {
        if (statusUI != null)
        {
            statusUI.UpdateStatusUI();
        }
    }

    public void UpdateQuestUI()
    {
        // ���� ���� ����Ʈ ��� ������Ʈ
        if (questUIManager != null)
        {
            questUIManager.ShowActiveAndCompletedQuests(); 
        }
    }

    public void UpdateUserUI()
    {
        if (isUpdatingUserUI)
        {
            return; // �̹� ������Ʈ ���̸� �ߺ� ȣ�� ����
        }

        isUpdatingUserUI = true;

        // ���� UI ������Ʈ �ڵ�
        userUI.UpdateUserList();

        isUpdatingUserUI = false; // �۾��� ������ �÷��� ����
        Debug.Log("userList������Ʈ ��");
    }
}
