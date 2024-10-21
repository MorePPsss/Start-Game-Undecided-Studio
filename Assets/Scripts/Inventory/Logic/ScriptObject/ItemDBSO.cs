using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*����ItemSO*/
[CreateAssetMenu]
public class ItemDBSO : ScriptableObject
{
    public List<InventoryItem> itemList = new List<InventoryItem>();

    public void AddItem(ItemSO newItemData)
    {
        bool found = false;
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemData == null && !found)
            {
                itemList[i].itemData = newItemData;
                break;
            }
        }
    }
}

[System.Serializable]
public class InventoryItem
{
    public ItemSO itemData;
}