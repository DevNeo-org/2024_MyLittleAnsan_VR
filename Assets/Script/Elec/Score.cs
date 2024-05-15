using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int score;
  
    public TextMeshPro scoreText;
    // Start is called before the first frame update
    void Start()
    {
        // scoreText�� �����Ϳ��� �Ҵ���� �ʾ��� ��� GetComponent�� �ʱ�ȭ �õ�
        if (scoreText == null)
        {
            scoreText = GetComponent<TextMeshPro>();
        }

        // scoreText�� ������ null�� ��� ���� �α� ���
        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned and could not be found on the GameObject.");
            return;
        }
        Soldering scorescript = FindObjectOfType<Soldering>();
        // Soldering ������Ʈ�� ���� ���� ������Ʈ���� ������
        

        // Soldering ������Ʈ�� null�� ��� ���� �α� ���
        if (scorescript == null)
        {
            Debug.LogError("Soldering component is not found on the GameObject.");
            return;
        }

        // score�� �ʱ�ȭ
        score = scorescript.SendScore();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "score: " + score.ToString();
    }
}
