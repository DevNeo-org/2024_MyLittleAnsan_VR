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
        //Grab���� �ƴ� ���
        if (!grabInteractor.HasSelectedInteractable)
        {
            grabInteractable = null;
            return;
        }
        //Grab���� ���
        if (grabInteractor.HasSelectedInteractable) 
        {
            grabInteractable = grabInteractor.SelectedInteractable;
        }

        if (grabInteractable != null)
        {
            isGrabbing = true;
        }

        isOnArea = gameManager.GetComponent<Lobby>().GetIsOnArea();

        //�ǹ� ������Ʈ�� ��� �ְ�, ���� ���� ������ 
        if (isGrabbing && isOnArea)
        {
            //��ƽ ���
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
