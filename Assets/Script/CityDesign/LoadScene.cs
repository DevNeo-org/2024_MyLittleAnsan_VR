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
        //Ʈ���� Ű�� ��ư ������
        if (isHovering)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                SceneTransform(sceneNum);
        }
    }

    public void IsHovering(int n)
    {
        isHovering = !isHovering;
        sceneNum = n;
    }

    //��ư�� ������ ���� �ҷ����� �޼ҵ�
    void SceneTransform(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
