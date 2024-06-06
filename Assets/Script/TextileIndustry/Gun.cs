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
    [SerializeField] ParticleSystem[] _bucketPop;   // �� ����� ��ƼŬ
    [SerializeField] GameObject gunObject;          // ����Ʈ�� ������Ʈ
    [SerializeField] Material[] gunMaterials;       // ����Ʈ�� �� ���� Material
    [SerializeField] Material[] ballMaterial;       // ����Ʈ�� ���� Material
    [SerializeField] private GameObject board;      // ��ĥ�Ǵ� ����
    [SerializeField] private GameObject ball;       // �߻�Ǵ� ����Ʈ�� prefab
    [SerializeField] private float vibSize = 0.2f;  // �߻� ���� ũ��
    [SerializeField] private GameObject changeSound;    // �� ���� ����

    private Ball ballCS;
    private Renderer ballRenderer;
    private Renderer gunRenderer;
    private float nextShoot;
    private TextileManager textileManager;
    private bool isStart = false;
    private bool isEnd = false;

    public float ballSpeed = 15f;       // ����Ʈ�� �߻� �ӵ�
    public float shootRate = 0.2f;      // �߻� ���� ����
    public int colorCode;               // �� �ڵ�
    public OVRInput.Controller controller;  // ��Ʈ�ѷ�


    void Start()
    {
        textileManager = FindAnyObjectByType<TextileManager>();

        gunRenderer = gunObject.GetComponent<Renderer>();
        ballRenderer = ball.GetComponent<Renderer>();
        ballCS = ball.GetComponent<Ball>();

        // ����Ʈ �� �ʱ�ȭ
        gunRenderer.material = gunMaterials[0];
        ballRenderer.material = ballMaterial[0];
        ballCS.ChangeColorCode(0);
        colorCode = 0;
    }
    private void FixedUpdate()
    {
        // Trigger input Ȯ�� 

        if (Get(Button.SecondaryIndexTrigger) && Time.time > nextShoot && isStart && !textileManager.IsMenuOn() && !isEnd)
        {
            nextShoot = Time.time + shootRate;
            Shoot();
        }
    }

    // ����Ʈ �߻� �Լ�
    private void Shoot()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(ShootTriggerHaptics());
        GameObject paintBall = Instantiate(ball, gunObject.transform.position, gunObject.transform.rotation);
        paintBall.GetComponent<Rigidbody>().velocity = paintBall.transform.forward * ballSpeed;
    }

    // ����Ʈ �� �� �� ���� �Լ�
    public void ChangeColor(int code)
    {
        _bucketPop[code].Play();
        gunRenderer.material = gunMaterials[code];
        ballRenderer.material = ballMaterial[code];
        ballCS.ChangeColorCode(code);
        colorCode = code;
    }

    // �� ����� ����Ʈ�� �浹 ����
    private void OnTriggerEnter(Collider other)
    {
        if (!textileManager.IsMenuOn() && isStart && !isEnd)
        {
            /*
             * �浹�� ����Ʈ�� ����
             * �� Ƣ�� ���� ��� �� ���� ���
             * �� ����
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

    // �� ���� ���� 
    IEnumerator TriggerHaptics()
    {
        OVRInput.SetControllerVibration(2.5f, 2.5f, controller);
        yield return new WaitForSeconds(0.5f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }

    // �� �߻� ����
    IEnumerator ShootTriggerHaptics()
    {
        OVRInput.SetControllerVibration(vibSize, vibSize, controller);
        yield return new WaitForSeconds(0.2f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }

    // ���� ���� �Լ�
    public void StartGame()
    {
        isStart = true;
    }

    // ���� ���� �Լ�
    public void EndGame()
    {
        isEnd = true;
    }
}
