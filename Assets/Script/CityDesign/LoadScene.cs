using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //��ư�� ������ ���� �ҷ����� �޼ҵ�
    public void SceneTransform(int sceneNum)
    {
        //sceneNum�� ���� �� ���� �ε�
        SceneManager.LoadScene(sceneNum);
    }
}
