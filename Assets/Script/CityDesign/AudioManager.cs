using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //오디오 배열
    public AudioSource[] audioArray;

    public void PlaySound(int n)
    {
        //오디오 재생
        audioArray[n].Play(); 
    }
}
