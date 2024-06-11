using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class ElecManagement : MonoBehaviour    
{
    public GameObject blinkingCirclePrefab;
    public float blinkInterval = 3f;
    private float rowposition = -0.19f;
    private float columnposition = 0.18f;
    [SerializeField] private GameObject[] circles;
    
    private Timer timer;
    
    private float time = 3f;
    int randomIndex = -1;
    int prerandomIndex = -2;
    bool startgame = false;

    [SerializeField] private GameObject leftSolder;
    [SerializeField] private GameObject rightSolder;
    void Start()
    {
        timer = FindAnyObjectByType<Timer>();
        leftSolder.gameObject.SetActive(false);
        rightSolder.gameObject.SetActive(false);

        
    }
    private void Update()
    {
        if (startgame)
        {
            timer.StartGame();
            leftSolder.gameObject.SetActive(true);
            rightSolder.gameObject.SetActive(true);
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