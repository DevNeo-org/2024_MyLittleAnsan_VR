using OVR.OpenVR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class WhiteboradMarker : MonoBehaviour
{
    [SerializeField] private Transform tip;
    [SerializeField] private int penSize = 5;

    private Renderer _renderer;
    private Color[] _color;
    private float tipHeight;

    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private quaternion _lastTouchRot;

    void Start()
    {
        _renderer = tip.GetComponent<Renderer>();
        _color = Enumerable.Repeat(_renderer.material.color, penSize * penSize).ToArray();
        tipHeight = tip.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(tip.position, transform.up, new Color(0, 1, 0));
        Draw();
    }

    private void Draw()
    {
        if (Physics.Raycast(tip.position, transform.up, out _touch, tipHeight))
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

                if (_touchedLastFrame)
                {
                    _whiteboard.texture.SetPixels(x, y, penSize, penSize, _color);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, penSize, penSize, _color);
                    }

                    transform.rotation = _lastTouchRot;

                    _whiteboard.texture.Apply();
                }

                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;
            }
        }

        _whiteboard = null;
        _touchedLastFrame = false;
    }
}


