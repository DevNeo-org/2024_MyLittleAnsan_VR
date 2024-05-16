using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float timeInSeconds = 60f;
    public TextMeshPro timeText;
    private bool gameStart = false;
    private bool timeOver = false;
    void Start()
    {
        // TextMeshProUGUI 컴포넌트를 찾아서 초기화합니다.
        timeText = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if (!gameStart || timeOver) return;

        // 시간 갱신
        timeInSeconds -= Time.deltaTime;

        // 시간을 분과 초로 변환
        
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        if (seconds < 0)
        {
            TimeOver();
            return;
        }
        // 시간 표시 포맷 지정하여 TextMeshPro에 표시
        timeText.text = "남은 시간: " + string.Format("{00}", seconds);
    }
    public void StartGame()
    {
        gameStart = true;
    }
    public void TimeOver()
    {
        timeText.text = "시간 종료!";
        timeOver = true;
    }
    public bool GetBool() // 60초 시간의 종료 여부 리턴
    {
        return timeOver;
    }
    public bool SendStartGame()
    {
        return gameStart;
    }
}

