using Oculus.Interaction;
using Oculus.Interaction.Editor.BuildingBlocks;
using Oculus.Interaction.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;
using Handedness = Oculus.Interaction.Input.Handedness;

public class GunInputHandler : MonoBehaviour
{

    private OVRInput.Button IndexTriggerAsButton => _controllerRef.Handedness == Handedness.Left ? OVRInput.Button.PrimaryIndexTrigger : OVRInput.Button.SecondaryIndexTrigger;
    private OVRInput.Axis1D IndexTriggerAsAxis1D => _controllerRef.Handedness == Handedness.Left ? OVRInput.Axis1D.PrimaryIndexTrigger : OVRInput.Axis1D.SecondaryIndexTrigger;
    private OVRInput.Axis2D IndexTriggerAsAxis2D => _controllerRef.Handedness == Handedness.Left ? OVRInput.Axis2D.PrimaryThumbstick : OVRInput.Axis2D.PrimaryThumbstick;
    
    [SerializeField] private ControllerRef _controllerRef;
    [SerializeField] private GrabInteractor _grabInteractor;

    private GrabInteractable _grabInteractable;

    private IControllerButtonInputsReceiver _controllerButtonInputsReceiver ;
    private IControllerAxis1DInputsReceiver _controllerAxis1DInputsReceiver;
    private IControllerAxis2DInputsReceiver _controllerAxis2DInputsReceiver;

    private void Update()
    {
        if (!_grabInteractor.HasSelectedInteractable)
        {
            _grabInteractable = null;
            _controllerButtonInputsReceiver = null;
            _controllerAxis1DInputsReceiver = null;
            _controllerAxis2DInputsReceiver = null;
            return;
        }

        if (_grabInteractable == null)
        {
            _grabInteractable = _grabInteractor.SelectedInteractable;
        }

        ReadInput();
    }

    private void ReadInput()
    {
        HandleButton(OVRInput.Get(IndexTriggerAsButton), IndexTriggerAsButton);

        HandleAxis1D(OVRInput.Get(IndexTriggerAsAxis1D), IndexTriggerAsAxis1D);

        HandleAxis2D(OVRInput.Get(IndexTriggerAsAxis2D), IndexTriggerAsAxis2D);
    }

    private void HandleButton(bool isPressed, OVRInput.Button button)
    {
        if (_controllerButtonInputsReceiver == null)
        {
            if (_grabInteractable.TryGetComponent(out IControllerButtonInputsReceiver buttonInputsReceiver))
            {
                _controllerButtonInputsReceiver = buttonInputsReceiver;
            }
        }

        if (_controllerButtonInputsReceiver != null)
        {
            _controllerButtonInputsReceiver.ReceiveInput(isPressed, button);
        }
    }

    private void HandleAxis1D(float value, OVRInput.Axis1D axis1D)
    {
        if (_controllerAxis1DInputsReceiver == null)
        {
            if (_grabInteractable.TryGetComponent(out IControllerAxis1DInputsReceiver axis1DInputsReceiver))
            {
                _controllerAxis1DInputsReceiver = axis1DInputsReceiver;
            }
        }

        if (_controllerAxis1DInputsReceiver != null)
        {
            _controllerAxis1DInputsReceiver.ReceiveInput(value, axis1D);
        }
    }

    private void HandleAxis2D(Vector2 value, OVRInput.Axis2D axis2D)
    {
        if (_controllerAxis2DInputsReceiver == null)
        {
            if (_grabInteractable.TryGetComponent(out IControllerAxis2DInputsReceiver axis2DInputsReceiver))
            {
                _controllerAxis2DInputsReceiver = axis2DInputsReceiver;
            }
        }

        if (_controllerAxis2DInputsReceiver != null)
        {
            _controllerAxis2DInputsReceiver.ReceiveInput(value, axis2D);
        }
    }
}
