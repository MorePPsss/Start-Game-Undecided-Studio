using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public InputActionAsset controls;
    public InputAction pauseAction;
    public InputAction stageReloadAction;
    public InputAction cameraSwitchAction;
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

    void Start()
    {
        pauseAction = controls.FindAction("Pause");
        stageReloadAction = controls.FindAction("Stage Reload");
        cameraSwitchAction = controls.FindAction("Camera Switch");
    }
}
