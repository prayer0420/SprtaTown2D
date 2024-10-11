// Improved TitleSceneManager
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleSceneManager : SceneManager
{
    public static TitleSceneManager Instance;

    // ĳ���� ���� ���� �ʵ�
    public CharacterJobSO[] characterJobs;
    private int selectedCharacterIndex = 0;

    // �̸� �Է� ���� �ʵ�
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

        // �̸� ���� ��ư Ŭ�� �� ȣ��
        submitButton.onClick.AddListener(OnSubmitName);
        submitButton.onClick.AddListener(() => LoadScene("GameScene"));
        
    }

    // �̸� ���� ó��
    private void OnSubmitName()
    {
        string playerName = nameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            GameManager.Instance.SetPlayerName(playerName);
            Debug.Log("Player Name Saved: " + playerName);
        }
    }

    // ĳ���� ���� �� ȣ��Ǵ� �޼���
    public override void OnCharacterSelected(int characterIndex)
    {
        Image InitCharacterImage = InitCharacterButton.GetComponent<Image>();

        selectedCharacterIndex = characterIndex;
        InitCharacterImage.sprite = CharacterImages[selectedCharacterIndex];

        // ĳ���� ���� �迭���� ���õ� ������ ������
        CharacterJobSO selectedJob = characterJobs[selectedCharacterIndex];

        // ���õ� ������ GameManager�� ����
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
