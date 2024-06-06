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
    public GameObject dialogManager;

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
            
            //buildPosition�� ���� ������ ����
            string bulidingType = Collider.gameObject.name;
            //��ư�� �������� UI Ȱ��ȭ
            dialogManager.GetComponent<DialogManager>().ShowSelectButtonUI();

            for (int i = 0; i<5; i++)
            {
                if (SampleNames[i] == bulidingType)
                {
                    //�ǹ� ���� ���
                    PlayerPrefs.SetInt(buildingSamples[i], System.Convert.ToInt16(true));
                    Vector3 buildPos = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
                    //�ǹ� ����
                    Instantiate(builidngPrefabs[i],buildPos, builidngPrefabs[i].transform.rotation);
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
            //��ƽ ����
            OVRInput.SetControllerVibration(0f, 0f, controller);
            //���� ����Ʈ ����
            selctEffect = GameObject.Find("Area_circles_blue(Clone)");
            Destroy(selctEffect);
            //���� �Ϸ�
            if (PlayerPrefs.GetInt(areas[0]) != 0 && PlayerPrefs.GetInt(areas[1]) != 0 && PlayerPrefs.GetInt(areas[2]) != 0)
            {
                dataManager.GetComponent<DataManager>().SetGameClear();
                gameManager.GetComponent<Lobby>().playCompleteEffect();
                dialogManager.GetComponent<DialogManager>().SetClearUI();
                gameManager.GetComponent<AudioManager>().PlaySound(4);
            }
        }
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
        buildEffect.transform.localScale = new Vector3(0.056f, 0.056f, 0.056f);
        //��ƼŬ ���
        buildEffect.Play();
    }

    //��ƽ
    IEnumerator TriggerHaptics()
    {
        yield return new WaitForSeconds(0.5f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }
}
