using UnityEngine;
using TMPro;
using System; 

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText; 

    void Update()
    {
        UpdateTime();
    }

    void UpdateTime()
    {
        // �ý����� ���� �ð��� ������
        DateTime currentTime = DateTime.Now;

        // �ð��� "HH:mm" �������� ����
        string formattedTime = currentTime.ToString("HH:mm");

        timeText.text = formattedTime;
    }
}
