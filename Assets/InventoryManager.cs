using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*ʵ�ֱ������&�Ƴ��߼�*/
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
    
    public List<ItemSO> itemList;//������Ϸ ʰȡ������Ʒ �㿪�����е�Manager��Ϸ���� ͨ��Inspector�۲��Ƿ���ȷ����

    //����
    public int id;
    public string name;
    public string description;

    public void AddItem(ItemSO itemSO)
    {
        itemList.Add(itemSO);
        showItemDetail(itemSO);//����ʹ��

        //TODO �ڱ���UI����ʾ����Ʒ

        //TODO ����б�Ҫ���Ե���һ��message����ʾΪ��Ҽ���ʲô
    }

    public void RemoveItem(ItemSO itemSO) 
    {
        itemList.Remove(itemSO);
    }

    //����ʹ��
    public void showItemDetail(ItemSO itemSO)
    {
        id = itemSO.id;
        name = itemSO.name;
        description = itemSO.description;
    }
}
