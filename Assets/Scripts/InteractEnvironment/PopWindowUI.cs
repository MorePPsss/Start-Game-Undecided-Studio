using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopWindowUI : InteractableObject
{
    [Header("Dialogue GameObject")]
    public GameObject Tip_WindowUIObject;

    [Header("Dialogue Detail")]
    public TextMeshProUGUI windowName;
    public TextMeshProUGUI contentText;
    public List<string> contentList;

    private int contentIndex = 0;
    private TextMeshProUGUI firstContent;

    void Start()
    {
        firstContent = Tip_WindowUIObject.transform.Find("Dialog/Text_tips").GetComponent<TextMeshProUGUI>();
        firstContent.text = contentList[contentIndex];
    }
    protected override void Interact()
    {
        haveInteracted = true;
        Show();
    }

    public void OnNextButtonClick()
    {
        contentIndex++;
        if (contentIndex >= contentList.Count)
        {
            Hide();
            return;
        }
        contentText.text = contentList[contentIndex];
    }

    public void OnCloseButtonClick()
    {
        haveInteracted = false;
        Hide();
    }

    public void Show()
    {
        Tip_WindowUIObject.SetActive(true);
    }

    public void Show(string popWindowTitle, string[] content)
    {
        contentList = new List<string>();
        windowName.text = popWindowTitle;
        contentList.AddRange(content);
        contentText.text = contentList[0];
    }

    public void Hide()
    {
        Tip_WindowUIObject.SetActive(false);
    }
}
