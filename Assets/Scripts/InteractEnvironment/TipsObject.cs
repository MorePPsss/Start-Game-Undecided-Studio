using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipsObject : InteractableObject
{
    public GameObject tipWindow;
    public TMP_Text tipsText;


    protected override void Interact()
    {
        haveInteracted = true;
        tipsText.text = "haha";
        tipWindow.SetActive(true);
    }

    public void OnNextButtonClick()
    {
        Debug.Log("Next"); 
    }

}
