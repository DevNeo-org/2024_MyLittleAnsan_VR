using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    int score;
  
    public TextMeshPro scoreText;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<TextMeshPro>();
        Soldering score = GetComponent<Soldering>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "score: " + score.ToString();
    }
}
