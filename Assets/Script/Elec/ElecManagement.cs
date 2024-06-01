using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class ElecManagement : MonoBehaviour    
{
    public GameObject blinkingCirclePrefab;
    public float blinkInterval = 3f;
    private float rowposition = -0.19f;
    private float columnposition = 0.18f;
    private GameObject[] circles;
    
    private Timer timer;
    private float time = 3f;
    int randomIndex = -1;
    int prerandomIndex = -2;
    bool startgame;
    void Start()
    {
        timer = FindAnyObjectByType<Timer>();
        circles = new GameObject[9];
        for (int i = 0; i < 9; i++)
        {

            GameObject circle = Instantiate(blinkingCirclePrefab, transform.position, Quaternion.identity);
            circle.transform.position = new Vector3(rowposition, -0.29f, columnposition);
            circle.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            circle.SetActive(false);
            circles[i] = circle;
            rowposition = rowposition + 0.09f;
            if (i == 2)
            {
                rowposition = -0.19f;
                columnposition = 0.09f;
            }
            if (i == 5)
            {
                rowposition = -0.19f;
                columnposition = 0f;
            }

        }
        
    }
    private void Update()
    {
        if (startgame)
        {
            timer.StartGame();
            time += Time.deltaTime;
            if (time >= 3f)
            {
                randomIndex = Random.Range(0, circles.Length);
                while (randomIndex == prerandomIndex)
                {
                    randomIndex = Random.Range(0, circles.Length);
                }
                circles[randomIndex].SetActive(true);


                time = 0f; // time 변수를 다시 초기화하여 다음 간격을 기다립니다.
                prerandomIndex = randomIndex;
            }
        }
    }
    
    public void StartGame()
    {
        startgame = true;
    }
    


}