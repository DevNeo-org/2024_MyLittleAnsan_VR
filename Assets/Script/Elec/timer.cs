using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class timer : MonoBehaviour
{
    float timeInSeconds = 60f;
    public TextMeshPro timeText;

    void Start()
    {
        // TextMeshProUGUI ������Ʈ�� ã�Ƽ� �ʱ�ȭ�մϴ�.
        timeText = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        // �ð� ����
        timeInSeconds -= Time.deltaTime;

        // �ð��� �а� �ʷ� ��ȯ
        
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        // �ð� ǥ�� ���� �����Ͽ� TextMeshPro�� ǥ��
        timeText.text = string.Format("{00}", seconds);
    }
}

