using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCollide : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tag.ENEMY))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag(Tag.PLAYER))
        {
            //TODO: Game Over
            GameManager.Instance.GameOver(DeadType.Trap);
        }
    }
}
