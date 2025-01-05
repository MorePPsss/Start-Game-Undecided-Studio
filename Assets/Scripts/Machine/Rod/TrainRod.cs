using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TrainRod : MonoBehaviour
{
    public bool powered;
    public Camera mainCamera;
    public LayerMask layerMask;
    public float rodAreaRadius;
    public Vector3 rotateAxis;
    public bool canInteractive;
    public float atanNum;
    public Train train;

    private Vector2 mouseStartPos;
    private Vector3 targetPos;
    private PlayerInput playerInput;
    private bool hold;
    private bool finish;
    private void Start()
    {
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
                if (hit.collider.gameObject.name == "ClickArea")
                {
                    mouseStartPos = mousePosition;
                    hold = true;
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
        float rotateAngle = 0;
        if (hold && !finish)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            if (Mathf.Abs(mousePosition.x - mouseStartPos.x) > rodAreaRadius)
            {
                hold = false;
            }
            rotateAngle = Mathf.Min((mousePosition.y - mouseStartPos.y) / 2 , 0) - 78;
            rotateAngle = Mathf.Max(rotateAngle, -240);
            Quaternion targetRotation = Quaternion.Euler(rotateAxis * rotateAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1f);
        }
        if (transform.rotation.eulerAngles.magnitude < 260 && powered)
        {
            Debug.Log(transform.rotation.eulerAngles.magnitude);
            Quaternion targetRotation = Quaternion.Euler(-270, 0 ,0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 4f);
            finish = true;
            if(transform.rotation.eulerAngles.magnitude > 259)
            {
                mainCamera.fieldOfView = 22;
                mainCamera.GetComponent<CameraFlatMove>().enabled = true;
                mainCamera.transform.position = new Vector3(-2.98f, 5.8f, -22f);
                train.targetPos = new Vector3(-6.37f, 0, 0);
                this.enabled = false;
            }
        }else if (!hold && !finish)
        {
            Quaternion targetRotation = Quaternion.Euler(-78, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
        }
    }
}
