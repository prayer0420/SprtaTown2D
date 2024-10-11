using UnityEngine;
using UnityEngine.UI;

public class NameChangeUI : MonoBehaviour
{
    public InputField inputField;  // �̸��� �Է¹޴� InputField
    public Button changeNameButton;  // �̸��� �����ϴ� ��ư

    private void Start()
    {
        // ��ư Ŭ�� �� �̸��� �����ϴ� ����
        changeNameButton.onClick.AddListener(ChangePlayerName);
    }

    private void ChangePlayerName()
    {
        string newName = inputField.text;

        if (!string.IsNullOrEmpty(newName))
        {
            GameManager.Instance.SetPlayerName(newName);
        }
    }
}
