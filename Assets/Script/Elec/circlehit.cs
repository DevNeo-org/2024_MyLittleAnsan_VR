using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlehit : MonoBehaviour
{
    private ElecMenuManagement menuManagement;
    private GameObject[] Circles;
    
    private void Start()
    {
        menuManagement = FindAnyObjectByType<ElecMenuManagement>();
        
    }
    public int ObjectHit()
    {
        Debug.Log(gameObject);
        transform.parent.gameObject.SetActive(false);
        menuManagement.ScorePlus();

        return 0;
    }

   
}

