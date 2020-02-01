using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Movement : MonoBehaviour
{
    public enum KeyMovement { up, left, right};
    public KeyMovement key;

    KeyCode right;
    KeyCode left;
    KeyCode up;

    public float acceleration = 3.0f;
    public float maxJumpHeight = 80.0f;
    public float MaxSpeed = 15.0f;
    public bool player2;

    private int jumpHeight = 0;
    private Rigidbody2D rigidBody;
    private Vector2 velocity;
    private Collider2D carCollider;
    bool touchingGround = false;
    public bool personInCar = true;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        carCollider = gameObject.GetComponent<Collider2D>();
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
        /*
        if (carCollider.IsTouching(GameObject.FindGameObjectWithTag("Ground").GetComponent<EdgeCollider2D>()))
        {
            touchingGround = true;
<<<<<<< HEAD
            //Debug.Log("TOUCHING GROUND");
=======
            Debug.Log("Touching Ground!");
>>>>>>> origin/CarMovement
        }
        else
        {
            touchingGround = false;
<<<<<<< HEAD
            //Debug.Log("NOT TOUCHING GROUND");

=======
>>>>>>> origin/CarMovement
        }

        if (personInCar)
        {
            keyMovement();
            keyJump();
        }
        */
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
        if (touchingGround)
        {
            jumpHeight = 0;

        }
        else if(jumpHeight <= 21)
        {
            jumpHeight++;
        }

        float verticalVelocity = velocity.y;
        if (Input.GetKey(up) && verticalVelocity <= 7*maxJumpHeight && jumpHeight <= 20 )
        {
            Vector2 horizontalForce = new Vector2(0.0f, maxJumpHeight);
            rigidBody.AddForce(horizontalForce);
        }
    }
}
