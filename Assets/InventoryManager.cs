using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*实现背包添加&移除逻辑*/
public class InventoryManager : MonoBehaviour
{
    //单例模式
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
    
    public List<ItemSO> itemList;//运行游戏 拾取测试物品 点开场景中的Manager游戏对象 通过Inspector观察是否正确捡起

    //测试
    public int id;
    public string name;
    public string description;

    public void AddItem(ItemSO itemSO)
    {
        itemList.Add(itemSO);
        showItemDetail(itemSO);//测试使用

        //TODO 在背包UI中显示该物品

        //TODO 如果有必要可以弹出一个message来提示为玩家捡到了什么
    }

    public void RemoveItem(ItemSO itemSO) 
    {
        itemList.Remove(itemSO);
    }

    //测试使用
    public void showItemDetail(ItemSO itemSO)
    {
        id = itemSO.id;
        name = itemSO.name;
        description = itemSO.description;
    }
}
