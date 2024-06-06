using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;
using static OVRInput;
using System.Linq;
using OVR.OpenVR;

public class Gun : MonoBehaviour
{
    [SerializeField] ParticleSystem[] _bucketPop;   // 색 변경시 파티클
    [SerializeField] GameObject gunObject;          // 페인트총 오브젝트
    [SerializeField] Material[] gunMaterials;       // 페인트총 색 변경 Material
    [SerializeField] Material[] ballMaterial;       // 페인트볼 변경 Material
    [SerializeField] private GameObject board;      // 색칠되는 보드
    [SerializeField] private GameObject ball;       // 발사되는 페인트볼 prefab
    [SerializeField] private float vibSize = 0.2f;  // 발사 진동 크기
    [SerializeField] private GameObject changeSound;    // 색 변경 사운드

    private Ball ballCS;
    private Renderer ballRenderer;
    private Renderer gunRenderer;
    private float nextShoot;
    private TextileManager textileManager;
    private bool isStart = false;
    private bool isEnd = false;

    public float ballSpeed = 15f;       // 페인트볼 발사 속도
    public float shootRate = 0.2f;      // 발사 가능 간격
    public int colorCode;               // 색 코드
    public OVRInput.Controller controller;  // 컨트롤러


    void Start()
    {
        textileManager = FindAnyObjectByType<TextileManager>();

        gunRenderer = gunObject.GetComponent<Renderer>();
        ballRenderer = ball.GetComponent<Renderer>();
        ballCS = ball.GetComponent<Ball>();

        // 페인트 색 초기화
        gunRenderer.material = gunMaterials[0];
        ballRenderer.material = ballMaterial[0];
        ballCS.ChangeColorCode(0);
        colorCode = 0;
    }
    private void FixedUpdate()
    {
        // Trigger input 확인 

        if (Get(Button.SecondaryIndexTrigger) && Time.time > nextShoot && isStart && !textileManager.IsMenuOn() && !isEnd)
        {
            nextShoot = Time.time + shootRate;
            Shoot();
        }
    }

    // 페인트 발사 함수
    private void Shoot()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(ShootTriggerHaptics());
        GameObject paintBall = Instantiate(ball, gunObject.transform.position, gunObject.transform.rotation);
        paintBall.GetComponent<Rigidbody>().velocity = paintBall.transform.forward * ballSpeed;
    }

    // 페인트 및 총 색 변경 함수
    public void ChangeColor(int code)
    {
        _bucketPop[code].Play();
        gunRenderer.material = gunMaterials[code];
        ballRenderer.material = ballMaterial[code];
        ballCS.ChangeColorCode(code);
        colorCode = code;
    }

    // 색 변경용 페인트통 충돌 감지
    private void OnTriggerEnter(Collider other)
    {
        if (!textileManager.IsMenuOn() && isStart && !isEnd)
        {
            /*
             * 충돌한 페인트통 감지
             * 물 튀는 사운드 재생 및 진동 재생
             * 색 변경
             */
            if (other.tag == "BlueBucket")
            {
                changeSound.GetComponent<AudioSource>().Play();
                StartCoroutine(TriggerHaptics());
                ChangeColor(0);
            }
            else if (other.tag == "RedBucket")
            {
                changeSound.GetComponent<AudioSource>().Play();
                StartCoroutine(TriggerHaptics());
                ChangeColor(1);
            }
            else if (other.tag == "YellowBucket")
            {
                changeSound.GetComponent<AudioSource>().Play();
                StartCoroutine(TriggerHaptics());
                ChangeColor(2);
            }
            else if (other.tag == "BlackBucket")
            {
                changeSound.GetComponent<AudioSource>().Play();
                StartCoroutine(TriggerHaptics());
                ChangeColor(3);
            }
        }
    }

    // 색 변경 진동 
    IEnumerator TriggerHaptics()
    {
        OVRInput.SetControllerVibration(2.5f, 2.5f, controller);
        yield return new WaitForSeconds(0.5f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }

    // 총 발사 진동
    IEnumerator ShootTriggerHaptics()
    {
        OVRInput.SetControllerVibration(vibSize, vibSize, controller);
        yield return new WaitForSeconds(0.2f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }

    // 게임 시작 함수
    public void StartGame()
    {
        isStart = true;
    }

    // 게임 종료 함수
    public void EndGame()
    {
        isEnd = true;
    }
}
