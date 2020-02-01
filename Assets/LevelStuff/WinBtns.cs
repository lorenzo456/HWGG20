using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinBtns : MonoBehaviour
{
    public void TopWin()
    {
        SceneManager.LoadScene(2);
    }

    public void BotWin()
    {
        SceneManager.LoadScene(3);
    }
}
