using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioArray;

    public void PlaySound(int n)
    {
        audioArray[n].Play(); 
    }
}
