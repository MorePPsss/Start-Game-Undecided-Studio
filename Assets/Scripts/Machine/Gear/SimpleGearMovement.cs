using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGearMovement : MonoBehaviour
{
    public Vector3 rotateAxisDir = Vector3.up;
    public float lineSpeed;                 //Gear teeth speed: number of teeth per second
    public float teethNum;
    private void Start()
    {
        rotateAxisDir = rotateAxisDir.normalized;
    }
    void Update()
    {
        transform.Rotate(rotateAxisDir * Time.deltaTime * 360 * lineSpeed / teethNum);
    }
}
