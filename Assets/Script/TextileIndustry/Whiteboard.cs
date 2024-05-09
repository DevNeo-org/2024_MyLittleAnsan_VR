using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public ParticleSystem Particulas;
    public Player player;
    public GameObject board;

    public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    ParticleSystem _particleSystem;

    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048,2048);
    // Start is called before the first frame update
    void Start()
    {
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log(other);
        // ParticleSystem part = other.GetComponent<ParticleSystem>(); // *** important! Making a variable to acess the particle system of the emmiting object, in this case, the lasers from my player ship.
        int numCollisionEvents = Particulas.GetCollisionEvents(this.gameObject, collisionEvents);

        foreach (ParticleCollisionEvent collisionEvent in collisionEvents) // for each collision, do the following:
        {
            Vector3 pos = collisionEvent.intersection;
            player.Draw(pos, board);
        }

    }
}
