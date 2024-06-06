using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothes : MonoBehaviour
{
    // 게임 종료 연출
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // 종료 연출 실행 함수
    public void PlayAnim()
    {
        Time.timeScale = 1;
        anim.SetBool("isClear", true);
        Invoke("PauseGame", 5f);
    }

    // 게임 정지 함수
    private void PauseGame()
    {
        Time.timeScale = 0;
    }

}
