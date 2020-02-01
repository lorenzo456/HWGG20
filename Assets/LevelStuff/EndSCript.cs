using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSCript : MonoBehaviour
{
    public void EndBtn()
    {
        SceneManager.LoadScene(0);
    }
}
