using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class DataManager : MonoBehaviour
{
    string[] buildingSamples = new string[] { "sample1", "sample2", "sample3", "sample4", "sample5" };
    string[] SceneNames = new string[] { "AutoMobile", "Electronic", "Textile"};
    public string[] areas = new string[] { "area1", "area2", "area3" };

    void Update()
    {
        //�׽�Ʈ��
        if (Input.GetKeyDown("c"))
        {
            //��ū ȹ��
            PlayerPrefs.SetInt("Token", System.Convert.ToInt16(true));
            //���� ���� �ε��� �ҷ�����
            PlayerPrefs.SetInt(SceneNames[2], System.Convert.ToInt16(true));
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown("d"))
        {
            DataClear();
            SceneManager.LoadScene(1);
        }
    }
    //���� ���۽� ��� ������ ����
    public void DataClear()
    {
        PlayerPrefs.DeleteAll();
    }

    //�� ü���� Ŭ���� ���� ����
    public void SetClear()
    {
        //��ū ȹ��
        PlayerPrefs.SetInt("Token", System.Convert.ToInt16(true));
        //���� ���� �ε��� �ҷ�����
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt(SceneNames[sceneNum - 2], System.Convert.ToInt16(true));
    }

    //�� ü���� Ŭ���� ���� ��ȯ
    public bool GetClear(int sceneNum)
    {
        bool isClear = System.Convert.ToBoolean(PlayerPrefs.GetInt(SceneNames[sceneNum]));
        return isClear;
    }

    public bool GetSampleDestroyed(int sampleNum)
    {
        bool isDestroyed = System.Convert.ToBoolean(PlayerPrefs.GetInt(buildingSamples[sampleNum]));
        return isDestroyed;
    }

    //�ǹ� ���� ����
    public void SampleDestroyed(int num)
    {
        PlayerPrefs.SetInt(buildingSamples[num], System.Convert.ToInt16(true));
    }
    //���� �Ǽ� ���� ��ȯ
    public int GetAreaState(int areaNum)
    {
        return PlayerPrefs.GetInt(areas[areaNum]);
    }
    //���� ��ü Ŭ���� ���� ����
    public void SetGameClear()
    {
        PlayerPrefs.SetInt("GameClear", System.Convert.ToInt16(true));
    }
}
