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
        //���� �浹�� �� ȿ���� ���
        if(Collider.gameObject.name == "Table")
            gameManager.GetComponent<AudioManager>().PlaySound(3);
    }
}
