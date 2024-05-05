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

    //buildPosition에 빌딩 오브젝트가 닿으면 빌딩 생성
    private void OnTriggerEnter(Collider Collider)
    {
        if (Collider.tag == "Buildings")
        {
            playBuildEffect();
            //buildPosition에 빌딩 프리팹 생성
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
            //buildingSample 오브젝트 비활성화
            Collider.gameObject.SetActive(false);
            //buildPosition의 콜라이더 비활성화
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //SelectArea 오브젝트 삭제
            Destroy(transform.parent.gameObject);
            //선택 이펙트 삭제
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
        //파티클프리팹 생성
        buildEffectObject = Instantiate(buildEffectPrefab, transform.position, Quaternion.identity);
        ParticleSystem instantEffect = buildEffectObject.GetComponent<ParticleSystem>();
        //파티클 생성 위치와 크기 설정
        buildEffect.transform.position = transform.position;
        buildEffect.transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
        //파티클 재생
        buildEffect.Play();
    }
}
