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
            gameObject.SetActive(false);
            return 0;
       
    }
}

