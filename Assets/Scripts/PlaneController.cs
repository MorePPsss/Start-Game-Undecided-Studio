using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public class PlaneController : MonoBehaviour
{
    public float moveSpeed = 0; // Initial speed of the plane
    private float thrustInput; // Input value for thrust (forward/backward movement)

    public float horizontalSpeed = 30; // Speed of horizontal rotation
    public float verticalSpeed = 15; // Speed of vertical rotation
    private Vector3 steeringInput; // Input values for steering (pitch, roll)

    public float leanAmount_X = 90; // Visual tilt amount on the X-axis
    public float leanAmount_Y = 30; // Visual tilt amount on the Y-axis

    public float steeringSmoothing = 1.5f; // Smoothing factor for steering input
    private Vector3 rawInputSteering; // Raw steering input from the player
    private Vector3 smoothInputSteering; // Smoothed steering input

    private Rigidbody rb; // Rigidbody component for physics-based movement
    public Transform model; // Visual representation of the plane
    public Text subtitleText; // UI Text for subtitles

    private bool isCutscenePlaying = true; // Flag to disable player control during cutscene
    public Image targetImage;
    public AudioClip toggleSpeedSound;
    private AudioSource audioSource;

    private void Awake()
    {
        // Initialize the Rigidbody component
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        // Subscribe to toggle speed event from InputManager
        if (InputManager.instance != null)
        {
            InputManager.instance.OnToggleSpeed += ToggleSpeed;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from toggle speed event to prevent memory leaks
        if (InputManager.instance != null)
        {
            InputManager.instance.OnToggleSpeed -= ToggleSpeed;
        }
    }

    private void OnEnable()
    {
        // Subscribe to input events from InputManager
        if (InputManager.instance != null)
        {
            InputManager.instance.OnInputHorizontalOrVertical += HandleSteeringInput;
            InputManager.instance.OnInputSpace += HandleThrustInput;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from input events
        if (InputManager.instance != null)
        {
            InputManager.instance.OnInputHorizontalOrVertical -= HandleSteeringInput;
            InputManager.instance.OnInputSpace -= HandleThrustInput;
        }
    }

    private void Start()
    {
        // Start the opening cutscene
        StartCoroutine(PlayOpeningCutscene());
    }

    private void Update()
    {
        if (!isCutscenePlaying)
        {
            // Smooth out input values for better control
            SmoothInputs();
        }
    }

    private void FixedUpdate()
    {

        // Allow movement and turning only after the cutscene
        Move();
        Turn();

    }

    private void HandleSteeringInput(float horizontal, float vertical)
    {
        // Convert horizontal and vertical inputs to a steering vector
        if (!isCutscenePlaying) // Ignore inputs during the cutscene
        {
            rawInputSteering = new Vector3(vertical, 0, -horizontal);
        }
    }

    private void HandleThrustInput(float thrust)
    {
        // Assign thrust input
        if (!isCutscenePlaying) // Ignore inputs during the cutscene
        {
            thrustInput = thrust;
        }
    }

    private void SmoothInputs()
    {
        // Smooth the steering input using Lerp
        smoothInputSteering = Vector3.Lerp(smoothInputSteering, rawInputSteering, Time.deltaTime * steeringSmoothing);
        steeringInput = smoothInputSteering;
    }

    private void Move()
    {
        // Apply forward velocity based on thrust input and move speed
        Vector3 moveDirection = transform.forward * moveSpeed;
        rb.velocity = moveDirection;
    }

    private void Turn()
    {
        // Calculate torque based on steering input
        Vector3 torque = new Vector3(steeringInput.x * horizontalSpeed, -steeringInput.z * verticalSpeed, 0);
        rb.AddRelativeTorque(torque);

        // Smoothly correct the plane's roll (Z-axis rotation)
        rb.rotation = Quaternion.Slerp(
            rb.rotation,
            Quaternion.Euler(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0)),
            Time.fixedDeltaTime * 2 // Adjust this factor for smoother roll correction
        );

        // Adjust the model's tilt for visual feedback
        AdjustModelTilt();
    }

    private void AdjustModelTilt()
    {
        // Tilt the plane model visually based on input
        model.localEulerAngles = new Vector3(
            steeringInput.x * leanAmount_Y,
            model.localEulerAngles.y,
            steeringInput.z * leanAmount_X
        );
    }

    private void ToggleSpeed()
    {
        if (!isCutscenePlaying) 
        {
            
            moveSpeed = moveSpeed == 0 ? 10 : 0;

            if (moveSpeed == 10)
            {
                
                if (toggleSpeedSound != null && !audioSource.isPlaying)
                {
                    audioSource.clip = toggleSpeedSound;
                    audioSource.loop = true; 
                    audioSource.Play();
                }
            }
            else
            {
                
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }

            Debug.Log($"Move Speed Toggled: {moveSpeed}");
        }
    }


    private IEnumerator PlayOpeningCutscene()
    {
        moveSpeed = 10; // Plane moves at speed 10
        subtitleText.text = ""; // Clear any existing subtitle
        audioSource.clip = toggleSpeedSound;
        audioSource.loop = true;
        audioSource.Play();
        // Display subtitles one by one
        yield return ShowSubtitle("Hi, Commander. It seems you have reached the target area.", 6f);
        yield return ShowSubtitle("There are four areas, get in there and decode the enemy's secrets.", 10f);
        yield return ShowSubtitle("We are all counting on you commander.", 3f);

        // End cutscene
        audioSource.Stop();
        thrustInput = 0;
        moveSpeed = 0;
        isCutscenePlaying = false;
        subtitleText.text = "";
        targetImage.gameObject.SetActive(false);
    }

    private IEnumerator ShowSubtitle(string message, float duration)
    {
        if (subtitleText != null)
        {
            // Set the text for the subtitle
            subtitleText.text = message;

            // Optional: Add fade-in and fade-out effect
            yield return FadeSubtitle(0, 1, 0.5f); // Fade-in over 0.5 seconds
            yield return new WaitForSeconds(duration); // Wait for the subtitle duration
            yield return FadeSubtitle(1, 0, 0.5f); // Fade-out over 0.5 seconds

            subtitleText.text = ""; // Clear subtitle after fade-out
        }
    }

    private IEnumerator FadeSubtitle(float startAlpha, float endAlpha, float duration)
    {
        CanvasGroup canvasGroup = subtitleText.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogWarning("CanvasGroup component missing on SubtitleText. Fading skipped.");
            yield break;
        }

        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}

