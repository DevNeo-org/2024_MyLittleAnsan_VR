using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    bool isHovering = false;
    public int sceneNum; 

    void Update()
    {
        if (isHovering)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                SceneTransform(sceneNum);
        }
    }
    //버튼을 누르면 씬을 불러오는 메소드
    void SceneTransform(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
    public void IsHovering(int n)
    {
        isHovering = !isHovering;
        sceneNum = n;

    }
}
