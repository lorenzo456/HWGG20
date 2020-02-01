using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Movement : MonoBehaviour
{
    public float speed = 3.0f;
    public float jump = 80.0f;
    public float MaxSpeed = 15.0f;
    private int jumpHeight = 0;
    private Rigidbody2D rigidBody;
    private Vector2 velocity;
    private Collider2D carCollider;
    bool touchingGround = false;
    bool personInCar = true;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        carCollider = gameObject.GetComponent<Collider2D>();
        velocity = rigidBody.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (carCollider.IsTouching(GameObject.Find("groundofGOD").GetComponent<Collider2D>()))
        {
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }

        if (personInCar)
        {
            keyMovement();
            keyJump();
        }
    }

    void keyMovement()
    {
        float horizontalVelocity = velocity.x;
        if (touchingGround)
        {

            if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && horizontalVelocity <= MaxSpeed)
            {
                Vector2 horizontalForce = new Vector2(2.0f, 0.0f);
                rigidBody.AddForce(horizontalForce * speed);
            }

            if (!Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow) && horizontalVelocity >= -MaxSpeed)
            {
                Vector2 horizontalForce = new Vector2(-2.0f, 0.0f);
                rigidBody.AddForce(horizontalForce * speed);
            }
        }
    }

    void keyJump()
    {
        if (touchingGround)
        {
            jumpHeight = 0;

        }
        else if(jumpHeight <= 21)
        {
            jumpHeight++;
        }

        float verticalVelocity = velocity.y;
        if (Input.GetKey(KeyCode.UpArrow) && verticalVelocity <= 7*jump && jumpHeight <= 20 )
        {
            Vector2 horizontalForce = new Vector2(0.0f, jump);
            rigidBody.AddForce(horizontalForce);
        }
    }
}
