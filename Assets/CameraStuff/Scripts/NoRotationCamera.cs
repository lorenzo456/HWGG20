using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotationCamera : MonoBehaviour
{
    public Transform car;

    // Update is called once per frame

    void Start()
    {
        transform.parent = null;
    }
    void Update()
    {
        transform.position = new Vector3(car.position.x,car.position.y + 3,transform.position.z);
        transform.rotation = Quaternion.Euler(0, 0, car.rotation.z);
    }
}
