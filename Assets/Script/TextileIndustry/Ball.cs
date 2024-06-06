using OVR.OpenVR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] int penSize;   // ��ĥ 1ĭ ũ��
    [SerializeField] GameObject particleManage; // ��ƼŬ �Ŵ���
    [SerializeField] Gun gun;   // Gun Ŭ����
    [SerializeField] Material[] colorMaterial;  // ��ĥ Material 
    [SerializeField] private int paintSizeRange = 2;    
    [SerializeField] private int paintSize = 2;

    private Whiteboard _whiteboard;
    private Rigidbody rigid;
    private RaycastHit _touch;
    private Vector2 _touchPos;
    private ParticleManager particleManager;
    private Color[] _color;

    public int colorCode;   
    public float ballSpeed; // ����Ʈ�� �ӵ�

    void Awake()
    {
        particleManager = particleManage.GetComponent<ParticleManager>();
        _color = Enumerable.Repeat(colorMaterial[colorCode].color, penSize * penSize).ToArray();    // �⺻ Color�� ����Ʈ �� ����
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;

        // �� �������� ��� �� ����Ʈ�� ����
        if (pos.x < -14 || pos.x > 14 || pos.y < -5 || pos.y > 16 || pos.z < -3 || pos.z > 15)
        {
            Destroy(gameObject);
        }

        // ����Ʈ ��ĥ
        if (Physics.Raycast(rigid.position, rigid.transform.forward, out _touch, 1f))
        {
            // �浹 ����� ������ Ȯ��
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                // ��ĥ �߽� ��ġ ����
                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (penSize / 2));

                // �� ������� ��ĥ ����
                for (int i = (-1)*paintSize; i <= paintSize; i++)
                {
                    for (int j = (-1) * paintSize; j <= paintSize; j++)
                    {
                        if (i * i + j * j <= paintSizeRange*paintSizeRange)
                        {
                            if (x + i * (penSize) > penSize
                                && x + i * (penSize) < _whiteboard.textureSize.x - penSize
                                && y + j * (penSize) > penSize
                                && y + j * (penSize) < _whiteboard.textureSize.y - penSize)
                            {
                                _whiteboard.texture.SetPixels(x + i * (penSize), y + j * (penSize), penSize, penSize, _color);
                            }
                        }
                    }
                }

                // ��ĥ ����
                _whiteboard.texture.Apply();

                // ����Ʈ Ƣ�� ��ƼŬ ���
                particleManager.PlayParticle(colorCode, rigid.position);

                Destroy(gameObject);

                return;
            }
        }
        _whiteboard = null;

    }

    // ����Ʈ �� ���� 
    public void ChangeColorCode(int code)
    {
        colorCode = code;
    }
}
