using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class ControllerInputsHandler : MonoBehaviour
{
    [SerializeField] private ControllerRef controllerRef;
    [SerializeField] private GrabInteractor grabInteractor;
    [SerializeField] private OVRControllerHelper controllerHelper;
    [SerializeField] private GameObject wrench;
    [SerializeField] private bool isLeft;
    private GrabInteractable grabInteractable;
    private HitboxSpawner hitboxSpawner;
    
    private void Start()
    {
        hitboxSpawner = FindAnyObjectByType<HitboxSpawner>();
    }
    private void Update()
    {
        if (!grabInteractor.HasSelectedInteractable)
        {
            grabInteractable = null;
            return;
        }
        if (grabInteractable == null)
        {
            if (Time.timeScale == 0) { return; }
            wrench.gameObject.SetActive(true);
            hitboxSpawner.PickUp(isLeft);
            controllerHelper.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
            grabInteractable = grabInteractor.SelectedInteractable;
            Destroy(grabInteractor.transform.parent.gameObject);
            Destroy(grabInteractable.gameObject);
            Destroy(gameObject);
        }
    }
}
