using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pausemMenuUI;
    void Update()
    {
        if (InputManager.instance.pauseAction.triggered)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else Pause();
        }
        if (InputManager.instance.stageReloadAction.triggered)
        {
            Restart();
        }
    }
    public void Restart()
    {
        ResetBag();
        SceneManager.LoadScene("Main");

    }
    public void ResetBag()
    {
        for (int i = 0; i < 2; i++)
        {
            InventoryManager.Instance.equipmentData.itemList[i].itemData = null;
            InventoryManager.Instance.InventoryData.itemList[i].itemData = null;
        }
        for (int i = 0; i < 6; i++)
        {
            InventoryManager.Instance.actionData.itemList[i].itemData = null;
        }
    }
    public void Resume()
    {
        pausemMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pausemMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
