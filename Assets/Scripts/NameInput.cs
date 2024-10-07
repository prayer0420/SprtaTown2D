using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameInput: MonoBehaviour
{
    public InputField inputField;  

    private void Start()
    {
        // 게임 시작 시 이미 저장된 이름이 있으면 InputField에 표시
        if(GameManager.Instance.PlayerName != null)
            inputField.text = GameManager.Instance.PlayerName;
    }

    // 이름 입력 시 호출될 함수
    public void OnNameChanged()
    {
        string newName = inputField.text;
        GameManager.Instance.SetPlayerName(newName);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("GameScene");
    }

}
