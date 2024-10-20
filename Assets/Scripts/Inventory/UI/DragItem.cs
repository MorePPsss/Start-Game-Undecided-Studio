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

        transform.SetParent(InventoryManager.Instance.dragCanvas.transform,true);//记录原始数据
    }
    

    public void OnDrag(PointerEventData eventData)
    {
        //跟随鼠标位置移动
        transform.position = eventData.position;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(EventSystem.current.IsPointerOverGameObject())
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
        transform.SetParent(InventoryManager.Instance.currentDrag.originalParent);

        RectTransform t = transform as RectTransform;
        t.offsetMax = -Vector2.one * 5;
        t.offsetMin = Vector2.one * 5;
        //放下物品 交换数据
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
