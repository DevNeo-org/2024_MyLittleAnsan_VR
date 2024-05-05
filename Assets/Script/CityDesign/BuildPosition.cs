using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPosition : MonoBehaviour
{

    public GameObject[] builidngPrefabs;
    public bool isBuildComplete = false;
    GameObject selctEffect;

    public ParticleSystem buildEffect;
    public GameObject buildEffectPrefab;
    GameObject buildEffectObject;

    //buildPosition�� ���� ������Ʈ�� ������ ���� ����
    private void OnTriggerEnter(Collider Collider)
    {
        if (Collider.tag == "Buildings")
        {
            playBuildEffect();
            //buildPosition�� ���� ������ ����
            string bulidingType = Collider.gameObject.name;
            switch (bulidingType)
            {
                case "BuildingSample1":
                    Instantiate(builidngPrefabs[0], transform.position, Quaternion.identity);
                    break;

                case "BuildingSample2":
                    Instantiate(builidngPrefabs[1], transform.position, Quaternion.identity);
                    break;
                case "BuildingSample3":
                    Instantiate(builidngPrefabs[2], transform.position, Quaternion.identity);
                    break;
                case "BuildingSample4":
                    Instantiate(builidngPrefabs[3], transform.position, Quaternion.identity);
                    break;
                case "BuildingSample5":
                    Instantiate(builidngPrefabs[4], transform.position, Quaternion.identity);
                    break;
                default:
                    Instantiate(builidngPrefabs[0], transform.position, Quaternion.identity);
                    break;

            }
            //buildingSample ������Ʈ ��Ȱ��ȭ
            Collider.gameObject.SetActive(false);
            //buildPosition�� �ݶ��̴� ��Ȱ��ȭ
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //SelectArea ������Ʈ ����
            Destroy(transform.parent.gameObject);
            //���� ����Ʈ ����
            selctEffect = GameObject.Find("Area_circles_blue(Clone)");
            Destroy(selctEffect);
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
        buildEffect.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        //��ƼŬ ���
        buildEffect.Play();
    }
}
