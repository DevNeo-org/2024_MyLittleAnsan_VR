using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllerAxis1DInputsReceiver
{
    public void ReceiveInput(float value, OVRInput.Axis1D axis1D);
}
