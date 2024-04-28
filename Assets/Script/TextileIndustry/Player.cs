using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using UnityEditor;

public class Player : MonoBehaviour
{
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

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Walk();
        CameraRotate();
        PlayerRotate();
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
}
