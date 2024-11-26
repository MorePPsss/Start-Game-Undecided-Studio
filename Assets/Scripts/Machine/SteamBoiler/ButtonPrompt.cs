using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPrompt : MonoBehaviour
{
    public ItemDBSO Bag;
    public GameObject promptUI;  
    public KeyCode interactKey = KeyCode.E; 
    private bool isPlayerNearby = false;
    public PanelR panelR;
    public PanelL panelL;

    public ItemSO requiredItem;

    private void Start()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);  
        }
        InitializeItemAmount();
    }
    private void InitializeItemAmount()
    {
        InventoryItem bagItem = Bag.itemList.Find(item => item.itemData == requiredItem);
        if (bagItem != null)
        {
            bagItem.amount = 0; 
        }
        else
        {
            InventoryItem newItem = new InventoryItem { itemData = requiredItem, amount = 0 };
            Bag.itemList.Add(newItem);
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
            if (CanInteractWithItem(requiredItem)) 
            {
                if (panelR != null)
                {
                    panelR.StartMoving();
                }
                if (panelL != null)
                {
                    panelL.StartMoving();
                }
                Interact(requiredItem);
            }
            else
            {
                Debug.Log("Not enough items to move the panel!");
            }
        }
    }
    private bool CanInteractWithItem(ItemSO itemData)
    {
        foreach (var item in Bag.itemList)
        {
            if (item.itemData == itemData && item.amount > 0)
            {
                return true;
            }
        }
        return false;
    }

    private void Interact(ItemSO itemData)
    {
        foreach (var item in Bag.itemList)
        {
            if (item.itemData == itemData && item.amount > 0)
            {
                item.amount -= 1;
                Debug.Log($"Used 1 {itemData.name}. Remaining: {item.amount}");
                break;
            }
        }
    }
}
