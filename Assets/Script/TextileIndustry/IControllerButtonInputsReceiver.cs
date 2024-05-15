using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllerButtonInputsReceiver
{
    public void ReceiveInput(bool isPressed, OVRInput.Button button);
}
