using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElecManagement : MonoBehaviour
{
    public GameObject blinkingCirclePrefab; 
    public float blinkInterval = 0.5f; 
    private float rowposition = -0.3f;
    private float columnposition = 0.3f;
    private GameObject[] circles;
    int i = 0;

    void Start()
    {
        circles = new GameObject[9];
        while (true)
        {
            
            GameObject circle = Instantiate(blinkingCirclePrefab, transform.position, Quaternion.identity);
            circle.transform.position = new Vector3(rowposition, columnposition, -0.05f);
            circle.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            circle.SetActive(false);
            circles[i] = circle;
            rowposition = rowposition + 0.3f;
            if (i == 2)
            {
                rowposition = -0.3f;
                columnposition = 0f;
            }
            if (i == 5)
            {
                rowposition = -0.3f;
                columnposition = -0.3f;
            }
            i++;
            if(i == 9)
            {
                i = 0;
            }

            
        }
        InvokeRepeating("ToggleRandomCircle", 0, blinkInterval);
    }

    void ToggleRandomCircle()
    {
        int randomIndex = Random.Range(0, 9);
        GameObject randomCircle = circles[randomIndex];
        randomCircle.SetActive(!randomCircle.activeSelf);
    }

   

}
