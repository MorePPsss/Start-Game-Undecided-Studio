using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInteracitve : MonoBehaviour
{
    public bool interactable;
    private PlayerInput playerInput;
    private void Start()
    {
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.UI.Click.performed += ctx =>
        {
        };
        playerInput.UI.Release.performed += ctx =>
        {
            //Debug.Log("Release" + UnityEngine.InputSystem.Mouse.current.position.ReadValue());
        };
    }

}
