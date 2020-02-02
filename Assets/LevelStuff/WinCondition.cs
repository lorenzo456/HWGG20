using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "Car")
        {
            SceneManager.LoadScene(2);
        }
        if(other.gameObject.tag == "Car2")
        {
            SceneManager.LoadScene(3);
        }
        
    }
}
