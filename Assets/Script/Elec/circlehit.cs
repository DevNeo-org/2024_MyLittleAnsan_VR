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
        transform.parent.gameObject.SetActive(false); //��ü�� ������ ����
        menuManagement.ScorePlus(); //������ 1�� ����
        return 0;
    }

   
}

