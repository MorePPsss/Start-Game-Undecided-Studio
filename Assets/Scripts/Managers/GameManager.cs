using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    private GameObject player; // 用于保存玩家对象的引用

    List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();
    public void GameOver(string deadType)
    {
        NotifyObserver();
        UIManager.Instance.popGameOverWindow_NoContinueButton(deadType);
    }
    public void RestartGame()
    {
        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public GameObject GetPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player"); // 使用标签查找玩家对象
        }
        return player;
    }
    // 设置玩家对象（用于手动注册玩家）
    public void SetPlayer(GameObject playerObject)
    {
        player = playerObject;
    }

    public void AddObserver(IEndGameObserver observer)
    {
        endGameObservers.Add(observer);
    }

    public void RemoveObserver(IEndGameObserver observer)
    {
        endGameObservers.Remove(observer);
    }

    //When this function is called, all enemy game objects mounted with EnemyController will execute the EndNotify() method -By Kehao
    public void NotifyObserver()
    {
        foreach (var observer in endGameObservers)
        {
            observer.EndNotify();
        }
    }
}
