using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class DataManager : MonoBehaviour
{
    public bool autoMobClear;
    public bool elecClear;
    public bool textileClear;

    private void Start()
    {
        //����� ������ ���� ���ٸ� �ʱ�ȭ
        if (!(PlayerPrefs.HasKey("AutoMobile") || PlayerPrefs.HasKey("Electronic") || PlayerPrefs.HasKey("Textile")))
        {
            autoMobClear = false;
            elecClear = false;
            textileClear = false;
        }
        //����� �����Ͱ� �ִٸ� �ҷ�����
        else
        {
            autoMobClear = GetClear(1);
            elecClear = GetClear(2);
            textileClear = GetClear(3);
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
        //���� ���� �ε��� �ҷ�����
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        switch (sceneNum)
        {
            //�� ���� Ŭ����� ����
            case 1:
                PlayerPrefs.SetInt("AutoMobile", System.Convert.ToInt16(true));
                break;
            case 2:
                PlayerPrefs.SetInt("Electronic", System.Convert.ToInt16(true));
                break;
            case 3:
                PlayerPrefs.SetInt("Textile", System.Convert.ToInt16(true));
                break;
            default:
                break;

        } 
    }

    //�� ü���� Ŭ���� ���� ��ȯ
    public bool GetClear(int sceneNum)
    {
        bool isClear;
        switch (sceneNum)
        {
            //�� ���� Ŭ���� ���� ��ȯ
            case 1:
                isClear = System.Convert.ToBoolean(PlayerPrefs.GetInt("AutoMobile"));
                break;
            case 2:
                isClear = System.Convert.ToBoolean(PlayerPrefs.GetInt("Electronic"));
                break;
            case 3:
                isClear = System.Convert.ToBoolean(PlayerPrefs.GetInt("Textile"));
                break;
            default:
                isClear = false;
                break;
        }
        return isClear;
    }

}
