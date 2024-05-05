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

    //버튼을 누르면 씬을 불러오는 메소드
    public void LoadScene(int sceneNum)
    {
        //sceneNum에 따라 각 씬을 로드
        SceneManager.LoadScene(sceneNum);
    }
}
