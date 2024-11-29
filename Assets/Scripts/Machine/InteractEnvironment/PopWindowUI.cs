using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopWindowUI : InteractableObject
{
    public string windowName;
    public string[] contentList;

    protected override void Interact()
    {
        haveInteracted = true;
        UIManager.Instance.Show(windowName, contentList);
    }
}
