using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lobby : MonoBehaviour
{
    public GameObject[] samplePrefabs;
    public GameObject[] buttonObjects;
    public GameObject dataManager;

    public bool token;

    public string[] samples = new string[] { "sample1", "sample2", "sample3", "sample4", "sample5" };
    public bool[] isClear = new bool[3];
    public bool[] sampleDestroyed = new bool[5];

    void Start()
    {
        //�� �ε� �� ��ū�� ���ٸ� �� ó���̹Ƿ� �ʱ�ȭ
        //��ū�� ������ �Ǽ� �Ұ��� �� �� ��ȯ ����
        if (!(PlayerPrefs.HasKey("Token")))
        {
            //��� ���尪 ����
            dataManager.GetComponent<DataManager>().DataClear();
            //��ū �ʱ�ȭ
            PlayerPrefs.SetInt("Token", System.Convert.ToInt16(false));
            token = false;
            //�ǹ� ���� ��� ���� �ʱ�ȭ
            for (int i = 0; i < 5; i++)
            {
                sampleDestroyed[i] = false;
            }
            //�� Ŭ���� ���� �ʱ�ȭ
            for(int i = 0; i < 3; i++)
            {
                isClear[i] = false;
            }
        }

        //��ū�� ������ �Ǽ� ���� �� �� ��ȯ �Ұ���
        else
        {
            token = true;
            //�� Ŭ���� ���� �ҷ�����
            for (int i = 0; i < 3; i++)
            {
                isClear[i] = dataManager.GetComponent<DataManager>().GetClear(i);
            }
            for (int i = 0; i < 5; i++)
            {
                sampleDestroyed[i] = dataManager.GetComponent<DataManager>().GetSampleDestroyed(i);
            }
        }
        makePrefab();
    }

    void makePrefab()
    {
        //�ǹ� ������ ������ �ʾ����� ������ ����
        for (int i = 0; i < 5; i++)
        {
            //bool isDestroyed = System.Convert.ToBoolean(PlayerPrefs.GetInt(samples[i]));
            if (!sampleDestroyed[i])
            {
                Instantiate(samplePrefabs[i], samplePrefabs[i].transform.position, samplePrefabs[i].transform.rotation);
            }
        }
        //�� Ŭ���� �Ǿ����� �� ��ȯ ��ư ��Ȱ��ȭ
        for (int i = 0; i < 3; i++)
        {
            if (isClear[i])
            {
                buttonObjects[i].SetActive(false);
            }
        }
    }

    
}
