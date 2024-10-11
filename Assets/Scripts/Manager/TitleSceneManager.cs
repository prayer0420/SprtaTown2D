// Improved TitleSceneManager
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleSceneManager : SceneManager
{
    public static TitleSceneManager Instance;

    // 캐릭터 선택 관련 필드
    public CharacterJobSO[] characterJobs;
    private int selectedCharacterIndex = 0;

    // 이름 입력 관련 필드
    public InputField nameInputField;
    public Button submitButton;

    public Button InitCharacterButton;
    public Sprite[] CharacterImages;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        // 이름 제출 버튼 클릭 시 호출
        submitButton.onClick.AddListener(OnSubmitName);
        submitButton.onClick.AddListener(() => LoadScene("GameScene"));
        
    }

    // 이름 제출 처리
    private void OnSubmitName()
    {
        string playerName = nameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            GameManager.Instance.SetPlayerName(playerName);
            Debug.Log("Player Name Saved: " + playerName);
        }
    }

    // 캐릭터 선택 시 호출되는 메서드
    public override void OnCharacterSelected(int characterIndex)
    {
        Image InitCharacterImage = InitCharacterButton.GetComponent<Image>();

        selectedCharacterIndex = characterIndex;
        InitCharacterImage.sprite = CharacterImages[selectedCharacterIndex];

        // 캐릭터 직업 배열에서 선택된 직업을 가져옴
        CharacterJobSO selectedJob = characterJobs[selectedCharacterIndex];

        // 선택된 직업을 GameManager에 설정
        GameManager.Instance.SetSelectedJob(selectedJob);

        Debug.Log("Character Selected: " + selectedJob.name);
    }

    public CharacterJobSO GetJob(int index)
    {
        if (index >= 0 && index < characterJobs.Length)
        {
            return characterJobs[index];
        }
        return null;
    }
}
