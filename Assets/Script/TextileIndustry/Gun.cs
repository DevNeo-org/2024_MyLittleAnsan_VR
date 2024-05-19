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
    private Ball ballCS;
    private Renderer ballRenderer;
    public float ballSpeed = 15f;
    public float shootRate = 0.2f;
    private float nextShoot;

    public int colorCode;


    void Start()
    {
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

        if (Get(Button.PrimaryIndexTrigger) && Time.time > nextShoot)
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
        if(other.tag == "BlueBucket")
        {
            ChangeColor(0);
        }
        else if(other.tag == "RedBucket")
        {
            ChangeColor(1);
        }
        else if (other.tag == "YellowBucket")
        {
            ChangeColor(2);
        }
        else if (other.tag == "BlackBucket")
        {
            ChangeColor(3);
        }
    }
}
