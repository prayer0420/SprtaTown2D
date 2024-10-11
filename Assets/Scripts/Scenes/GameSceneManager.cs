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
        // ���� �Ŵ����� ���� ���� �̺�Ʈ ����
        GameManager.Instance.OnPlayerInfoChanged += UpdatePlayerInfo;

        // ���� ���� �� ����� ���� �̸��� ���� ���� ��������
        UpdatePlayerInfo();

        // �̸� ���� ��ư Ŭ�� �� ChangeNameĵ���� ����, Confirm��ư ������ ����
        nameChangeButton.onClick.AddListener(()=> ChangeName.gameObject.SetActive(true));
        nameChanageConfirm.onClick.AddListener(OnChangeName);

        //ĳ���ͺ��� ��ư Ŭ���� ChangeButtonĵ���� ����
        jobChangeButton.onClick.AddListener(()=>ChangeCharacter.gameObject.SetActive(true));
    }

    // �̸��� �����ϴ� �Լ�
    private void OnChangeName()
    {
        string newName = nameChangeInputField.text;

        if (!string.IsNullOrEmpty(newName))
        {
            GameManager.Instance.SetPlayerName(newName); //Gameamanger ������Ʈ
            playerNameText.text = newName;  // UI ������Ʈ
            UpdatePlayerInfo();
            Debug.Log("�÷��̾� �̸� �����: " + newName);
        }
    }

    // ������ �����ϴ� �Լ�
    public override void OnCharacterSelected(int JobIndex)
    {

        int changeJobIndex = JobIndex;

        // ĳ���� ���� �迭���� ���õ� ������ ������
        CharacterJobSO selectedJob = GameManager.Instance.characterJobs[JobIndex];

        // ���õ� ������ GameManager�� ����
        GameManager.Instance.SetSelectedJob(selectedJob);

        Debug.Log("�÷��̾� ���� �����: " + selectedJob.characterJobName);
    }

    // ���� �Ŵ������� �÷��̾� ���� ������Ʈ
    private void UpdatePlayerInfo()
    {
        playerNameText.text = $"{GameManager.Instance.GetPlayerName()}, {GameManager.Instance.GetCharacterJobName()} ";
    }


    private void OnDestroy()
    {
        // ���� �ı��� �� �̺�Ʈ ���� ����
        GameManager.Instance.OnPlayerInfoChanged -= UpdatePlayerInfo;
    }
    
    
    //����Ű 
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.K))
        {
            Debug.Log("Change info");
            ChangeInfo.SetActive(!ChangeInfo.activeSelf);
        }
    }
}

