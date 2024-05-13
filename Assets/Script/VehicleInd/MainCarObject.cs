using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainCarObject : MonoBehaviour
{
    VehicleManager vehicleManager;
    int score = 0;
    bool clear = false;
    private Animation anim;

    AnimationState[] animStates;
    void Start()
    {
        anim = GetComponent<Animation>();
        animStates = anim.Cast<AnimationState>().ToArray();
        vehicleManager = FindAnyObjectByType<VehicleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        int tmpScore = vehicleManager.GetScore();
        if (score != tmpScore)
        {
            score = tmpScore;
            if (score < 9)
            {
                transform.GetChild(score - 1).gameObject.SetActive(true);
                anim.Play(animStates[score - 1].name);
            }
            if (score == 9 || score == 10)
            {
                anim.Play(animStates[score - 1].name);
            }
            if (score == 10)
            {
                anim.Play(animStates[score - 1].name);
                clear = true;
            }
        }
    }
}
