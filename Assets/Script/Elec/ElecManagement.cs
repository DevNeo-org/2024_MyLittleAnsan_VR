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
    private float rowposition = -0.19f;
    private float columnposition = 0.18f;
    private GameObject[] circles;
    
    private Timer timer;
    
    private float time = 2f;
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
        circles = new GameObject[9];
        for (int i = 0; i < 9; i++)  //9개의 원 생성
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
        if (startgame) //게임 시작시
        {
            timer.StartGame();
            leftSolder.gameObject.SetActive(true);
            rightSolder.gameObject.SetActive(true);
            time += Time.deltaTime;
            if (time >= 2f) //시간이 2초가 되면 랜덤으로 원을 키게 됨
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