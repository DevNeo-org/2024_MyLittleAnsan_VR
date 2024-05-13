using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    //버튼을 누르면 씬을 불러오는 메소드
    public void SceneTransform(int sceneNum)
    {
        //sceneNum에 따라 각 씬을 로드
        SceneManager.LoadScene(sceneNum);
    }
}
