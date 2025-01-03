using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [Header("UI TextDetail")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI contentText;
    public TextMeshProUGUI tipContentText;
    public TextMeshProUGUI cameraIndexText;
    public TextMeshProUGUI baitNumText;
    [Header("UI Objects")]
    public GameObject windowObject;
    public GameObject tipUIObject;
    public GameObject baitNumUIObject;
    public GameObject gameOverUI; // ÓÎÏ·½áÊøµÄµ¯´° UI
    [Header("CameraInfo")]
    public CameraSwitcher cameraSwitcher;
    private List<string> contentList;
    private int contentIndex = 0;
    private TextMeshProUGUI titleTest;
    private TextMeshProUGUI contentTest;
    [Header("TipUI SettingDetail")]
    public float tipRemainingTime;

    private void Update()
    {
        ShowCameraUI();
    }

    public void ShowBaitNumUI(int baitNum)
    {
        baitNumUIObject.SetActive(true);
        baitNumText.text = "BaitNumber:" + baitNum;
    }
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
        StartCoroutine(HideTipAfterSeconds(tipRemainingTime));
    }
    public void HideTipUI()
    {
        tipUIObject.SetActive(false);
    }

    public void ShowCameraUI()
    {
        if (cameraSwitcher != null)
        {
            cameraIndexText.text = "Camera: " + cameraSwitcher.GetCurrentCameraIndex();
        }
    }

    public void HideWindowUI()
    {
        windowObject.SetActive(false);
    }

    public void OnNextButtonClick()
    {
        contentIndex++;
        if (contentIndex >= contentList.Count)
        {
            //UIManager.Instance.CloseWindow("TipWindow");
            HideWindowUI();
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
            case DeadType.Trap:
                contentTest.text = "Beware of the SecuritySystem!";
                break;
            case DeadType.Enemy:
                contentTest.text = "Be careful to avoid crazy security robots!";
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

    private IEnumerator HideTipAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UIManager.Instance.HideTipUI();
    }
}
