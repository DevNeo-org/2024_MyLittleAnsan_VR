using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //����� �迭
    public AudioSource[] audioArray;

    public void PlaySound(int n)
    {
        //����� ���
        audioArray[n].Play(); 
    }
}
