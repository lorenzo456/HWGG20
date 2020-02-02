using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start : MonoBehaviour
{

    public GameObject Prefab;
    public GameObject Parent;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Prefab, Parent.transform );
    }

    
}
