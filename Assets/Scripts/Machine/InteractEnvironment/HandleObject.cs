using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandleObject : MonoBehaviour
{
    public bool canInteract = false;

    [SerializeField]
    private Transform ConnectedObj;
    [SerializeField]
    private GameObject player;
    private PlayerInput playerInput;
    private bool isHold = false;
    private Vector3 distance;
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
            if(isHold)
            {
                ConnectedObj.position += new Vector3(player.GetComponent<PlayerWASDMovement>().moveDir.x, 0, 0) * Time.deltaTime;
            }
        }
        else
        {
            playerInput.Disable();
        }
    }
    public void HandleInterct()
    {
        isHold = !isHold;
        Rigidbody rigidbody = player.GetComponent<Rigidbody>();
        player.GetComponent<PlayerWASDMovement>().canTurn = !isHold;
        GetComponent<MeshCollider>().enabled = !isHold;
        if (isHold)
        {
            player.GetComponent<PlayerWASDMovement>().moveSpeed = 1;
            rigidbody.constraints |= RigidbodyConstraints.FreezePositionZ;
            distance = ConnectedObj.position - rigidbody.position;
        }else
        {
            player.GetComponent<PlayerWASDMovement>().moveSpeed = 2;
            rigidbody.constraints &= ~RigidbodyConstraints.FreezePositionZ;
        }
    }

}
