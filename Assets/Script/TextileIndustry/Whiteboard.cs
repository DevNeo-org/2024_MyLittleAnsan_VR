using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    // Texture ����
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(512,512);
    
    void Start()
    {
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        // �� texture �� �� ����
        r.material.mainTexture = texture;
        r.material.color = new Color (255/(float)255, 255 / (float)255, 255 / (float)255, 60 / (float)255);
    }
}
