using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothes : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnim()
    {
        Time.timeScale = 1;
        anim.SetBool("isClear", true);
        Invoke("PauseGame", 3f);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }
}
