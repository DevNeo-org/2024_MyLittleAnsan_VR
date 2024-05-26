using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainCarObject : MonoBehaviour
{
    [SerializeField] int scoreIndex; // 0 또는 10으로 설정, 0이면 좌측으로 이동하는 애니메이션, 10이면 우측
    VehicleManager vehicleManager;
    int score = 0;
    private Animation anim;

    AnimationState[] animStates;
    void Start()
    {
        anim = GetComponent<Animation>();
        animStates = anim.Cast<AnimationState>().ToArray();
        vehicleManager = FindAnyObjectByType<VehicleManager>();
    }

    void Update()
    {
        int tmpScore = vehicleManager.GetScore();
        if (score != tmpScore) // 점수 변동이 있을 경우
        {
            score = tmpScore;
            if (scoreIndex < score && score - scoreIndex < 9)
            {
                transform.GetChild(score - scoreIndex - 1).gameObject.SetActive(true);
                anim.Play(animStates[score - scoreIndex - 1].name);
            }
            if (score - scoreIndex == 9)
            {
                anim.Play(animStates[score - scoreIndex - 1].name);
            }
            if (score - scoreIndex == 10)
            {
                anim.Play(animStates[10 - (scoreIndex / 10)].name); // scoreIndex가 10이면 10번째, 0이면 9번째 클립 플레이
            }
        }
    }
}
