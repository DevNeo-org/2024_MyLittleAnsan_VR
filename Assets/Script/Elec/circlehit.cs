using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlehit : MonoBehaviour
{
    private ElecMenuManagement menuManagement;

    private void Start()
    {
        menuManagement = FindAnyObjectByType<ElecMenuManagement>();
    }
    public int ObjectHit()
    {
        StartCoroutine(DeactivateAfterDelay());
        
        return 0;
    }

    IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second
        gameObject.SetActive(false); // Deactivate the game object
        menuManagement.ScorePlus();
    }
}

