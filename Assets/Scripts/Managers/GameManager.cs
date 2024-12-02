using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    private GameObject player; // ���ڱ�����Ҷ��������

    List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();
    public void GameOver(string deadType)
    {
        NotifyObserver();
        UIManager.Instance.popGameOverWindow_NoContinueButton(deadType);
    }
    public void RestartGame()
    {
        // ���¼��ص�ǰ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
