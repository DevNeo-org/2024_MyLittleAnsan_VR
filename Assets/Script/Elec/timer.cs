using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    float timeInSeconds = 60f;
    public TextMeshPro timeText;
    private bool gameStart = false;
    void Start()
    {
        // TextMeshProUGUI 컴포넌트를 찾아서 초기화합니다.
        timeText = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if (!gameStart) return;

        // 시간 갱신
        timeInSeconds -= Time.deltaTime;

        // 시간을 분과 초로 변환
        
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // 시간 표시 포맷 지정하여 TextMeshPro에 표시
        timeText.text = string.Format("{00}", seconds);
    }
    public void StartGame()
    {
        gameStart = true;
    }
}

