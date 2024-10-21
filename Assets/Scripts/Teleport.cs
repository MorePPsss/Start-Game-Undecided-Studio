using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Teleport : MonoBehaviour
{
    public int nextSceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger activated, object collided: " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger area. Loading scene " + nextSceneIndex);
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
