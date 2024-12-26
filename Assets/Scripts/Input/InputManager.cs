using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public InputActionAsset controls; // Input action asset for all input configurations
    public InputAction pauseAction; // Action for pausing the game
    public InputAction stageReloadAction; // Action for reloading the stage
    public InputAction cameraSwitchAction; // Action for switching the camera
    public InputAction putBait; // Action for putting bait
    public InputAction toggleSpeedAction; // Action for toggling the plane speed

    public delegate void InputSpaceEventHandler(float space);
    private InputSpaceEventHandler inputSpaceEventHandler;

    // Event for space input (e.g., thrust)
    public event InputSpaceEventHandler OnInputSpace
    {
        add { inputSpaceEventHandler += value; }
        remove { inputSpaceEventHandler -= value; }
    }

    // Event for horizontal and vertical movement input
    public event Action<float, float> OnInputHorizontalOrVertical;

    // Event for toggling speed when F key is pressed
    public event Action OnToggleSpeed;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
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
        // Trigger space input event if subscribed
        if (inputSpaceEventHandler != null)
        {
            inputSpaceEventHandler(Input.GetAxisRaw("Space"));
        }

        // Trigger horizontal/vertical input event if subscribed
        if (OnInputHorizontalOrVertical != null)
        {
            OnInputHorizontalOrVertical(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
    }

    void Start()
    {
        // Find and enable actions from the InputActionAsset
        pauseAction = controls.FindAction("Pause");
        stageReloadAction = controls.FindAction("Stage Reload");
        cameraSwitchAction = controls.FindAction("Camera Switch");
        putBait = controls.FindAction("PutBait");
        toggleSpeedAction = controls.FindAction("Toggle Speed"); // Find action for toggling speed

        pauseAction.Enable();
        stageReloadAction.Enable();
        cameraSwitchAction.Enable();
        putBait.Enable();
        toggleSpeedAction.Enable();

        // Register callback for toggling speed
        toggleSpeedAction.performed += context => OnToggleSpeed?.Invoke();
    }
}
