using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("AirPlane");
    }  
    public void QuitGame()
    {
        Debug.Log("quit game");
        Application.Quit();
    }
}
