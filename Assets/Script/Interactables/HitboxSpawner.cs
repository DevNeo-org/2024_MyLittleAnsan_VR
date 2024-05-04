using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] hitboxPrefab;
    [SerializeField] Transform[] points;
    public Transform[] finalPoints;
    private float beat = 3f;
    private float timer;
    void Start()
    {
        
    }

    void Update()
    {
        if (timer > beat)
        {
            GameObject hitbox = Instantiate(hitboxPrefab[0], points[Random.Range(0,3)]);
            hitbox.transform.localPosition = Vector3.zero;
            hitbox.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
            timer -= beat;
        }
        timer += Time.deltaTime;
    }
}