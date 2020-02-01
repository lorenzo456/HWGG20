using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Movement : MonoBehaviour
{
    public float acceleration = 3.0f;
    public float jump = 40.0f;
    public float MaxSpeed = 15.0f;
    private float jumpHeight = 0.0f;
    private Rigidbody2D rigidBody;
    private Vector2 velocity;
    private Collider2D carCollider;
    bool touchingGround = false;
    bool personInCar = true;

    KeyCode left;
    KeyCode right;
    KeyCode up;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        carCollider = gameObject.GetComponent<CircleCollider2D>();
        velocity = rigidBody.velocity;

        if (player2)
        {
            left = KeyCode.A;
            right = KeyCode.D;
            up = KeyCode.W;
        }
        else
        {
            left = KeyCode.LeftArrow;
            right = KeyCode.RightArrow;
            up = KeyCode.UpArrow;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (carCollider.IsTouching(GameObject.FindGameObjectWithTag("Ground").GetComponent<EdgeCollider2D>()))
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

            if (Input.GetKey(right) && !Input.GetKey(left) && horizontalVelocity <= MaxSpeed)
            {
                Vector2 horizontalForce = new Vector2(2.0f, 0.0f);
                rigidBody.AddForce(horizontalForce * acceleration);
            }

            if (!Input.GetKey(right) && Input.GetKey(left) && horizontalVelocity >= -MaxSpeed)
            {
                Vector2 horizontalForce = new Vector2(-2.0f, 0.0f);
                rigidBody.AddForce(horizontalForce * acceleration);
            }
        }
    }

    void keyJump()
    {
        float verticalVelocity = velocity.y;
        if (Input.GetKey(KeyCode.UpArrow) && touchingGround)
        {
            jumpHeight++;
            if (jumpHeight >= 80)
            {
                jumpHeight = 80;
            }
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && touchingGround)
        {
            Vector2 horizontalForce = new Vector2(0.0f, (jump + (jump*0.2f*jumpHeight)));
            rigidBody.AddForce(horizontalForce);
        }
        else if (touchingGround)
        {
            jumpHeight = 0;
        }
    }
}
