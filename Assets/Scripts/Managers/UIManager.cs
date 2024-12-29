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

    public TextMeshProUGUI tipContentText;

    public GameObject windowObject;
    public GameObject tipUIObject;
    private List<string> contentList;
    private int contentIndex = 0;
    public GameObject gameOverUI; // ÓÎÏ·½áÊøµÄµ¯´° UI
    private TextMeshProUGUI titleTest;
    private TextMeshProUGUI contentTest;

    public void Show(string popWindowTitle, string[] content)
    {
        nameText.text = popWindowTitle;
        contentList = new List<string>(content);
        //contentList.AddRange(content);
        contentIndex = 0;
        contentText.text = contentList[0];
        windowObject.SetActive(true);
    }

    public void ShowTipUI(string tipContent)
    {
        tipContentText.text = tipContent;
        tipUIObject.SetActive(true);
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

    public void popGameOverWindow_NoContinueButton(string deadType)
    {
        titleTest = gameOverUI.transform.Find("Dialog/Title/Text_Title").GetComponent<TextMeshProUGUI>();
        contentTest = gameOverUI.transform.Find("Dialog/Text_Content").GetComponent<TextMeshProUGUI>();
        titleTest.text = "Game Over!";
        switch (deadType)
        {
            case DeadType.Hammered:
                contentTest.text = "Beware of the hammer!";
                break;
            case DeadType.Enemy:
                contentTest.text = "Watch out the Enemy next time!";
                break;
            case DeadType.Burned:
                contentTest.text = "Watch out the Fire next time!";
                break;
            default:
                contentTest.text = "You died. Try again!";
                break;
        }
        gameOverUI.SetActive(true);
    }
}
