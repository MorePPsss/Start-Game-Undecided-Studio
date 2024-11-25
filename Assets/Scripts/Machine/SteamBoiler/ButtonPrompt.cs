using UnityEngine;
using UnityEngine.UI;

public class ButtonPrompt : MonoBehaviour
{
    public GameObject promptUI;  
    public KeyCode interactKey = KeyCode.E; 
    private bool isPlayerNearby = false;
    //public PanelRise panelUD;
    //public PanelLR panelLR;
    public PanelR panelR;
    public PanelL panelL;


    private void Start()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);  
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (promptUI != null)
            {
                promptUI.SetActive(true);  
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (promptUI != null)
            {
                promptUI.SetActive(false);  
            }
        }
    }

    private void Update()
    {

        if (isPlayerNearby && Input.GetKeyDown(interactKey))
        {
            /*if (panelUD != null)
            {
                panelUD.StartRising();
            }
            if (panelLR != null)
            {
                panelLR.StartMoving();
            }*/
            if(panelR != null)
            {
                panelR.StartMoving();
            }
            if (panelL != null)
            {
                panelL.StartMoving();
            }
            Interact();
        }
    }

    private void Interact()
    {
        Debug.Log("added coal");
 
    }
}
