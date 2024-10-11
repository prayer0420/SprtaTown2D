using UnityEngine;
using UnityEngine.UI;

public class NameChangeUI : MonoBehaviour
{
    public InputField inputField;  // 이름을 입력받는 InputField
    public Button changeNameButton;  // 이름을 변경하는 버튼

    private void Start()
    {
        // 버튼 클릭 시 이름을 변경하는 로직
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
