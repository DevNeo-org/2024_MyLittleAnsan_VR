using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] hitboxPrefab;
    [SerializeField] Transform[] points;
    public Transform[] finalPoints;
    private float beat = 2f;
    private float timer = 0;
    private int started = 0;
    private VehicleManager vehicleManager;
    private GameObject timerText;
    public bool leftWrenchOn = false;
    public bool rightWrenchOn = false;
    void Start()
    {
        vehicleManager = FindAnyObjectByType<VehicleManager>();
        timerText = GameObject.Find("Timer");
    }

    void Update()
    {
        if (!leftWrenchOn || !rightWrenchOn) { return; }
        timerText.GetComponent<Timer>().StartGame();
        if (timer > beat)
        {
            int num = Random.Range(0, 4);
            GameObject hitbox = Instantiate(hitboxPrefab[0], points[num]);
            hitbox.transform.localPosition = Vector3.zero;
            hitbox.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
            hitbox.GetComponent<HitboxMovement>().SetFinalPoint(num);
            hitbox.transform.GetChild(0).GetComponent<VehicleHitbox>().SetVehicleManager(vehicleManager);
            PlaySound();
            timer -= beat;
        }
        timer += Time.deltaTime;
    }
    public void PickUp(bool isLeft)
    {
        if (isLeft){ leftWrenchOn = true; }
        else { rightWrenchOn = true; }
    }
    private void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
