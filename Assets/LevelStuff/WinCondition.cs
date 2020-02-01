using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Finish")
        {
            if(this.tag == "Player1")
            {
                SceneManager.LoadScene(2);
            }
            if(this.tag == "Player2")
            {
                SceneManager.LoadScene(3);
            }
        }
    }
}
