using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingGrapped : MonoBehaviour
{
    public OVRInput.Controller controller;
    GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter(Collider Collider)
    {
        //ÇÝÆ½ Àç»ý
        StartCoroutine(TriggerHaptics());
        //¶¥°ú Ãæµ¹ÇÒ ¶§ È¿°úÀ½ Àç»ý
        //if (Collider.gameObject.name == "Table")
        //    gameManager.GetComponent<AudioManager>().PlaySound(3);
    }

    IEnumerator TriggerHaptics()
    {
        OVRInput.SetControllerVibration(0.3f, 0.3f, controller);
        yield return new WaitForSeconds(0.1f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }

}
