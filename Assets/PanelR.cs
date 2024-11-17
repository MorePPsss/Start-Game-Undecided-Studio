using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelR : MonoBehaviour
{
    public float moveDistance = 5f; 
    public float moveSpeed = 2f;     
    public float stayDuration = 2f;   

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
            StartCoroutine(MoveRightAndReturnCoroutine());
        }
    }

    private IEnumerator MoveRightAndReturnCoroutine()
    {
        isMoving = true;

        Vector3 targetPositionRight = originalPosition + Vector3.right * moveDistance;

        while (Vector3.Distance(transform.position, targetPositionRight) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPositionRight, moveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(stayDuration);

        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false; 
    }
}
