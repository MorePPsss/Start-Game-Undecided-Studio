using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public InputActionAsset controls;
    public InputAction pauseAction;
    public InputAction stageReloadAction;
    public InputAction cameraSwitchAction;
    public InputAction putBait;
    public delegate void InputSpaceEventHandler(float space);
    private InputSpaceEventHandler inputSpaceEventHandler;
    public event InputSpaceEventHandler OnInputSpace
    {
        add{
            inputSpaceEventHandler += value;
        }
        remove { 
            inputSpaceEventHandler -= value;
        }
    }
    public event Action<float, float> OnInputHorizontalOrVertical;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (inputSpaceEventHandler != null)
        {
            inputSpaceEventHandler(Input.GetAxisRaw("Space"));
        }

        if (OnInputHorizontalOrVertical != null)
        {
            OnInputHorizontalOrVertical(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }
    void Start()
    {
        pauseAction = controls.FindAction("Pause");
        stageReloadAction = controls.FindAction("Stage Reload");
        cameraSwitchAction = controls.FindAction("Camera Switch");
        putBait = controls.FindAction("PutBait");
        pauseAction.Enable();
        stageReloadAction.Enable();
        cameraSwitchAction.Enable();
        putBait.Enable();
    }
}
