using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : InteractableObject
{
    public ItemSO itemSO;
    protected override void Interact()
    {
        Debug.Log("玩家捡起物品！");
        Destroy(this.gameObject);
        //TODO 放入背包数据结构
        InventoryManager.Instance.InventoryData.AddItem(itemSO);
        InventoryManager.Instance.InventoryUI.RefreshUI();
    }
}
