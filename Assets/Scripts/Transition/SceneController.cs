using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    private GameObject player;
    public NavMeshAgent playerAgent;
    public void TransitionToDestination(TransitionPoint transitionPoint)
    {
        switch(transitionPoint.transitionType)
        {
            case TransitionPoint.TransitionType.SameScene:
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, transitionPoint.destinationTag));
                break;
            case TransitionPoint.TransitionType.DifferentScene:

                break;
        }
    }

    //异步加载（需要的场景在后台已经加载好）
    IEnumerator Transition(string sceneName, TransitionDestination.DestinationTag destinationTag)
    {
        player = GameManager.Instance.GetPlayer();
        playerAgent = player.GetComponent<NavMeshAgent>();
        playerAgent.enabled = false;
        //传送: using SetPositionAndRotation()
        player.transform.SetPositionAndRotation(GetTransition(destinationTag).transform.position, GetTransition(destinationTag).transform.rotation);
        playerAgent.enabled = true;
        yield return null;

    }
    
    private TransitionDestination GetTransition(TransitionDestination.DestinationTag destinationTag)
    {
        var entrances = FindObjectsOfType<TransitionDestination>();
        for (int i = 0; i < entrances.Length; i++) 
        {
            if (entrances[i].destinationTag == destinationTag)
            {
                return entrances[i];
            }
        }
        return null;
    }
}
