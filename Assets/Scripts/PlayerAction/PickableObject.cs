using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : InteractableObject
{
    public ItemSO itemSO;
    protected override void Interact()
    {
        Debug.Log("��Ҽ�����Ʒ��");
        Destroy(this.gameObject);
        //TODO ���뱳�����ݽṹ
        InventoryManager.Instance.InventoryData.AddItem(itemSO);
        InventoryManager.Instance.InventoryUI.RefreshUI();
    }
}
