using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainCarObject : MonoBehaviour
{
    [SerializeField] int scoreIndex; // 0 �Ǵ� 10���� ����, 0�̸� �������� �̵��ϴ� �ִϸ��̼�, 10�̸� ����
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
        if (score != tmpScore) // ���� ������ ���� ���
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
                anim.Play(animStates[10 - (scoreIndex / 10)].name); // scoreIndex�� 10�̸� 10��°, 0�̸� 9��° Ŭ�� �÷���
            }
        }
    }
}
