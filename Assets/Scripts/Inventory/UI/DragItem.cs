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
            if(InventoryManager.Instance.CheckInActionUI(eventData.position) || InventoryManager.Instance.CheckInInventoryUI(eventData.position) || InventoryManager.Instance.CheckInEquipmentUI(eventData.position) )
            {
                if (eventData.pointerEnter.gameObject.GetComponent<SlotHolder>())
                    targetHolder = eventData.pointerEnter.gameObject.GetComponent<SlotHolder>();
                else
                    targetHolder = eventData.pointerEnter.gameObject.GetComponentInParent<SlotHolder>();
                switch(targetHolder.slotType)
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
                targetHolder.UpdateItem();
            }
        }
        else
        {
            Spawn();
            currentHolder.UpdateItem();
            
        }

        transform.SetParent(InventoryManager.Instance.currentDrag.originalParent);

        RectTransform t = transform as RectTransform;
        t.offsetMax = -Vector2.one * 5;
        t.offsetMin = Vector2.one * 5;
        
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

        Vector3 dropposition = playerposition.Getposition();
        GameObject obj = Instantiate(Dropitem.prefab,dropposition + Vector3.down * 1 , Quaternion.identity);
        var tempItem = currentHolder.itemUI.Bag.itemList[currentHolder.itemUI.Index];
        tempItem.itemData = null;
    }

    public void SwapItem()
    {
        var targetItem = targetHolder.itemUI.Bag.itemList[targetHolder.itemUI.Index];
        var tempItem = currentHolder.itemUI.Bag.itemList[currentHolder.itemUI.Index];

        bool isSame = tempItem.itemData == targetItem.itemData;

        if (isSame)
        {
            tempItem.itemData = null;
        }
        else
        {
            currentHolder.itemUI.Bag.itemList[currentHolder.itemUI.Index] = targetItem;
            targetHolder.itemUI.Bag.itemList[targetHolder.itemUI.Index] = tempItem;
        }
    }
}
