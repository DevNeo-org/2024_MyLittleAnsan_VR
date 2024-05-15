using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerButtonInputReceiver : MonoBehaviour, IControllerButtonInputsReceiver
{
    public event Action<bool, OVRInput.Button> ButtonInputReceived;

    public void ReceiveInput(bool isPressed, OVRInput.Button button)
    {
        ButtonInputReceived?.Invoke(isPressed, button);
    }
}
