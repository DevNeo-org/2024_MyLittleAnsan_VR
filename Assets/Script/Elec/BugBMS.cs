using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBMS : MonoBehaviour
{
    public GameObject blinkingCirclePrefab; // 깜빡이는 원 프리팹
    public float blinkInterval = 0.5f; // 깜빡이는 간격
    private float rowposition = -0.6f;
    private float columnposition = 0.6f;
    private GameObject[] circles;

    // Start is called before the first frame update
    void Start()
    {
        circles = new GameObject[9];
        for (int i = 0; i < 9; i++)
        {
            GameObject circle = Instantiate(blinkingCirclePrefab, transform.position, Quaternion.identity);
            circle.transform.position = new Vector3(rowposition , columnposition, -0.1f);
            circle.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            circle.SetActive(false);
            circles[i] = circle;
            rowposition = rowposition + 0.6f;
            if (i == 2)
            {
                rowposition = -0.6f;
                columnposition = 0f;
            }
            if(i == 5)
            {
                rowposition = -0.6f;
                columnposition = -0.6f;
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

    void Update()
    {
        // 사용자가 아무 키나 눌렀을 때 클릭된 서클의 상태를 false로 변경합니다.
        if (Input.anyKeyDown)
        {
            // 클릭된 서클을 확인합니다.
            GameObject clickedCircle = GetClickedCircle();
            if (clickedCircle != null)
            {
                // 클릭된 서클의 상태를 false로 변경합니다.
                clickedCircle.SetActive(false);
            }
        }
    }

    GameObject GetClickedCircle()
    {
        // 마우스 위치를 Unity의 좌표로 변환합니다.
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 클릭된 서클을 확인합니다.
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
        if (hitCollider != null)
        {
            return hitCollider.gameObject;
        }
        return null;
    }
}
