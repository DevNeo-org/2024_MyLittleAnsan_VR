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

    void Awake()
    {
        particleManager = particleManage.GetComponent<ParticleManager>();
        _color = Enumerable.Repeat(colorMaterial[colorCode].color, penSize * penSize).ToArray();
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
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

                if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x)
                {
                    return;
                }

                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -2; j <= 2; j++)
                    {
                        _whiteboard.texture.SetPixels(x + i * (penSize), y + j * (penSize), penSize, penSize, _color);
                    }
                }
                _whiteboard.texture.SetPixels(x + (penSize) / 2, y + 3 * (penSize), penSize, penSize, _color);
                _whiteboard.texture.SetPixels(x - (penSize) / 2, y + 3 * (penSize), penSize, penSize, _color);
                _whiteboard.texture.SetPixels(x + (penSize) / 2, y - 3 * (penSize), penSize, penSize, _color);
                _whiteboard.texture.SetPixels(x - (penSize) / 2, y - 3 * (penSize), penSize, penSize, _color);

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
