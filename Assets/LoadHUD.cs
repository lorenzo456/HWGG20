using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHUD : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        SceneManager.LoadSceneAsync(1);
    }

    private void OnDestroy()
    {
        SceneManager.UnloadSceneAsync(1);
    }

}
