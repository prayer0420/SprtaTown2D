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
        // 시스템의 현재 시간을 가져옴
        DateTime currentTime = DateTime.Now;

        // 시간을 "HH:mm" 형식으로 설정
        string formattedTime = currentTime.ToString("HH:mm");

        timeText.text = formattedTime;
    }
}
