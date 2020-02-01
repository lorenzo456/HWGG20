using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTime : MonoBehaviour
{

    public int arrowSpeed;
    public float distanceFromCircle;
    public GameObject[] arrows;
    public GameObject parent;
    


    GameObject newParent;
    bool arrowMove = false;
    float arrowDist = 0f;

    public float deletedist;

    void Start()
    {

        newParent = Instantiate(parent, this.transform);
        
        SpawnArrow();
    }

    void SpawnArrow()
    {
        int arrowType = Random.Range(0, 3);
        GameObject newarrow;
        Vector3 moveDirection;

        if (arrowType == 0)
        {
            moveDirection = Vector3.up;
            newarrow = Instantiate(arrows[0], new Vector3(0f, 0f, 0f), Quaternion.Euler(0f,0f,0f));
            newarrow.transform.position += moveDirection * -distanceFromCircle;
        }
        else if(arrowType == 1)
        {
            moveDirection = Vector3.left;
            newarrow = Instantiate(arrows[1], new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 90f));
            newarrow.transform.position += moveDirection * -distanceFromCircle;
        }
        else if (arrowType == 2)
        {
            moveDirection = Vector3.down;
            newarrow = Instantiate(arrows[2], new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, 180f));
            newarrow.transform.position += moveDirection * -distanceFromCircle;
        }
        else
        {
            moveDirection = Vector3.right;
            newarrow = Instantiate(arrows[3], new Vector3(0f, 0f, 0f), Quaternion.Euler(0f, 0f, -90f));
            newarrow.transform.position += moveDirection * -distanceFromCircle;
        }
        newarrow.AddComponent<Rigidbody2D>();
        newarrow.transform.parent = newParent.transform;
        Debug.Log("" + newarrow.transform.parent.name);
        MoveArrow(newarrow, moveDirection);
    }

    void MoveArrow(GameObject newarrow, Vector3 moveDirection)
    {
        newarrow.GetComponent<Rigidbody2D>().gravityScale = 0f;
        newarrow.GetComponent<Rigidbody2D>().AddForce(moveDirection * arrowSpeed);
        arrowMove = true;
    }

    void Update()
    {
        arrowDist = (newParent.transform.GetChild(0).transform.position - newParent.transform.position).magnitude;
        if (newParent.transform.childCount == 1)
        {
            if (arrowDist > deletedist)
            {
                Destroy(newParent.transform.GetChild(0).gameObject);
                SpawnArrow();
            }
        }
    }
}
