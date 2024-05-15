using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllerAxis2DInputsReceiver
{
    public void ReceiveInput(Vector2 value, OVRInput.Axis2D axis2D);
}
