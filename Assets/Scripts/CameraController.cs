using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform TargetPlayer;
    public Vector3 offset = new Vector3(0, 0, -10); // z���� -10���� ������ ������ �����ٺ��� ����
    public float smoothSpeed = 0.12f;

    void LateUpdate()
    {
        //��ǥ ��ġ = �÷��̾� ��ġ + ������
        Vector3 desiredPosition = TargetPlayer.position + offset;

        //�ε巴�� ���� ��ġ���� ��ǥ��ġ�� �̵�
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        Debug.Log("Camera Position: " + transform.position);
    }
}
