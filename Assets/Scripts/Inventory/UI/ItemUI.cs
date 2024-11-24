using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
   public Image icon = null;
   public Text amount = null;
   public ItemDBSO Bag { get; set; }
   public int Index { get; set; } = -1;
    public ItemSO currentItem { get; private set; } // add currentItem 


    public void SetupItemUI(ItemSO item, int itemAmount)
    {
        currentItem = item;
        if (item != null)
        {
            icon.sprite = item.icon; //assign the icon
            
            amount.text = itemAmount.ToString();
            icon.gameObject.SetActive(true);
        }
        else
            icon.gameObject.SetActive(false);
    }
}
