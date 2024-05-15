using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElecManagement : MonoBehaviour
{
    public GameObject blinkingCirclePrefab;
    public float blinkInterval = 3f;
    private float rowposition = -0.19f;
    private float columnposition = 0.18f;
    private GameObject[] circles;

    public GameObject timerText;

    void Start()
    {
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
        InvokeRepeating("ToggleRandomCircle", 3, blinkInterval);
        
        InvokeRepeating("ToggleRandomCircle", 30, blinkInterval);
    }


    void ToggleRandomCircle()
    {
        int randomIndex = Random.Range(0, 9);
        GameObject randomCircle = circles[randomIndex];
        randomCircle.SetActive(true);
        timerText.GetComponent<timer>().StartGame();
    }



}