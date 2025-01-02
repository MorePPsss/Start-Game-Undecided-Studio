using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ItemUI))]
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    ItemUI currentItemUI;
    SlotHolder currentHolder;
    SlotHolder targetHolder;
    public GameObject item;
    public ItemSO Dropitem;
    public ItemDBSO inventoryData;
    public PlayerController playerposition;

    void Awake()
    {
        currentItemUI = GetComponent<ItemUI>();
        currentHolder = GetComponentInParent<SlotHolder>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        InventoryManager.Instance.currentDrag = new InventoryManager.DragData();
        InventoryManager.Instance.currentDrag.originalHolder = GetComponentInParent<SlotHolder>();
        InventoryManager.Instance.currentDrag.originalParent = (RectTransform)transform.parent;

        Dropitem = currentItemUI.currentItem;

        transform.SetParent(InventoryManager.Instance.dragCanvas.transform,true);//recoed original data
    }
    

    public void OnDrag(PointerEventData eventData)
    {
        //move with mouse position
        transform.position = eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //put item down, switch data
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if(InventoryManager.Instance.CheckInActionUI(eventData.position) || InventoryManager.Instance.CheckInInventoryUI(eventData.position) || InventoryManager.Instance.CheckInEquipmentUI(eventData.position) ) // check if mouse is inside one of the slot
            {
                if (eventData.pointerEnter.gameObject.GetComponent<SlotHolder>()) //check if the target position contains a slotholder
                    targetHolder = eventData.pointerEnter.gameObject.GetComponent<SlotHolder>();
                else
                    targetHolder = eventData.pointerEnter.gameObject.GetComponentInParent<SlotHolder>();//if there is something over the slot, it will return an image, so to get the slotholder of the target slot, we go to parent.
                switch(targetHolder.slotType)//Distinguishing slottype. For example, we don't want to place items that can't be equipped into the equipment slot.
                {
                    case SlotType.BAG:
                        SwapItem();
                    break;
                    case SlotType.WEAPON:
                        SwapItem();
                    break;
                    case SlotType.FEET:
                        SwapItem();
                    break;
                    case SlotType.ACTION:
                        SwapItem();
                     break;
                }
                currentHolder.UpdateItem();
                targetHolder.UpdateItem();// update 
            }
        }
        else
        {
            Spawn();
            currentHolder.UpdateItem();
            
        }

        transform.SetParent(InventoryManager.Instance.currentDrag.originalParent); // return the slot of dragitem to it's original position

        RectTransform t = transform as RectTransform;
        t.offsetMax = -Vector2.one * 5;
        t.offsetMin = Vector2.one * 5; //fix offset
        
    }

    public void Spawn() 
    {
        if (Dropitem == null)
        {
            Debug.LogError("currentItem is null! Make sure the item data is correctly assigned.");
            return;
        }

        if (Dropitem.prefab == null)
        {
            Debug.LogError("Prefab is null! Make sure the prefab is assigned in the ItemSO.");
            return;
        }
        if (playerposition == null)
        {
            Debug.LogError("Player character is null! Please assign it in the inspector.");
            return;
        }

        Vector3 dropposition = playerposition.Getposition(); //get player position ,and set it to dropposition
            Debug.LogError(Dropitem.prefab);
        
        GameObject obj = Instantiate(Dropitem.prefab,dropposition , Quaternion.identity); //instantiate an gameobject on dropposition
        var tempItem = currentHolder.itemUI.Bag.itemList[currentHolder.itemUI.Index];
        tempItem.itemData = null; //remove it from database
    }

    public void SwapItem()
    {
        var targetItem = targetHolder.itemUI.Bag.itemList[targetHolder.itemUI.Index];//get inventoryItem of targetItem from the itemList denpending on the Index;
        var tempItem = currentHolder.itemUI.Bag.itemList[currentHolder.itemUI.Index];//get inventoryItem of the dragitem from the itemList denpending on the Index;

        bool isSame = tempItem.itemData == targetItem.itemData;//prepared for stackable item
        //targetitem.amount += tempitem.amount;

        if (isSame && targetItem.itemData.stackable)
        {
            targetItem.amount += tempItem.amount;
            tempItem.itemData = null;// if it's stackable and same item, add amount should be on the next line,then destory the dragitem
            tempItem.amount = 0;
        }
        else
        {
            currentHolder.itemUI.Bag.itemList[currentHolder.itemUI.Index] = targetItem;
            targetHolder.itemUI.Bag.itemList[targetHolder.itemUI.Index] = tempItem; //swap item
        }
    }
}
