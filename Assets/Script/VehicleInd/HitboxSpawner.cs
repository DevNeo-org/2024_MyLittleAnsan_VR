using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] hitboxPrefab;
    [SerializeField] Transform[] points;
    public Transform[] finalPoints;
    private float beat = 3f;
    private float timer = 0;
    private int started = 2;
    void Start()
    {
    }

    void Update()
    {
        if (started < 2) { return; }
        if (timer > beat)
        {
            int num = Random.Range(0, 4);
            GameObject hitbox = Instantiate(hitboxPrefab[0], points[num]);
            hitbox.transform.localPosition = Vector3.zero;
            hitbox.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
            hitbox.GetComponent<HitboxMovement>().SetFinalPoint(num);
            timer -= beat;
            Debug.Log("spawn" + timer);
        }
        timer += Time.deltaTime;
    }
    public void PickUp()
    {
        started++;
    }
}
