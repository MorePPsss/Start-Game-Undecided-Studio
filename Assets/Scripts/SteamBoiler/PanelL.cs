using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PanelL : MonoBehaviour
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
            StartCoroutine(MoveLeftAndReturnCoroutine());
        }
    }

    private IEnumerator MoveLeftAndReturnCoroutine()
    {
        isMoving = true;

        Vector3 targetPositionLeft = originalPosition + Vector3.left * moveDistance;

        while (Vector3.Distance(transform.position, targetPositionLeft) > 0.01f)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPositionLeft, moveSpeed * Time.deltaTime);
            transform.position = newPosition;

            if (playerNavMeshAgent != null)
            {
                Vector3 playerTargetPosition = transform.position; 
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
