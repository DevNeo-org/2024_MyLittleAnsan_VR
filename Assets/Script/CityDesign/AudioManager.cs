using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] audioArray;

    public void PlaySound(int n)
    {
        switch(n)
        {
            case 0:
                audioArray[0].Play(); break;
            case 1:
                audioArray[1].Play(); break;
            case 2:
                audioArray[2].Play(); break;
            case 3:
                audioArray[3].Play(); break;
            default:
                audioArray[0].Play(); break;
        }
    }
}
