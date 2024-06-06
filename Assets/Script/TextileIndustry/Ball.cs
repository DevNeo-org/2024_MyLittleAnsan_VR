using OVR.OpenVR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] int penSize;   // 색칠 1칸 크기
    [SerializeField] GameObject particleManage; // 파티클 매니저
    [SerializeField] Gun gun;   // Gun 클래스
    [SerializeField] Material[] colorMaterial;  // 색칠 Material 
    [SerializeField] private int paintSizeRange = 2;    
    [SerializeField] private int paintSize = 2;

    private Whiteboard _whiteboard;
    private Rigidbody rigid;
    private RaycastHit _touch;
    private Vector2 _touchPos;
    private ParticleManager particleManager;
    private Color[] _color;

    public int colorCode;   
    public float ballSpeed; // 페인트볼 속도

    void Awake()
    {
        particleManager = particleManage.GetComponent<ParticleManager>();
        _color = Enumerable.Repeat(colorMaterial[colorCode].color, penSize * penSize).ToArray();    // 기본 Color로 페인트 색 설정
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 pos = transform.position;

        // 맵 일정수준 벗어날 시 페인트볼 제거
        if (pos.x < -14 || pos.x > 14 || pos.y < -5 || pos.y > 16 || pos.z < -3 || pos.z > 15)
        {
            Destroy(gameObject);
        }

        // 페인트 색칠
        if (Physics.Raycast(rigid.position, rigid.transform.forward, out _touch, 1f))
        {
            // 충돌 대상이 옷인지 확인
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }

                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                // 색칠 중심 위치 설정
                var x = (int)(_touchPos.x * _whiteboard.textureSize.x - (penSize / 2));
                var y = (int)(_touchPos.y * _whiteboard.textureSize.y - (penSize / 2));

                // 원 모양으로 색칠 설정
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

                // 색칠 적용
                _whiteboard.texture.Apply();

                // 페인트 튀는 파티클 재생
                particleManager.PlayParticle(colorCode, rigid.position);

                Destroy(gameObject);

                return;
            }
        }
        _whiteboard = null;

    }

    // 페인트 색 변경 
    public void ChangeColorCode(int code)
    {
        colorCode = code;
    }
}
