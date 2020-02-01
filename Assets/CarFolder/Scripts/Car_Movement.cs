using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Movement : MonoBehaviour
{
    public float speed = 3.0f;
    public float jump = 20.0f;
    public float MaxSpeed = 15.0f;
    private Rigidbody2D rigidBody;
    private Vector2 velocity;
    bool personInCar = true;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        velocity = rigidBody.velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (personInCar)
        {
            keyMovement();
            keyJump();
        }
    }

    void keyMovement()
    {
        float horizontalVelocity = velocity.x;

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

    void keyJump()
    {
        float verticalVelocity = velocity.y;
        if (Input.GetKey(KeyCode.UpArrow) && verticalVelocity <= 3*jump)
        {
            Vector2 horizontalForce = new Vector2(0.0f, jump);
            rigidBody.AddForce(horizontalForce);
        }
    }
}
