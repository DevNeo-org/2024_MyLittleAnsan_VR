using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPosition : MonoBehaviour
{

    public GameObject[] builidngPrefabs;
    public bool isBuildComplete = false;
    GameObject selctEffect;

    public GameObject gameManager;
    public GameObject dataManager;

    public ParticleSystem buildEffect;
    public GameObject buildEffectPrefab;
    GameObject buildEffectObject;
    public OVRInput.Controller controller;
    string[] buildingSamples = new string[] { "sample1", "sample2", "sample3", "sample4", "sample5" };
    string[] SampleNames = new string[] { "BuildingSample1(Clone)", "BuildingSample2(Clone)", "BuildingSample3(Clone)", "BuildingSample4(Clone)", "BuildingSample5(Clone)" };
    string[] areas = new string[] { "area1", "area2", "area3" };



    //buildPosition�� ���� ������Ʈ�� ������ ���� ����
    private void OnTriggerEnter(Collider Collider)
    {
        //��ū�� �ִ��� Ȯ��
        if (Collider.tag == "Buildings" && System.Convert.ToBoolean(PlayerPrefs.GetInt("Token")))
        {
            //ȿ���� ���
            gameManager.GetComponent<AudioManager>().PlaySound(2);
            //�Ǽ� ����Ʈ
            playBuildEffect();
            //��ƽ ����
            StartCoroutine(TriggerHaptics());
            //buildPosition�� ���� ������ ����
            string bulidingType = Collider.gameObject.name;
            for(int i = 0; i<5; i++)
            {
                if (SampleNames[i] == bulidingType)
                {
                    //�ǹ� ���� ���
                    PlayerPrefs.SetInt(buildingSamples[i], System.Convert.ToInt16(true));
                    //�ǹ� ����
                    Instantiate(builidngPrefabs[i], transform.position, builidngPrefabs[i].transform.rotation);
                    //���� ���� Ȯ��
                    string areaName = transform.parent.gameObject.name;
                    for (int j = 0; j < 3 ; j++){
                        if (areas[j] == areaName)
                        {
                            PlayerPrefs.SetInt(areas[j], i+1);
                        }
                    }
                }
            }
            //��ū ���
            PlayerPrefs.SetInt("Token", System.Convert.ToInt16(false));
            //buildingSample ������Ʈ ����
            Destroy(Collider.gameObject);
            //buildPosition�� �ݶ��̴� ��Ȱ��ȭ
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //SelectArea ������Ʈ ����
            Destroy(transform.parent.gameObject);
            //���� ����Ʈ ����
            selctEffect = GameObject.Find("Area_circles_blue(Clone)");
            Destroy(selctEffect);

            if (PlayerPrefs.GetInt(areas[0]) != 0 && PlayerPrefs.GetInt(areas[1]) != 0 && PlayerPrefs.GetInt(areas[2]) != 0)
            {
                gameManager.GetComponent<Lobby>().playCompleteEffect();
                Debug.Log("���� Ŭ����");
            }
        }
    }

    void Update()
    {
        
    }

    public bool returnBuildComplete()
    {
        return isBuildComplete;
    }

    void playBuildEffect()
    {
        //��ƼŬ������ ����
        buildEffectObject = Instantiate(buildEffectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect = buildEffectObject.GetComponent<ParticleSystem>();
        //��ƼŬ ���� ��ġ�� ũ�� ����
        buildEffect.transform.position = transform.position;
        buildEffect.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        //��ƼŬ ���
        buildEffect.Play();
    }

    //��ƽ
    IEnumerator TriggerHaptics()
    {
        OVRInput.SetControllerVibration(1f, 1f, controller);
        yield return new WaitForSeconds(1f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }
}
