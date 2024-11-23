using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : InteractableObject
{
    public ItemSO itemSO;
    protected override void Interact()
    {
        Debug.Log("Player Pick Item£¡");
        if (this.gameObject.CompareTag(Tag.BAITITEM))
        {
            baitNum += 1;
            Debug.Log(baitNum);
        }
        Destroy(this.gameObject);
        InventoryManager.Instance.InventoryData.AddItem(itemSO);
        InventoryManager.Instance.InventoryUI.RefreshUI();
    }
}
