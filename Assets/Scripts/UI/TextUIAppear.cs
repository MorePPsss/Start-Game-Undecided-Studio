using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.001f; 
    private string fullText; 
    private string currentText = ""; 
    private TMP_Text uiText; 

    void Start()
    {
        
        uiText = GetComponent<TMP_Text>();
        fullText = uiText.text;
        uiText.text = ""; 
        StartCoroutine(ShowText()); 
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length / 2; i++)
        {
            currentText = fullText.Substring(0, i * 2); 
            uiText.text = currentText; 
            yield return new WaitForSeconds(delay); 
        }
    }
}