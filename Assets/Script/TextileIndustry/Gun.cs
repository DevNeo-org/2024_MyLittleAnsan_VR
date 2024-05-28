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
    [SerializeField] ParticleSystem[] _bucketPop;

    [SerializeField] GameObject gunObject;
    private Renderer gunRenderer;

    [SerializeField] Material[] gunMaterials;
    [SerializeField] Material[] ballMaterial;

    [SerializeField] private GameObject board;

    [SerializeField] private GameObject ball;
    public OVRInput.Controller controller;
    [SerializeField] private float vibSize = 0.2f;
    [SerializeField] private GameObject changeSound;

    private Ball ballCS;
    private Renderer ballRenderer;
    public float ballSpeed = 15f;
    public float shootRate = 0.2f;
    private float nextShoot;

    public int colorCode;

    private TextileManager textileManager;

    private bool isStart = false;


    void Start()
    {
        textileManager = FindAnyObjectByType<TextileManager>();

        gunRenderer = gunObject.GetComponent<Renderer>();
        ballRenderer = ball.GetComponent<Renderer>();
        ballCS = ball.GetComponent<Ball>();

        // initiate color code
        gunRenderer.material = gunMaterials[0];
        ballRenderer.material = ballMaterial[0];
        ballCS.ChangeColorCode(0);
        colorCode = 0;
    }
    private void FixedUpdate()
    {
        // input 

        if (Get(Button.SecondaryIndexTrigger) && Time.time > nextShoot && isStart && !textileManager.IsMenuOn())
        {
            nextShoot = Time.time + shootRate;
            Shoot();
        }

        /* if (Input.GetMouseButtonDown(0) && Time.time > nextShoot)
        {
            nextShoot = Time.time + shootRate;
            Shoot();
        }
        */
    }

    // shoot paint ball
    private void Shoot()
    {
        GetComponent<AudioSource>().Play();
        StartCoroutine(ShootTriggerHaptics());
        GameObject paintBall = Instantiate(ball, gunObject.transform.position, gunObject.transform.rotation);
        paintBall.GetComponent<Rigidbody>().velocity = paintBall.transform.forward * ballSpeed;
    }

    // change color of paint and gun
    public void ChangeColor(int code)
    {
        _bucketPop[code].Play();
        gunRenderer.material = gunMaterials[code];
        ballRenderer.material = ballMaterial[code];
        ballCS.ChangeColorCode(code);
        colorCode = code;
    }

    // check Trigger with paint bucket
    private void OnTriggerEnter(Collider other)
    {
        if (!textileManager.IsMenuOn() && isStart)
        {
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

    IEnumerator TriggerHaptics()
    {
        OVRInput.SetControllerVibration(2.5f, 2.5f, controller);
        yield return new WaitForSeconds(0.5f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }

    IEnumerator ShootTriggerHaptics()
    {
        OVRInput.SetControllerVibration(vibSize, vibSize, controller);
        yield return new WaitForSeconds(0.2f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }

    public void StartGame()
    {
        isStart = true;
    }
}
