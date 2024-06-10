using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class GrabCheck : MonoBehaviour
{
    [SerializeField] private OVRInput.Controller controller;
    [SerializeField] private ControllerRef controllerRef;
    [SerializeField] private GrabInteractor grabInteractor;
    [SerializeField] private OVRControllerHelper controllerHelper;
    [SerializeField] private bool isLeft;
    private GrabInteractable grabInteractable;

    GameObject gameManager;
    bool isGrabbing;
    bool isOnArea;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    private void Update()
    {
        //Grab중이 아닌 경우
        if (!grabInteractor.HasSelectedInteractable)
        {
            grabInteractable = null;
            return;
        }
        //Grab했을 경우
        if (grabInteractor.HasSelectedInteractable) 
        {
            grabInteractable = grabInteractor.SelectedInteractable;
        }

        if (grabInteractable != null)
        {
            isGrabbing = true;
        }

        isOnArea = gameManager.GetComponent<Lobby>().GetIsOnArea();

        //건물 오브젝트를 잡고 있고, 구역 위에 있으면 
        if (isGrabbing && isOnArea)
        {
            //햅틱 재생
            StartCoroutine(StartTriggerHaptics());
        }
    }

    IEnumerator StartTriggerHaptics()
    {
        OVRInput.SetControllerVibration(2.5f, 2.5f, controller);
        yield return new WaitForSeconds(0.3f);
        OVRInput.SetControllerVibration(0f, 0f, controller);
    }
}
