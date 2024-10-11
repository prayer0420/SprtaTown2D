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

    private void Start()
    {
        // 플레이어의 이벤트에 구독하여 UI 업데이트를 이벤트 기반으로 처리
        if (GameManager.Instance.Player != null)
        {
            GameManager.Instance.Player.OnStatusChanged += UpdateStatusUI;
            GameManager.Instance.Player.OnExperienceGained += UpdateStatusUI;
            GameManager.Instance.Player.inventorySystem.OnInventoryChanged += UpdateInventoryUI;
            QuestManager.Instance.OnQuestUpdated += UpdateQuestUI; 
        }

        // 초기 UI 업데이트
        //UpdateAllUI();
    }

    // 모든 UI 업데이트 함수
    public void UpdateAllUI()
    {
        UpdateInventoryUI();
        UpdateStatusUI();
        UpdateQuestUI();
        UpdateUserUI();
    }

    // 개별 UI 업데이트 함수들
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
        // 진행 중인 퀘스트 목록 업데이트
        if (questUIManager != null)
        {
            questUIManager.ShowActiveAndCompletedQuests(); 
        }
    }

    public void UpdateUserUI()
    {
        if (isUpdatingUserUI)
        {
            return; // 이미 업데이트 중이면 중복 호출 방지
        }

        isUpdatingUserUI = true;

        // 실제 UI 업데이트 코드
        userUI.UpdateUserList();

        isUpdatingUserUI = false; // 작업이 끝나면 플래그 해제
        Debug.Log("userList업데이트 끝");
    }
}
