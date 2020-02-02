using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardCollision : MonoBehaviour
{
    public Sprite Boom;
    bool enabled = true;
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
        if (enabled)
        {
            if(collision.gameObject.name == "CAR(body)")
            {
                if (collision.gameObject.GetComponent<PolygonCollider2D>().IsTouching(gameObject.GetComponent<Collider2D>()))
                {
                    collision.gameObject.transform.parent.GetComponent<Car_Movement_Plus>().DurabilityDamage();
                    explosion();
                }

            }
        }
    }

    void explosion()
    {
        enabled = false;
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        if(gameObject.name == "Collision_Car(body)")
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Boom;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Boom;
        }
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);

    }
}
