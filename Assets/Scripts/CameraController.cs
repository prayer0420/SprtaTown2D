using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform TargetPlayer;
    public Vector3 offset = new Vector3(0, 0, -10); // z값을 -10으로 설정해 위에서 내려다보는 형식
    public float smoothSpeed = 0.12f;

    void LateUpdate()
    {
        //목표 위치 = 플레이어 위치 + 오프셋
        Vector3 desiredPosition = TargetPlayer.position + offset;

        //부드럽게 현재 위치에서 목표위치로 이동
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        Debug.Log("Camera Position: " + transform.position);
    }
}
