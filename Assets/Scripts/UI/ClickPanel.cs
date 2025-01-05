using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
//pure text introduce changing
public class UIPanelClick : MonoBehaviour
{
    public float waitTime;
    public int textCount;
    public GameObject gearMachineArea;
    public GameObject trainIntroArea;
    public PlayerWASDMovement playerControl;

    private int textIndex;
    private float timeCount;
    private PlayerInput playerInput;
    private void Start()
    {
        playerControl.enabled = false;
        textIndex = 0;
        playerInput = new PlayerInput();
        playerInput.Enable();
        textCount = transform.childCount;
        playerInput.UI.Click.performed += ctx =>
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                NextText();
            }
        };
    }
    private void Update()
    {
        if(timeCount < waitTime)
        {
            timeCount += Time.deltaTime;
        }
    }
    private void NextText()
    {
        if(timeCount >= waitTime)
        {
            transform.GetChild(textIndex).gameObject.SetActive(false);
            timeCount = 0;
            textIndex++;
            if(textIndex >= textCount)
            {
                gearMachineArea.SetActive(true);
                trainIntroArea.SetActive(false);
                this.transform.parent.gameObject.SetActive(false);
                playerControl.enabled = true;
                playerInput.Disable();
            }
            else
            {
                transform.GetChild(textIndex).gameObject.SetActive(true);
            }
        }
    }
}