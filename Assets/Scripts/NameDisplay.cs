using UnityEngine;
using TMPro;
using System.Collections;

public class NameDisplay : MonoBehaviour
{
    public Camera mainCamera;         // 메인 카메라
    public Transform playerTransform; // 플레이어의 Transform
    private Vector3 offset = new Vector3(0, 1.5f, 0);  // 캐릭터 위로 배치할 오프셋

    private TMP_Text playerNameText;  // TextMeshPro 텍스트 (이름 표시용)

    private void Start()
    {
        // 플레이어의 자식에서 Canvas를 찾고, 그 안에 있는 텍스트 컴포넌트를 찾음
        Canvas canvas = GetComponentInChildren<Canvas>();

        if (canvas == null)
        {
            Debug.LogError("Canvas가 플레이어의 자식으로 설정되어 있지 않습니다.");
            return;
        }

        // Canvas 밑에 있는 텍스트를 찾음 (TextMeshPro가 있을 경우 TMP_Text로)
        playerNameText = canvas.GetComponentInChildren<TMP_Text>();

        if (playerNameText == null)
        {
            Debug.LogError("Canvas 안에 TMP_Text가 없습니다.");
            return;
        }

        // GameManager에서 초기 이름 가져와서 텍스트 업데이트
        UpdatePlayerName(GameManager.Instance.GetPlayerName());

        // 플레이어 이름 변경 시 text 업데이트
        GameManager.Instance.OnPlayerNameChanged += UpdatePlayerName;

        // 플레이어의 위치에 이름 표시 (만약 위치 업데이트가 필요하면 코루틴으로 업데이트)
        //StartCoroutine(UpdateNamePosition(playerTransform));
    }

    private void UpdatePlayerName(string newName)
    {
        // 이름이 변경될 때 텍스트 업데이트
        playerNameText.text = newName;
    }

    //private IEnumerator UpdateNamePosition(Transform target)
    //{
    //    while (target != null)
    //    {
    //        // 플레이어의 월드 좌표를 스크린 좌표로 변환하여 텍스트 위치 조정
    //        Vector3 screenPosition = mainCamera.WorldToScreenPoint(target.position + offset);
    //        playerNameText.transform.position = screenPosition;

    //        yield return null;  // 매 프레임마다 업데이트
    //    }

    //    // 대상이 파괴되면 이름 텍스트도 파괴
    //    Destroy(playerNameText.gameObject);
    //}

    private void OnDestroy()
    {
        // 씬이 종료되거나 오브젝트가 파괴될 때 이벤트 구독 해제
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnPlayerNameChanged -= UpdatePlayerName;
        }
    }
}
