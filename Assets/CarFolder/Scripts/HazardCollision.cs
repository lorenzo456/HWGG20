using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Car" || collision.gameObject.tag == "Car2")
        {
            if (collision.gameObject.GetComponent<PolygonCollider2D>().IsTouching(gameObject.GetComponent<Collider2D>()))
            {
                collision.gameObject.transform.parent.GetComponent<Car_Movement_Plus>().DurabilityDamage();
                explosion();
            }
        }
    }

    void explosion()
    {
        Destroy(gameObject);
    }
}
