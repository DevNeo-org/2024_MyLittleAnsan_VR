using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class circlehit : MonoBehaviour
{
   
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
    }
}

