using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType { BAG,WEAPON,FEET,ACTION } //����

public class SlotHolder : MonoBehaviour
{
    public SlotType slotType;
    public ItemUI itemUI;

    public void UpdateItem() //������ʾ
    {
        switch(slotType)
        {
            case SlotType.BAG:
                itemUI.Bag = InventoryManager.Instance.InventoryData;
                break;
            case SlotType.ACTION:
                itemUI.Bag = InventoryManager.Instance.actionData;
                break;
            case SlotType.WEAPON:
                itemUI.Bag = InventoryManager.Instance.equipmentData;
                break;
            case SlotType.FEET:
                itemUI.Bag = InventoryManager.Instance.equipmentData;
                break;
        }

        var item = itemUI.Bag.itemList[itemUI.Index];
        itemUI.SetupItemUI(item.itemData);
    }
}
