using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("Dialogue Detail")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI contentText;
    public GameObject windowObject;
    private List<string> contentList;
    private int contentIndex = 0;

    public void Show(string popWindowTitle, string[] content)
    {
        nameText.text = popWindowTitle;
        contentList = new List<string>(content);
        //contentList.AddRange(content);
        contentIndex = 0;
        contentText.text = contentList[0];
        windowObject.SetActive(true);
    }

    public void Show()
    {
        windowObject.SetActive(true);
    }

    public void Hide()
    {
        windowObject.SetActive(false);
    }

    public void OnNextButtonClick()
    {
        contentIndex++;
        if (contentIndex >= contentList.Count)
        {
            //UIManager.Instance.CloseWindow("TipWindow");
            Hide();
            return;
        }
        contentText.text = contentList[contentIndex];
    }

}
