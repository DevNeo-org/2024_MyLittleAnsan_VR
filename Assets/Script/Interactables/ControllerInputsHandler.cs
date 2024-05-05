using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class ControllerInputsHandler : MonoBehaviour
{
    private OVRInput.Button IndexTriggerAsButton => _controllerRef.Handedness == Handedness.Left ? OVRInput.Button.PrimaryIndexTrigger : OVRInput.Button.SecondaryIndexTrigger;

    [SerializeField] private ControllerRef _controllerRef;
    [SerializeField] private GrabInteractor _grabInteractor;
    [SerializeField] private OVRControllerHelper _controllerHelper;
    [SerializeField] private GameObject wrench;
    private GrabInteractable _grabInteractable;
    private HitboxSpawner _hitboxSpawner;
    
    private void Start()
    {
        _hitboxSpawner = FindAnyObjectByType<HitboxSpawner>();
    }
    private void Update()
    {
        if (!_grabInteractor.HasSelectedInteractable)
        {
            _grabInteractable = null;
            return;
        }
        if (_grabInteractable == null)
        {
            wrench.gameObject.SetActive(true);
            _hitboxSpawner.PickUp();
            _controllerHelper.m_showState = OVRInput.InputDeviceShowState.ControllerNotInHand;
            _grabInteractable = _grabInteractor.SelectedInteractable;
            Destroy(_grabInteractor.gameObject);
            Destroy(_grabInteractable.gameObject);
            Destroy(gameObject);
        }
        //ReadInputs();
    }
    private void ReadInputs()
    {
        HandleButton(OVRInput.Get(IndexTriggerAsButton), IndexTriggerAsButton);

    }
    private void HandleButton(bool isPressed, OVRInput.Button button)
    {

    }
}
