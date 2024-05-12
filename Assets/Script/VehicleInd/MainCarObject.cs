using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCarObject : MonoBehaviour
{
    VehicleManager vehicleManager;
    int score = 0;
    bool clear = false;
    void Start()
    {
        vehicleManager = FindAnyObjectByType<VehicleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (clear) { return; }
        int tmpScore = vehicleManager.GetScore();
        if (score != tmpScore)
        {
            score = tmpScore;
            Debug.Log(tmpScore-1);
            transform.GetChild(score-1).gameObject.SetActive(true);
        }
        if (score == 8)
        {
            score = 0;
            GetComponent<Animation>().Play();
            clear = true;
        }
    }
}
