using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    private bool carClear;
    private int score;
    private float timer;
    DataManager dataManager;
    void Start()
    {
        score = 0;
        dataManager = FindAnyObjectByType<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (score > 10)
        {
            dataManager.SetClear();
            //carClear = true;
        }
    }
    public void ScorePlus()
    {
        score++;
    }
    public int GetScore()
    {
        return score;
    }
}
