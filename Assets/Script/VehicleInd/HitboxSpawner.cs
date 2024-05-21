using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxSpawner : MonoBehaviour
{
    [SerializeField] GameObject hitboxPrefab; // 히트박스 적용
    [SerializeField] Transform[] points; // 시작 위치
    [SerializeField] GameObject finalPointObject; // 최종 위치 부모 오브젝트
    public Transform[] finalPoints; // 최종 위치
    private float beat = 2f; // 히트박스 소환 주기
    private float timer = 0;
    private VehicleManager vehicleManager;
    private GameObject timerText;
    public bool leftWrenchOn = false;
    public bool rightWrenchOn = false;
    void Start()
    {
        vehicleManager = FindAnyObjectByType<VehicleManager>();
        timerText = GameObject.Find("Timer");
    }

    void Update()
    {
        if (!leftWrenchOn || !rightWrenchOn) { return; } // 렌치가 들려있지 않을 시 return
        finalPointObject.SetActive(true);
        timerText.GetComponent<Timer>().StartGame();
        if (timer > beat)
        {
            int num = Random.Range(0, 4); // 랜덤으로 스폰 위치 설정
            GameObject hitbox = Instantiate(hitboxPrefab, points[num]);
            hitbox.transform.localPosition = Vector3.zero;
            hitbox.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
            hitbox.GetComponent<HitboxMovement>().SetFinalPoint(num);
            hitbox.transform.GetChild(0).GetComponent<VehicleHitbox>().SetVehicleManager(vehicleManager);
            PlaySound();
            timer -= beat;
        }
        timer += Time.deltaTime;
    }
    public void PickUp(bool isLeft)
    {
        if (isLeft){ leftWrenchOn = true; }
        else { rightWrenchOn = true; }
    }
    private void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
