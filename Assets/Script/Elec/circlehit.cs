using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circlehit : MonoBehaviour
{
    private ElecMenuManagement menuManagement;
    
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

