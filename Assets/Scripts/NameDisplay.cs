using UnityEngine;
using TMPro;
using System.Collections;

public class NameDisplay : MonoBehaviour
{
    public Camera mainCamera;         // ���� ī�޶�
    public Transform playerTransform; // �÷��̾��� Transform
    private Vector3 offset = new Vector3(0, 1.5f, 0);  // ĳ���� ���� ��ġ�� ������

    private TMP_Text playerNameText;  // TextMeshPro �ؽ�Ʈ (�̸� ǥ�ÿ�)

    private void Start()
    {
        // �÷��̾��� �ڽĿ��� Canvas�� ã��, �� �ȿ� �ִ� �ؽ�Ʈ ������Ʈ�� ã��
        Canvas canvas = GetComponentInChildren<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("Canvas�� �÷��̾��� �ڽ����� �����Ǿ� ���� �ʽ��ϴ�.");
            return;
        }

        // Canvas �ؿ� �ִ� �ؽ�Ʈ�� ã�� (TextMeshPro�� ���� ��� TMP_Text��)
        playerNameText = canvas.GetComponentInChildren<TMP_Text>();

        if (playerNameText == null)
        {
            Debug.LogError("Canvas �ȿ� TMP_Text�� �����ϴ�.");
            return;
        }

        // GameManager���� �ʱ� �̸� �����ͼ� �ؽ�Ʈ ������Ʈ
        UpdatePlayerName(GameManager.Instance.GetPlayerName());

        // �÷��̾� �̸� ���� �� text ������Ʈ
        GameManager.Instance.OnPlayerNameChanged += UpdatePlayerName;

        // �÷��̾��� ��ġ�� �̸� ǥ�� (���� ��ġ ������Ʈ�� �ʿ��ϸ� �ڷ�ƾ���� ������Ʈ)
        //StartCoroutine(UpdateNamePosition(playerTransform));
    }

    private void UpdatePlayerName(string newName)
    {
        // �̸��� ����� �� �ؽ�Ʈ ������Ʈ
        playerNameText.text = newName;
    }

    //private IEnumerator UpdateNamePosition(Transform target)
    //{
    //    while (target != null)
    //    {
    //        // �÷��̾��� ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ�Ͽ� �ؽ�Ʈ ��ġ ����
    //        Vector3 screenPosition = mainCamera.WorldToScreenPoint(target.position + offset);
    //        playerNameText.transform.position = screenPosition;

    //        yield return null;  // �� �����Ӹ��� ������Ʈ
    //    }

    //    // ����� �ı��Ǹ� �̸� �ؽ�Ʈ�� �ı�
    //    Destroy(playerNameText.gameObject);
    //}

    private void OnDestroy()
    {
        // ���� ����ǰų� ������Ʈ�� �ı��� �� �̺�Ʈ ���� ����
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerNameChanged -= UpdatePlayerName;
        }
    }
}
