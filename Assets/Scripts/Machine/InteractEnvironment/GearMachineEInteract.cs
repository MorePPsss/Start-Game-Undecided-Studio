using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GearMachineEInteract : MonoBehaviour
{
    public bool canInteract;
    public Camera mainCamera;
    public GameObject gearMachine;
    public GameObject gearMachineArea;
    public GameObject gearMachineUI;

    private PlayerInput playerInput;
    private void Start()
    {
        canInteract = false;
        playerInput = new PlayerInput();
        playerInput.Disable();
        playerInput.Player.Inetract.performed += ctx =>
        {
            OpenDoor();
        };
    }
    private void Update()
    {
        if (canInteract)
        {
            playerInput.Enable();
        }
    }
    private void OpenDoor()
    {
        if (canInteract)
        {
            playerInput.Disable();
            gearMachine.SetActive(true);
            gearMachineArea.SetActive(false);
            mainCamera.transform.position = new Vector3(-41.54f, 5.8f, -22.05f);
            canInteract = false;
            mainCamera.GetComponent<CameraFlatMove>().enabled = false;
            mainCamera.fieldOfView = 38;
            gearMachineUI.SetActive(true);
        }
    }
}
