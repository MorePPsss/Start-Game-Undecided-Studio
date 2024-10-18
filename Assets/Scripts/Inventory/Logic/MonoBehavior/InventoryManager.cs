using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ʵ�ֱ�������&�Ƴ��߼�*/
public class InventoryManager : MonoBehaviour
{
    //����ģʽ
    public static InventoryManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public class DragData{
        public SlotHolder originalHolder;
        public RectTransform originalParent;
    }
  


    [Header("Inventory Data")]
    public ItemDBSO InventoryData;
    public ItemDBSO actionData;
    public ItemDBSO equipmentData;

    [Header("ContainerS")]
    public ContainerUI InventoryUI;
    public ContainerUI actionUI;
    public ContainerUI equipmentUI;

    [Header("Drag Canvas")]
    public Canvas dragCanvas;
    public DragData currentDrag;
    void Start()
    {
        if (InventoryUI != null)
        {
            InventoryUI.RefreshUI();
            actionUI.RefreshUI();
            equipmentUI.RefreshUI();
        }
        else
        {
            Debug.LogError("Container UI is not assigned!");
        }

    }
    #region 
    public bool CheckInInventoryUI(Vector3 position)
    {
        for (int i = 0; i < InventoryUI.slotHolders.Length; i++)
        {
            RectTransform t = InventoryUI.slotHolders[i].transform as RectTransform;
            if(RectTransformUtility.RectangleContainsScreenPoint(t,position))
            {
                return true;
            }
        }
        return false;
    }
    public bool CheckInActionUI(Vector3 position)
    {
        for (int i = 0; i < actionUI.slotHolders.Length; i++)
        {
            RectTransform t = actionUI.slotHolders[i].transform as RectTransform;

            if(RectTransformUtility.RectangleContainsScreenPoint(t,position))
            {
                return true;
            }
        }
        return false;
    }
    public bool CheckInEquipmentUI(Vector3 position)
    {
        for (int i = 0; i < equipmentUI.slotHolders.Length; i++)
        {
            RectTransform t = equipmentUI.slotHolders[i].transform as RectTransform;

            if(RectTransformUtility.RectangleContainsScreenPoint(t,position))
            {
                return true;
            }
        }
        return false;
    }
}
#endregion





//����ʹ��
