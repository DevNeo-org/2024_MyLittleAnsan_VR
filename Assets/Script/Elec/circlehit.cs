using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlehit : MonoBehaviour
{
    private bool isCorrect = false; // ������Ʈ ��ġ�� ���� ȹ�� ������ ��ġ���� üũ
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public int ObjectHit()
    {
        if (isCorrect)
        {
            gameObject.SetActive(false);
            return 0;
        }
        else
        {
           
            return 1;
        }
    }
}

