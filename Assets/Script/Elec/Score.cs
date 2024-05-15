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
        // scoreText가 에디터에서 할당되지 않았을 경우 GetComponent로 초기화 시도
        if (scoreText == null)
        {
            scoreText = GetComponent<TextMeshPro>();
        }

        // scoreText가 여전히 null인 경우 오류 로그 출력
        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned and could not be found on the GameObject.");
            return;
        }
        Soldering scorescript = FindObjectOfType<Soldering>();
        // Soldering 컴포넌트를 현재 게임 오브젝트에서 가져옴
        

        // Soldering 컴포넌트가 null인 경우 오류 로그 출력
        if (scorescript == null)
        {
            Debug.LogError("Soldering component is not found on the GameObject.");
            return;
        }

        // score를 초기화
        score = scorescript.SendScore();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "score: " + score.ToString();
    }
}
