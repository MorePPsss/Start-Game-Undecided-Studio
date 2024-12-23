using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFlatMove : MonoBehaviour
{
    GameObject player;
    public float moveSpeed = 5f; 
    public float smoothTime = 0.3f; 
    private Vector3 velocity = Vector3.zero; 
    public bool follow;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (follow)
        {
            CameraFollowPlayer();
        }
    }
    private void CameraFollowPlayer()
    {
        Vector3 targetPos = transform.position;
        targetPos.x = player.transform.position.x;
        Vector3 target = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }
    public void SetCameraPos(Vector3 pos)
    {
        Vector3 targetPos = pos;
        Vector3 target = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }
    
}
