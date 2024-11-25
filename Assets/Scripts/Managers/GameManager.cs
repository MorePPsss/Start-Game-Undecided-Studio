using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject gameOverUI; // ��Ϸ�����ĵ��� UI
    private GameObject player; // ���ڱ�����Ҷ��������
    List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();
    public void GameOver()
    {
        NotifyObserver();
        gameOverUI.SetActive(true);
    }
    public GameObject GetPlayer()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player"); // ʹ�ñ�ǩ������Ҷ���
        }
        return player;
    }
    // ������Ҷ��������ֶ�ע����ң�
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
