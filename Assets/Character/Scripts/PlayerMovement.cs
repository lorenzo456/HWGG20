using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string playerName;
    public float MAX_SPEED = 10;
    public float ACCELERATION = 0.5f;
    public float JUMP_POWER = 5;
    private Vector2 speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3();
        bool hasMoved = false;

        if (Input.GetKey(KeyCode.W))
        {
            speed.x += ACCELERATION;
            if (speed.x > MAX_SPEED)
            {
                speed.x = MAX_SPEED;
            }
            hasMoved = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            speed.x -= ACCELERATION;
            if (speed.x < -MAX_SPEED)
            {
                speed.x = -MAX_SPEED;
            }
            hasMoved = true;
        }

        // Reset speed when not moving
        if (!hasMoved)
        {
            speed.x = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JUMP_POWER), ForceMode2D.Impulse);
        }



        direction.x += speed.x * Time.deltaTime;
        transform.Translate(direction);

    }


    public void collectedItem(string type)
    {
            Debug.Log(playerName + " Has collided with " + type); 
    }



}
