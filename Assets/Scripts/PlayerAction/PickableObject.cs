using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : InteractableObject
{
    public ItemSO itemSO;
    protected override void Interact()
    {
        Debug.Log("Player Pick Item£¡");
        Destroy(this.gameObject);
        InventoryManager.Instance.InventoryData.AddItem(itemSO);
        InventoryManager.Instance.InventoryUI.RefreshUI();
    }
}
