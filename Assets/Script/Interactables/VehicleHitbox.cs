using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleHitbox : MonoBehaviour
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
            Destroy(transform.parent.gameObject, 0.01f);
            return 0;
        }
        else
        {
            Destroy(transform.parent.gameObject, 0.01f);
            return 1;
        }
    }
}
