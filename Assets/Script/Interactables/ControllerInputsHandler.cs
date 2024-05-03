using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;
using Oculus.Interaction.Input;

public class ControllerInputsHandler : MonoBehaviour
{
    [Tooltip("The interactable to monitor for state changes.")]
    /// <summary>
    /// The interactable to monitor for state changes.
    /// </summary>
    [SerializeField, Interface(typeof(IInteractableView))]
    private UnityEngine.Object _interactableView;

    [Tooltip("The mesh that will change color based on the current state.")]
    /// <summary>
    /// The mesh that will change color based on the current state.
    /// </summary>
    [SerializeField]
    private Renderer _renderer;

    [Tooltip("Displayed when the state is normal.")]
    /// <summary>
    /// Displayed when the state is normal.
    /// </summary>
    [SerializeField]
    private Color _normalColor = Color.red;

    [Tooltip("Displayed when the state is hover.")]
    /// <summary>
    /// Displayed when the state is hover.
    /// </summary>
    [SerializeField]
    private Color _hoverColor = Color.blue;

    [Tooltip("Displayed when the state is selected.")]
    /// <summary>
    /// Displayed when the state is selected.
    /// </summary>
    [SerializeField]
    private Color _selectColor = Color.green;

    [Tooltip("Displayed when the state is disabled.")]
    /// <summary>
    /// Displayed when the state is disabled.
    /// </summary>
    [SerializeField]
    private Color _disabledColor = Color.black;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
