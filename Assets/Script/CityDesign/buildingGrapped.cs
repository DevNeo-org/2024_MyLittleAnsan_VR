using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingGrapped : MonoBehaviour
{
    GameObject gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }
    private void OnTriggerEnter(Collider Collider)
    {
        //땅과 충돌할 때 효과음 재생
        if(Collider.gameObject.name == "Table")
            gameManager.GetComponent<AudioManager>().PlaySound(3);
    }
}
