using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : InteractableObject
{
    public ItemSO itemSO;
    protected override void Interact()
    {
        Debug.Log("Player Pick Item��");
        PickBait();
        InventoryManager.Instance.InventoryData.AddItem(itemSO,itemSO.itemAmount);
        Destroy(this.gameObject);
        InventoryManager.Instance.InventoryUI.RefreshUI();
    }

    public void PickBait()
    {
        if (this.gameObject.CompareTag(Tag.BAITITEM))
        {
            if (GameManager.ifFirstTimeGetBait)
            {
                GameManager.ifFirstTimeGetBait = false;
                UIManager.Instance.ShowTipUI("Press the x key and put down the 'bait' in front of you");
            }
            baitNum += 1;
            Debug.Log(baitNum);
        }
    }
}
