using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEditor.Playables;
using UnityEngine;

//S1��������DataScriptObject�ļ��� �Ҽ����Create ����һ���µ�ItemSO ��������������Ϸ�п��Լ������Ʒ
//S2���ǵ���DataScriptObject�ļ��е��б������������ItemSO���������ȻĿǰû���漰����
[CreateAssetMenu()]
public class ItemSO : ScriptableObject
{
    public int id;// ��ƷID
    public new string name;// ��Ʒ����
    public ItemType itemType;// ��Ʒ����
    public string description;// ��Ʒ����
    public List<Ability> AbilityList;// �����˽���������
    public Sprite icon;// ��Ʒ�ڱ������ͼ��
    public GameObject prefab;// ��Ʒ��Ԥ����
}

//��Ʒ����
public enum ItemType
{
    Installable,//�ɰ�װ���������Ʒ
    EnvironmentInteraction//���뻷�������ĵ���
}

//��������
public enum AbilityType
{
    EnhancedJump,//���ɡ�����ǿ����
    IntegratingMachinery//���ĳ�����ߡ��������˿��Խ��������뻷���еĻ��زٿ�����
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
