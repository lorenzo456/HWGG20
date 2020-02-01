using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadHUD : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);
        //SceneManager.GetSceneByName("CameraStuff/Cameras").buildIndex
    }


}
