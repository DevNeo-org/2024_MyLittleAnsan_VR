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

    [SerializeField] GameObject ball;
    private ParticleSystem particle;
    private Renderer particleRenderer;
    [SerializeField] Material[] paintMats;

    [SerializeField] GameObject gun;
    [SerializeField] GameObject gunModel;
    private Renderer gunRenderer;
    private Rigidbody rigid;

    [SerializeField] private int penSize = 35;

    private Color[] _color;
    private Whiteboard _whiteboard;
    [SerializeField] Material[] mats;
    [SerializeField] Material[] gunMats;
    private Vector3 shootPos, shootDir;
    private RaycastHit _touch;
    private Vector2 _touchPos;

    [SerializeField] GameObject board;


    void Start()
    {
        gunRenderer = gunModel.GetComponent<Renderer>();
        rigid = gun.GetComponent<Rigidbody>();
        _color = Enumerable.Repeat(mats[0].color, penSize * penSize).ToArray();
        particle = ball.GetComponent<ParticleSystem>();
        particleRenderer = particle.GetComponent<Renderer>();
    }
    private void FixedUpdate()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            Shoot();
        }
        ball.transform.rotation = gun.transform.rotation;
    }

    private void Shoot()
    {
        shootPos = rigid.transform.position;
        shootDir = Camera.main.transform.forward;
        particle.Play();
    }
    public void Draw()
    {
        if (Physics.Raycast(shootPos, shootDir, out _touch, 50f))
        {
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (penSize / 2));

                if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x)
                {
                    return;
                }

                for (int i=-1; i<=1; i++)
                {
                    for (int j=-2; j<=2; j++)
                    {
                        _whiteboard.texture.SetPixels(x + i*(penSize), y + j * (penSize), penSize, penSize, _color);
                    }
                }
                _whiteboard.texture.SetPixels(x + (penSize) / 2, y + 3 * (penSize), penSize, penSize, _color);
                _whiteboard.texture.SetPixels(x - (penSize) / 2, y + 3 * (penSize), penSize, penSize, _color);
                _whiteboard.texture.SetPixels(x + (penSize) / 2, y - 3 * (penSize), penSize, penSize, _color);
                _whiteboard.texture.SetPixels(x - (penSize) / 2, y - 3 * (penSize), penSize, penSize, _color);

                _whiteboard.texture.Apply();

                return;
            }
        }

        _whiteboard = null;

    }

    public void ChangeColor(int code)
    {
        _bucketPop[code].Play();
        _color = Enumerable.Repeat(mats[code].color, penSize * penSize).ToArray();
        gunRenderer.material = gunMats[code];
        particleRenderer.material = paintMats[code];
        board.GetComponent<Whiteboard>().ChangeCode(code);
    }
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
