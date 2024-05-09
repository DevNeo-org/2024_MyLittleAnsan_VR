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

    public void Draw(Vector3 pos, GameObject board)
    {
        if (_whiteboard == null)
        {
            _whiteboard = board.GetComponent<Whiteboard>();
        }

        var x = (int)(pos.x * _whiteboard.textureSize.x - (penSize / 2));
        var y = (int)(pos.y * _whiteboard.textureSize.y - (penSize / 2));

        if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x)
        {
            return;
        }
        _whiteboard.texture.SetPixels(x, y, penSize, penSize, _color);

        _whiteboard.texture.Apply();
        _whiteboard = null;
    }

}
