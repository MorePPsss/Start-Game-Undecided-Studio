using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rod : MonoBehaviour
{
    public bool interactable;
    public bool powered;
    public Camera mainCamera;
    public LayerMask layerMask;
    public Canvas canvas;
    public GameObject eventArea;
    public GameObject door;

    private Vector2 mouseStartPos;
    private Vector3 targetPos;
    private PlayerInput playerInput;
    private bool hold;
    private bool finish;
    private void Start()
    {
        targetPos = door.transform.position;
        targetPos.y = 8.27f;
        hold = false;
        finish = false;
        powered = false;
        playerInput = new PlayerInput();
        playerInput.Enable();
        playerInput.UI.Click.performed += ctx =>
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            Ray ray = mainCamera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.gameObject.name == "ClickArea" && interactable)
                {
                    mouseStartPos = mousePosition;
                    hold = true;
                    eventArea.SetActive(false);
                    canvas.gameObject.SetActive(false);
                }
            }
        };
        playerInput.UI.Release.performed += ctx =>
        {
            hold = false;
        };
    }
    private void Update()
    {
        if(hold && interactable)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            if(Mathf.Abs(mousePosition.y - mouseStartPos.y) > 80)
            {
                hold = false;
            }
            float rotateAngle = - Mathf.Atan2((mousePosition.x - mouseStartPos.x) , 70) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, rotateAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 4f);
        }
        if(transform.rotation.eulerAngles.z < 300 && transform.rotation.eulerAngles.z > 280 )
        {
            if (hold)
            {
                playerInput.Disable();
            }
            Quaternion targetRotation = Quaternion.Euler(0, 0, -80f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 4f);
            if (transform.rotation.eulerAngles.z <= 280.5) { finish = true; hold = false; }
        }
        else if(!hold)
        {
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
        }
        if (finish)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, targetPos, Mathf.SmoothStep(0f, 1f, Time.deltaTime * 5)) ;
        }
    }
}
