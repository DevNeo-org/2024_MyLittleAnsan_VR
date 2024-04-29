using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleHitbox : MonoBehaviour
{
    private bool isCorrect = false; // 오브젝트 위치가 점수 획득 가능한 위치인지 체크
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
            Destroy(gameObject, 0.01f);
            return 0;
        }
        else
        {
            Destroy(gameObject, 0.01f);
            return 1;
        }
    }
}
