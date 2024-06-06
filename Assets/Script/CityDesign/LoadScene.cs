using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    bool isHovering = false;
    public int sceneNum;
    GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

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
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            gameManager.GetComponent<AudioManager>().PlaySound(1);
        }
        SceneManager.LoadScene(sceneNum);
    }
}
