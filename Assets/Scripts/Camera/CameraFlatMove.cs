using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFlatMove : MonoBehaviour
{
    
    public float moveSpeed = 80f; 
    public float smoothTime = 0.12f; 
    private Vector3 velocity = Vector3.zero; 
    private GameObject player;

    public bool follow;
    public bool inZone;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (follow)
        {
            CameraFollowPlayer();
        }else
        {
            MovetoLevel1PresetPos();
        }
    }
    private void CameraFollowPlayer()
    {
        Vector3 targetPos = transform.position;
        targetPos.x = player.transform.position.x;
        SetCameraPos(targetPos);
    }
    public void SetCameraPos(Vector3 pos)
    {
        Vector3 targetPos = pos;
        Vector3 target = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }
    public void MovetoLevel1PresetPos()
    {
        Vector3 pos = new Vector3(-5.84f, 4.15f, -19.03f);
        if (inZone)
        {
            pos.x *= 0.1f;
            if(transform.position.x > 0)
            {
                pos.x = -pos.x;
            }
        }
        else if (player.transform.position.x > 0)
        {
            pos.x = 5.84f;
        }
        if(player.transform.position.y > 4)
        {
            pos.y = 8.29f;
        }
        SetCameraPos(pos);
    }
}
