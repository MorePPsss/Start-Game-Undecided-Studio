using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Train : MonoBehaviour
{
    public Vector3 targetPos;
    public float moveSpeed;
    private void Start()
    {
        targetPos = transform.position;
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
