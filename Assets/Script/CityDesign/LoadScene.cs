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
    //��ư�� ������ ���� �ҷ����� �޼ҵ�
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
