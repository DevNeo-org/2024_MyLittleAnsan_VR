using OVR.OpenVR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Whiteboard _whiteboard;
    private Rigidbody rigid;
    private RaycastHit _touch;
    private Vector2 _touchPos;
    [SerializeField] int penSize;
    [SerializeField] GameObject particleManage;
    private ParticleManager particleManager;
    private Color[] _color;

    public int colorCode;
    public float ballSpeed;

    [SerializeField] Gun gun;
    [SerializeField] Material[] colorMaterial;

    [SerializeField] private int paintSizeRange = 2;
    [SerializeField] private int paintSize = 2;

    void Awake()
    {
        particleManager = particleManage.GetComponent<ParticleManager>();
        _color = Enumerable.Repeat(colorMaterial[colorCode].color, penSize * penSize).ToArray();
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        if (pos.x < -14 || pos.x > 14 || pos.y < -5 || pos.y > 16 || pos.z < -3 || pos.z > 15)
        {
            Destroy(gameObject);
        }

        if (Physics.Raycast(rigid.position, rigid.transform.forward, out _touch, 1f))
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

                /*
                if (y < penSize * (paintSizeRange + 2) 
                    || y > _whiteboard.textureSize.y - (penSize) * (paintSizeRange + 2) 
                    || x < penSize * (paintSizeRange + 2)
                    || x > _whiteboard.textureSize.x - (penSize) * (paintSizeRange+2))
                {
                    return;
                }
                */

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

                _whiteboard.texture.Apply();

                particleManager.PlayParticle(colorCode, rigid.position);

                Destroy(gameObject);

                return;
            }
        }
        _whiteboard = null;

    }

    public void ChangeColorCode(int code)
    {
        colorCode = code;
    }
}
