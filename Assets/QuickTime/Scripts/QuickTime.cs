using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTime : MonoBehaviour
{
    public bool QTPlay;
    public int arrowSpeed;
    public float distanceFromCircle;
    public GameObject[] arrows;
    public GameObject parent;
    


    GameObject newParent;
    bool arrowMove = false;
    float arrowDist = 0f;

    public float deletedist;

    private KeyCode up, down, left, right;
    public int points = 0;

    public float perfectDist;
    public float goodDist;
    public float badDist;

    public int winPoints;

    void Start()
    {
       
        up = KeyCode.W;
        down = KeyCode.S;
        left = KeyCode.A;
        right = KeyCode.D;

        //up = KeyCode.UpArrow;
        //down = KeyCode.DownArrow;
        //left = KeyCode.LeftArrow;
        //right = KeyCode.RightArrow;

        
        newParent = Instantiate(parent, new Vector3(0f,0f,0f), Quaternion.identity);
        newParent.transform.parent = this.transform;
        newParent.transform.position += new Vector3(0f,0f,5f);
        SpawnArrow();
        
    }

    void SpawnArrow()
    {
        if (QTPlay)
        {
            int arrowType = Random.Range(0, 3);
            GameObject newarrow;
            Vector3 moveDirection;

            if (arrowType == 0)
            {
                moveDirection = Vector3.up;
                newarrow = Instantiate(arrows[0], newParent.transform);
                newarrow.transform.position += moveDirection * -distanceFromCircle;
            }
            else if(arrowType == 1)
            {
                moveDirection = Vector3.left;
                newarrow = Instantiate(arrows[1], newParent.transform);
                newarrow.transform.position += moveDirection * -distanceFromCircle;
            }
            else if (arrowType == 2)
            {
                moveDirection = Vector3.down;
                newarrow = Instantiate(arrows[2], newParent.transform);
                newarrow.transform.position += moveDirection * -distanceFromCircle;
            }
            else
            {
                moveDirection = Vector3.right;
                newarrow = Instantiate(arrows[3], newParent.transform);
                newarrow.transform.position += moveDirection * -distanceFromCircle;
            }
            newarrow.AddComponent<Animation>();
            newarrow.AddComponent<Rigidbody2D>();
            //newarrow.transform.parent = newParent.transform;
            Debug.Log("" + newarrow.transform.parent.name);
            MoveArrow(newarrow, moveDirection);
        }
        
    }

    void MoveArrow(GameObject newarrow, Vector3 moveDirection)
    {
        newarrow.GetComponent<Rigidbody2D>().gravityScale = 0f;
        newarrow.GetComponent<Rigidbody2D>().AddForce(moveDirection * arrowSpeed);
        arrowMove = true;
    }


    void CheckKey(string key)
    {
        if (key == newParent.transform.GetChild(0).tag)
        {

            float currentDist = Vector3.Distance(newParent.transform.GetChild(0).transform.position, newParent.transform.position);
            if(currentDist < perfectDist)
            {
                Debug.Log("50");
                points += 50;
            }
            else if(currentDist < goodDist)
            {
                Debug.Log("25");
                points += 25;
            }
            else if(currentDist < badDist)
            {
                Debug.Log("10");
                points += 10;
            }
            else
            {
                Debug.Log("1");
                points += 1;
            }
            if (points >= winPoints)
            {
                Destroy(newParent.transform.GetChild(0).gameObject);
                points = 0;
                QTPlay = false;
                Destroy(newParent);
            }
            else
            {
                Destroy(newParent.transform.GetChild(0).gameObject);
                SpawnArrow();
            }
            
        }
    }


    void Update()
    {
        if (QTPlay)
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


            if(Input.GetKeyDown(up))
            {
                CheckKey("up");
            }
            if(Input.GetKeyDown(down))
            {
                CheckKey("down");
            }
            if (Input.GetKeyDown(left))
            {
                CheckKey("left");
            }
            if (Input.GetKeyDown(right))
            {
                CheckKey("right");
            }

            //todo end of minigame
            
        }

    }

    
}
