using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType { BAG,WEAPON,ARMOR,FEET } //����

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
            case SlotType.WEAPON:
                break;
            case SlotType.ARMOR:
                break;
            case SlotType.FEET:
                break;
        }

        var item = itemUI.Bag.itemList[itemUI.Index];
        itemUI.SetupItemUI(item.itemData);
    }
}
