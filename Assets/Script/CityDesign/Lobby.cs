using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //��ư�� ������ ���� �ҷ����� �޼ҵ�
    public void LoadScene(int sceneNum)
    {
        //sceneNum�� ���� �� ���� �ε�
        SceneManager.LoadScene(sceneNum);
    }
}
