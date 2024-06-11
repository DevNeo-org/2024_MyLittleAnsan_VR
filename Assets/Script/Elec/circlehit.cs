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
        transform.parent.gameObject.SetActive(false); //¹°Ã¼¿¡ ´êÀ¸¸é ²¨Áü
        menuManagement.ScorePlus(); //Á¡¼ö°¡ 1Á¡ ¿À¸§
        return 0;
    }

   
}

