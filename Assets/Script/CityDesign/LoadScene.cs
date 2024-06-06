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
        //트리거 키로 버튼 누르기
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

    //버튼을 누르면 씬을 불러오는 메소드
    void SceneTransform(int sceneNum)
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            gameManager.GetComponent<AudioManager>().PlaySound(1);
        }
        SceneManager.LoadScene(sceneNum);
    }
}
