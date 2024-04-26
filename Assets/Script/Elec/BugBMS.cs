using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBMS : MonoBehaviour
{
    public GameObject blinkingCirclePrefab; // �����̴� �� ������
    public float blinkInterval = 0.5f; // �����̴� ����
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
        // ����ڰ� �ƹ� Ű�� ������ �� Ŭ���� ��Ŭ�� ���¸� false�� �����մϴ�.
        if (Input.anyKeyDown)
        {
            // Ŭ���� ��Ŭ�� Ȯ���մϴ�.
            GameObject clickedCircle = GetClickedCircle();
            if (clickedCircle != null)
            {
                // Ŭ���� ��Ŭ�� ���¸� false�� �����մϴ�.
                clickedCircle.SetActive(false);
            }
        }
    }

    GameObject GetClickedCircle()
    {
        // ���콺 ��ġ�� Unity�� ��ǥ�� ��ȯ�մϴ�.
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Ŭ���� ��Ŭ�� Ȯ���մϴ�.
        Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
        if (hitCollider != null)
        {
            return hitCollider.gameObject;
        }
        return null;
    }
}
