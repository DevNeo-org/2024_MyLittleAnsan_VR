using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;
using static OVRInput;
using System.Linq;
using OVR.OpenVR;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject ball;
    private ParticleSystem particle;

    [SerializeField]
    private float moveSpeed;
    private Rigidbody rigid;

    [SerializeField]
    private float cameraRotateLimit;
    private float curCameraRotateX;

    [SerializeField]
    private float lookSen;

    [SerializeField]
    private Camera cam;

    [SerializeField] private int penSize = 35;

    private Color[] _color;
    private Whiteboard _whiteboard;
    [SerializeField] Material[] mats;
    private Vector3 shootPos, shootDir;
    private RaycastHit _touch;
    private Vector2 _touchPos;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        _color = Enumerable.Repeat(mats[0].color, penSize * penSize).ToArray();
        particle = ball.GetComponent<ParticleSystem>();

    }
    private void FixedUpdate()
    {
        Walk();
        CameraRotate();
        PlayerRotate();

        if (Input.GetMouseButtonDown(0))
        {
            shootPos = rigid.transform.position;
            shootDir = Camera.main.transform.forward;
            particle.Play();
        }

        ball.transform.rotation = Camera.main.transform.rotation;
    }

    private void Walk()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveX = transform.right * moveDirX;
        Vector3 moveZ = transform.forward * moveDirZ;

        Vector3 velocity = (moveX + moveZ).normalized * moveSpeed;

        rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void CameraRotate()
    {
        float rotateX = Input.GetAxisRaw("Mouse Y");
        float cameraRotateX = rotateX * lookSen;
        curCameraRotateX -= cameraRotateX;
        curCameraRotateX = Mathf.Clamp(curCameraRotateX, -cameraRotateLimit, cameraRotateLimit);

        cam.transform.localEulerAngles = new Vector3(curCameraRotateX, 0, 0);
    }

    private void PlayerRotate()
    {
        float rotateY = Input.GetAxisRaw("Mouse X");
        Vector3 playerRotateY = new Vector3(0, rotateY, 0) * lookSen;
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(playerRotateY));
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
}
