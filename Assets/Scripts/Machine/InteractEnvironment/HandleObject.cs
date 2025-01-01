using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandleObject : MonoBehaviour
{
    [SerializeField]
    private Transform ConnectedObj;
    [SerializeField]
    private GameObject player;

    private PlayerInput playerInput;
    public bool canInteract = false;
    public bool isHold = false;
    private void Start()
    {
        player = GameObject.Find("Player");
        ConnectedObj = transform.parent.transform;
        playerInput = new PlayerInput();
        playerInput.Disable();
        playerInput.Player.Inetract.performed += ctx =>
        {
            HandleInterct();
        };
    }
    private void Update()
    {
        if (canInteract)
        {
            playerInput.Enable();
        }
        else
        {
            playerInput.Disable();
        }
    }
    private void HandleInterct()
    {
        isHold = !isHold;
        player.GetComponent<PlayerWASDMovement>().canTurn = !isHold;
        if (isHold)
        {
            Rigidbody rigidbody = player.GetComponent<Rigidbody>();
            rigidbody.constraints |= RigidbodyConstraints.FreezePositionZ;
        }else
        {
            Rigidbody rigidbody = player.GetComponent<Rigidbody>();
            rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        }
    }

}
