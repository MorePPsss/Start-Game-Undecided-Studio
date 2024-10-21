using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*The base class of interactive logic -By Kehao*/
public class InteractableObject : MonoBehaviour
{
    public NavMeshAgent playerAgent;
    private bool haveInteracted = false;
    public void OnClick(NavMeshAgent playerAgent)
    {
        this.playerAgent = playerAgent;
        /*Take two steps to get nearby+interact -By Kehao*/
        //S1 nearby
        playerAgent.SetDestination(transform.position);
        //S2 interact

        //TODO Interaction with the environment!
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAgent != null && haveInteracted == false && playerAgent.pathPending == false)
        {
            if (playerAgent.remainingDistance <= 1)
            {
                Interact();
                haveInteracted = true;
            }
        }
    }
    /*For subclasses, interaction should be rewritable for different types of items: 
     installable items, objects that can affect the scene -By Kehao
    */
    protected virtual void Interact()
    {
        Debug.Log("Interactor with Player£¡");
    }
}
