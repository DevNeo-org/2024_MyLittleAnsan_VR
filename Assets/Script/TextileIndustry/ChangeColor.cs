using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private Gun gun;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BlueBucket"))
        {
            gun.ChangeColor(0);
        }
        else if (collision.collider.CompareTag("RedBucket"))
        {
            gun.ChangeColor(1);
        }
        else if (collision.collider.CompareTag("YellowBucket"))
        {
            gun.ChangeColor(2);
        }
        else if (collision.collider.CompareTag("BlackBucket"))
        {
            gun.ChangeColor(3);
        }
    }
}
