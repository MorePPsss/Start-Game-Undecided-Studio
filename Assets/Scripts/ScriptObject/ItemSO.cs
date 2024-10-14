using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEditor.Playables;
using UnityEngine;

//S1：鼠标放在DataScriptObject文件上 右键点击Create 创建一个新的ItemSO 可以随意增加游戏中可以捡起的物品
//S2：记得在DataScriptObject文件中的列表中添加新增的ItemSO方便管理，虽然目前没有涉及调用
[CreateAssetMenu()]
public class ItemSO : ScriptableObject
{
    public int id;// 物品ID
    public new string name;// 物品名字
    public ItemType itemType;// 物品类型
    public string description;// 物品描述
    public List<Ability> AbilityList;// 机器人解锁的能力
    public Sprite icon;// 物品在背包里的图标
    public GameObject prefab;// 物品的预制体
}

//物品类型
public enum ItemType
{
    Installable,//可安装于自身的物品
    EnvironmentInteraction//可与环境互动的道具
}

//能力类型
public enum AbilityType
{
    EnhancedJump,//弹簧——增强弹跳
    IntegratingMachinery//获得某样道具——机器人可以将自生融入环境中的机关操控它们
}

[Serializable]
public class Ability
{
    public AbilityType abilityType;
    public int abilityID;
    public Ability()
    {

    }
    public Ability(AbilityType abilityType, int abilityID)
    {
        this.abilityType = abilityType; 
        this.abilityID = abilityID;
    }
}
