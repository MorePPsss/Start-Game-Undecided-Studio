using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class GearMachineEInteract : MonoBehaviour
{
    public bool canInteract;
    public Camera mainCamera;
    public GameObject gearMachine;
    public GameObject gearMachineArea;
    public GameObject gearMachineUI;
    public GameObject Door3DUI;

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
            //success opening door
            playerInput.Disable();
            //gearMachine effects
            gearMachine.SetActive(true);
            mainCamera.transform.position = new Vector3(-41.54f, 5.8f, -22.05f);
            mainCamera.fieldOfView = 38;
            gearMachineUI.SetActive(true);
            //disable useless functions
            gearMachineArea.SetActive(false);
            canInteract = false;
            mainCamera.GetComponent<CameraFlatMove>().enabled = false;
            //disable E to open Door UI
            Door3DUI.transform.GetChild(0).gameObject.SetActive(false);
            Door3DUI.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
