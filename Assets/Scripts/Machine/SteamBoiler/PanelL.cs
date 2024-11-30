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
        isMoving = true; // Mark the platform as moving

        Vector3 targetPositionLeft = originalPosition + Vector3.left * moveDistance; // Calculate the target position to the left

        // Move the platform to the left target position
        while (Vector3.Distance(transform.position, targetPositionLeft) > 0.01f)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPositionLeft, moveSpeed * Time.deltaTime);
            transform.position = newPosition; // Update platform's position

            // Use Warp to synchronize the player's position with the platform
            if (playerNavMeshAgent != null)
            {
                playerNavMeshAgent.Warp(newPosition); // Force the player to warp to the platform's position
            }

            yield return null; // Wait for the next frame
        }

        yield return new WaitForSeconds(stayDuration); // Wait at the left position for the specified duration

        // Move the platform back to its original position
        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            transform.position = newPosition; // Update platform's position

            // Use Warp to synchronize the player's position with the platform
            if (playerNavMeshAgent != null)
            {
                playerNavMeshAgent.Warp(newPosition); // Force the player to warp to the platform's position
            }

            yield return null; // Wait for the next frame
        }

        isMoving = false; // Mark the platform as not moving
    }
}
