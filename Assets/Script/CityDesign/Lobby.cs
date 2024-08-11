using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lobby : MonoBehaviour
{
    //�ǹ� ���� ������
    public GameObject[] samplePrefabs;
    //�ǹ� ������
    public GameObject[] buildingPrefabs;
    //�� ��ȯ ��ư ������Ʈ
    public GameObject[] buttonObjects;
    //�Ǽ� ���� ������Ʈ
    public GameObject[] AreaObjects;
    //���� ������ ���� ������Ʈ
    public GameObject dataManager;
    //�Ǽ� ��ū
    public bool token;
    
    public string[] samples = new string[] { "sample1", "sample2", "sample3", "sample4", "sample5" };
    public string[] sampleClones = new string[] { "BuildingSample1(Clone)", "BuildingSample2(Clone)", "BuildingSample3(Clone)", "BuildingSample4(Clone)", "BuildingSample5(Clone)" };
    public string[] areas = new string[] { "area1","area2","area3"};
    
    public bool[] isClear = new bool[3];
    public bool[] sampleDestroyed = new bool[5];
    public int[] areaBuild = new int[3];

    public ParticleSystem completeEffect;
    public GameObject completeEffectPrefab;
    public GameObject DialogManager;
    GameObject completeEffectObject;

    GameObject buildingPrefab;
    GameObject area;

    bool isDialog;
    public bool isOnArea;

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
            
            for(int i = 0; i < 3; i++)
            {
                //�� Ŭ���� ���� �ʱ�ȭ
                isClear[i] = false;
                //���� �Ǽ� ���� �ʱ�ȭ
                // 0 : �̰Ǽ� , 1 ~ 5 : �� ��ȣ�� �ǹ� �Ǽ�
                areaBuild[i] = 0;
            }
        }

        //��ū�� ������ �Ǽ� ���� �� �� ��ȯ �Ұ���
        else
        {
            token = true;
            
            for (int i = 0; i < 3; i++)
            {
                //�� Ŭ���� ���� �ҷ�����
                isClear[i] = dataManager.GetComponent<DataManager>().GetClear(i);
                //���� �Ǽ� ���� �ҷ�����
                areaBuild[i] = dataManager.GetComponent<DataManager>().GetAreaState(i);
            }
            for (int i = 0; i < 5; i++)
            {
                //�ǹ� ���� ��� ���� �ҷ�����
                sampleDestroyed[i] = dataManager.GetComponent<DataManager>().GetSampleDestroyed(i);
            }
        }
        makePrefab();
    }
    
    void Update()
    {
            //�ǹ� ������ �Ʒ��� �������� ���� �� �����
                for (int i = 0; i < 5; i++)
                {
                    //���� ������ ���� �ǹ��� ���
                    if (!sampleDestroyed[i])
                    {
                        buildingPrefab = GameObject.Find(sampleClones[i]);
                        if (buildingPrefab != null)
                        {
                            //�ǹ��� �Ʒ��� ������ ���
                            if (buildingPrefab.transform.position.y < -5)
                            {
                                Destroy(buildingPrefab);
                                Instantiate(samplePrefabs[i], samplePrefabs[i].transform.position, samplePrefabs[i].transform.rotation);
                            }
                        }
                    }
                    
                }
        

        isDialog = DialogManager.GetComponent<DialogManager>().SendOnDialog();
        //��ȭ�� OR ���� �Ϸ�  => ���� ȭ��ǥ ��Ȱ��ȭ, ��ư ȭ��ǥ ��Ȱ��ȭ
        if (isDialog || System.Convert.ToBoolean(PlayerPrefs.GetInt("GameClear")))
        {
            if (System.Convert.ToBoolean(PlayerPrefs.GetInt("Token")))
            {
                for (int i = 0; i < 3; i++)
                {
                    if (AreaObjects[i] != null)
                    {
                        AreaObjects[i].transform.GetChild(0).gameObject.SetActive(false);
                    }
                    if (!isClear[i])
                    {
                        buttonObjects[i].SetActive(false);
                    }
                }
            }
        }
        //��ȭ�� X, ��ū O => ���� ȭ��ǥ Ȱ��ȭ, ��ư ȭ��ǥ ��Ȱ��ȭ
        if (!isDialog)
        {
            if (System.Convert.ToBoolean(PlayerPrefs.GetInt("Token"))){
                for (int i = 0; i < 3; i++)
                {
                    if (AreaObjects[i] != null)
                    {
                        AreaObjects[i].transform.GetChild(0).gameObject.SetActive(true);
                    }
                    if (!isClear[i])
                    {
                        buttonObjects[i].SetActive(false);
                    }
                }
            }
            //��ȭ�� X, ��ū X => ���� ȭ��ǥ ��Ȱ��ȭ, ��ư ȭ��ǥ Ȱ��ȭ
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    if (AreaObjects[i] != null)
                    {
                        AreaObjects[i].SetActive(false);
                    }
                    if (!isClear[i])
                    {
                        buttonObjects[i].SetActive(true);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                if (AreaObjects[i] != null)
                {
                    AreaObjects[i].transform.GetChild(0).gameObject.SetActive(false);
                }
                if (!isClear[i])
                {
                    buttonObjects[i].gameObject.SetActive(false);
                }
            }
        }
    }

    void makePrefab()
    {
        //�ǹ� ������ ������ �ʾ����� ������ ����
        for (int i = 0; i < 5; i++)
        {
            if (!sampleDestroyed[i])
            {
                Instantiate(samplePrefabs[i], samplePrefabs[i].transform.position, samplePrefabs[i].transform.rotation);
            }
        }
        
        for (int i = 0; i < 3; i++)
        {
            //�� Ŭ���� �Ǿ����� �� ��ȯ ��ư ��Ȱ��ȭ
            if (isClear[i])
            {
                buttonObjects[i].SetActive(false);
            }
            //1. ������ �ǹ��� �Ǽ��Ǿ��ִٸ� ���� ������Ʈ ��Ȱ��ȭ
            //2. �ش� ������ �Ǽ��� �ǹ� ������ ����
            if (areaBuild[i] != 0)
            {
                AreaObjects[i].SetActive(false);
                Instantiate(buildingPrefabs[areaBuild[i]-1], AreaObjects[i].transform.position, buildingPrefabs[areaBuild[i]-1].transform.rotation);
            }
        }
    }
    //���� Ŭ���� ����Ʈ
    public void playCompleteEffect()
    {
        //��ƼŬ������ ����
        completeEffectObject = Instantiate(completeEffectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect1 = completeEffectObject.GetComponent<ParticleSystem>();
        //��ƼŬ ���� ��ġ�� ũ�� ����
        completeEffect.transform.position = transform.position;
        completeEffect.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //��ƼŬ ���
        completeEffect.Play();
        //���� ���

    }

    //�ǹ� ������ �Ǽ� ���� ���� �ִ��� ����
    public void SetIsOnArea(bool torf)
    {
        isOnArea = torf;
    }

    //�ǹ� ������ �Ǽ� ���� ���� �ִ��� ��ȯ
    public bool GetIsOnArea()
    {
        return isOnArea;
    }
}
