using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class PanelR : MonoBehaviour
{
    public float moveDistance;  
    public float moveSpeed;     
    public float stayDuration;  

    private Vector3 originalPosition;  
    private bool isMoving = false;     

    private Transform playerTransform; 
    private NavMeshAgent playerNavMeshAgent;

    private void Start()
    {
        originalPosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerNavMeshAgent = other.GetComponent<NavMeshAgent>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = null; 
            playerNavMeshAgent = null;
        }
    }
    public void StartMoving()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveRightAndReturnCoroutine());
        }
    }
    private IEnumerator MoveRightAndReturnCoroutine()
    {
        isMoving = true;

        Vector3 targetPositionRight = originalPosition + Vector3.right * moveDistance;


        while (Vector3.Distance(transform.position, targetPositionRight) > 0.01f)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPositionRight, moveSpeed * Time.deltaTime);
            transform.position = newPosition;


            if (playerNavMeshAgent != null)
            {
                Vector3 playerTargetPosition = newPosition + Vector3.right * 0.5f;
                playerNavMeshAgent.SetDestination(playerTargetPosition); 
            }

            yield return null;
        }

        yield return new WaitForSeconds(stayDuration);

        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            transform.position = newPosition;

            if (playerNavMeshAgent != null)
            {
                playerNavMeshAgent.SetDestination(transform.position); 
            }

            yield return null;
        }

        isMoving = false;
    }
}
