using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBMS : MonoBehaviour
{
    public GameObject blinkingCirclePrefab; // ±ôºýÀÌ´Â ¿ø ÇÁ¸®ÆÕ
    public float blinkInterval = 0.5f; // ±ôºýÀÌ´Â °£°Ý
    private float rowposition = -6f;
    private float columnposition = 6f;
    private GameObject[] circles;

    // Start is called before the first frame update
    void Start()
    {
        circles = new GameObject[9];
        for (int i = 0; i < 9; i++)
        {
            GameObject circle = Instantiate(blinkingCirclePrefab, transform.position, Quaternion.identity);
            circle.transform.position = new Vector3(rowposition , columnposition, 0f);
            circle.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            circle.SetActive(false);
            circles[i] = circle;
            rowposition = rowposition + 6f;
            if (i == 2)
            {
                rowposition = -6f;
                columnposition = 0f;
            }
            if(i == 5)
            {
                rowposition = -6f;
                columnposition = -6f;
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
