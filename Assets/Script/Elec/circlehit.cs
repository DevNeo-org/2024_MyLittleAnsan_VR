using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlehit : MonoBehaviour
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
            gameObject.SetActive(false);
            return 0;
        }
        else
        {
           
            return 1;
        }
    }
}

