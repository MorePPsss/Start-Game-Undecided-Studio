using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelLR : MonoBehaviour
{
    public float moveDistance;     
    public float moveDuration;      
    public float moveSpeed;         

    private Vector3 originalPosition;
    private bool isMoving = false;

    private void Start()
    {
        originalPosition = transform.position;  
    }

    public void StartMoving()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveLeftAndRightCoroutine());
        }
    }

    private IEnumerator MoveLeftAndRightCoroutine()
    {
        isMoving = true;

        // move to left target
        Vector3 targetPositionLeft = originalPosition + Vector3.left * moveDistance;
        while (Vector3.Distance(transform.position, targetPositionLeft) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPositionLeft, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // stay
        yield return new WaitForSeconds(moveDuration);

        // move opposite
        Vector3 targetPositionRight = originalPosition + Vector3.right * moveDistance;
        while (Vector3.Distance(transform.position, targetPositionRight) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPositionRight, moveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(moveDuration);

        // back
        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }
}
