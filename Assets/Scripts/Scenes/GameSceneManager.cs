using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameSceneManager : SceneManager
{
    static public GameSceneManager Instance {get; set;}

    [SerializeField]
    public TextMeshProUGUI playerNameText;

    private Text characterJobText;

    public GameObject ChangeInfo;

    [Header("ChnageCharacter")]
    public Button jobChangeButton;
    public Canvas ChangeCharacter;

    [Header("ChnageName")]
    public Button nameChangeButton;
    public Canvas ChangeName;
    public InputField nameChangeInputField;
    public Button nameChanageConfirm;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // 게임 매니저의 정보 변경 이벤트 구독
        GameManager.Instance.OnPlayerInfoChanged += UpdatePlayerInfo;

        // 게임 시작 시 저장된 유저 이름과 직업 정보 가져오기
        UpdatePlayerInfo();

        // 이름 변경 버튼 클릭 시 ChangeName캔버스 열고, Confirm버튼 누르면 적용
        nameChangeButton.onClick.AddListener(()=> ChangeName.gameObject.SetActive(true));
        nameChanageConfirm.onClick.AddListener(OnChangeName);

        //캐릭터변경 버튼 클릭시 ChangeButton캔버스 열기
        jobChangeButton.onClick.AddListener(()=>ChangeCharacter.gameObject.SetActive(true));
    }

    // 이름을 변경하는 함수
    private void OnChangeName()
    {
        string newName = nameChangeInputField.text;

        if (!string.IsNullOrEmpty(newName))
        {
            GameManager.Instance.SetPlayerName(newName); //Gameamanger 업데이트
            playerNameText.text = newName;  // UI 업데이트
            UpdatePlayerInfo();
            Debug.Log("플레이어 이름 변경됨: " + newName);
        }
    }

    // 직업을 변경하는 함수
    public override void OnCharacterSelected(int JobIndex)
    {

        int changeJobIndex = JobIndex;

        // 캐릭터 직업 배열에서 선택된 직업을 가져옴
        CharacterJobSO selectedJob = GameManager.Instance.characterJobs[JobIndex];

        // 선택된 직업을 GameManager에 설정
        GameManager.Instance.SetSelectedJob(selectedJob);

        Debug.Log("플레이어 직업 변경됨: " + selectedJob.characterJobName);
    }

    // 게임 매니저에서 플레이어 정보 업데이트
    private void UpdatePlayerInfo()
    {
        playerNameText.text = $"{GameManager.Instance.GetPlayerName()}, {GameManager.Instance.GetCharacterJobName()} ";
    }


    private void OnDestroy()
    {
        // 씬이 파괴될 때 이벤트 구독 해제
        GameManager.Instance.OnPlayerInfoChanged -= UpdatePlayerInfo;
    }
    
    
    //단축키 
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.K))
        {
            Debug.Log("Change info");
            ChangeInfo.SetActive(!ChangeInfo.activeSelf);
        }
    }
}

