using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Manage ItemSO -By Kehao*/
[CreateAssetMenu]
public class ItemDBSO : ScriptableObject
{
    public List<InventoryItem> itemList = new List<InventoryItem>();

    public void AddItem(ItemSO newItemData,int amount) //add item to the list
    {
        bool found = false;
        if (newItemData.stackable){
            foreach (var item in itemList){
                if (item.itemData == newItemData){
                    item.amount += amount;
                    found = true;
                    break;
                }
            }
        }
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemData == null && !found)
            {
                itemList[i].itemData = newItemData;
                itemList[i].amount = amount;
                break;
            }
        }
    }
}

[System.Serializable]
public class InventoryItem
{
    public ItemSO itemData;
    public int amount;
}