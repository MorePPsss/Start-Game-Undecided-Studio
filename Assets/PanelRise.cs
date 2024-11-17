using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelRise : MonoBehaviour
{
    public float riseHeight;         
    public float riseDuration;       
    public float riseSpeed;          

    private Vector3 originalPosition;
    private bool isRising = false;

    private void Start()
    {
        originalPosition = transform.position;  
    }

    public void StartRising()
    {
        if (!isRising)
        {
            StartCoroutine(RiseAndFallCoroutine());
        }
    }

    private IEnumerator RiseAndFallCoroutine()
    {
        isRising = true;

        // rise to target
        Vector3 targetPosition = originalPosition + Vector3.up * riseHeight;
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, riseSpeed * Time.deltaTime);
            yield return null;
        }

        // stay
        yield return new WaitForSeconds(riseDuration);

        // back
        while (Vector3.Distance(transform.position, originalPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, riseSpeed * Time.deltaTime);
            yield return null;
        }

        isRising = false;
    }
}

