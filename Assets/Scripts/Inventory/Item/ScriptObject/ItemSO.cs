using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEditor.Playables;
using UnityEngine;

/*Create a ScriptObject for an item without mounting it to any game object -By Kehao
Usage£º
S1£ºRight click on the DataScriptObject file and create a new ItemSO to freely add items that can be picked up in the game
S2£ºRemember to add the newly added ItemSOs to the list in the DataScriptObject file for easy management, although there are currently no calls involved
 */

[CreateAssetMenu()]
public class ItemSO : ScriptableObject
{
    public int id;
    public new string name;
    public ItemType itemType;
    public string description;
    public List<Ability> AbilityList;
    public Sprite icon;
    public GameObject prefab;
    [Header("Equipment")]
    public GameObject Equipment_prefab;
}

//Item type
public enum ItemType
{
    Installable,//Items that can be installed on oneself
    EnvironmentInteraction//Props that can interact with the environment
}

//Ability type
public enum AbilityType
{
    EnhancedJump,//Spring - Enhance Bounce
    IntegratingMachinery//Obtain a certain item - robots can manipulate mechanisms that integrate themselves into the environment to control them
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
