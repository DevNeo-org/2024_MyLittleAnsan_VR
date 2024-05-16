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
        // TextMeshProUGUI ������Ʈ�� ã�Ƽ� �ʱ�ȭ�մϴ�.
        timeText = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if (!gameStart || timeOver) return;

        // �ð� ����
        timeInSeconds -= Time.deltaTime;

        // �ð��� �а� �ʷ� ��ȯ
        
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);
        if (seconds < 0)
        {
            TimeOver();
            return;
        }
        // �ð� ǥ�� ���� �����Ͽ� TextMeshPro�� ǥ��
        timeText.text = "���� �ð�: " + string.Format("{00}", seconds);
    }
    public void StartGame()
    {
        gameStart = true;
    }
    public void TimeOver()
    {
        timeText.text = "�ð� ����!";
        timeOver = true;
    }
    public bool GetBool() // 60�� �ð��� ���� ���� ����
    {
        return timeOver;
    }
    public bool SendStartGame()
    {
        return gameStart;
    }
}

