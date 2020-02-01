using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtns : MonoBehaviour
{
    public void LvlStart()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitBtn()
    {
        Debug.Log("QUITING...");
        Application.Quit();
    }
}
