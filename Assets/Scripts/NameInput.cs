using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameInput: MonoBehaviour
{
    public InputField inputField;  

    private void Start()
    {
        // ���� ���� �� �̹� ����� �̸��� ������ InputField�� ǥ��
        if(GameManager.Instance.PlayerName != null)
            inputField.text = GameManager.Instance.PlayerName;
    }

    // �̸� �Է� �� ȣ��� �Լ�
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
