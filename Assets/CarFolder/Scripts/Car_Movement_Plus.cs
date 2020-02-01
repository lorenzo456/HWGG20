using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Movement_Plus : MonoBehaviour
{
    public enum KeyMovement { up, left, right };
    public KeyMovement key;

    KeyCode right;
    KeyCode left;
    KeyCode up;

    public float speed = 500.0f;
    public float maxJumpHeight = 20.0f;
    public bool player2;
    private int jumpHeight = 0;

    private Rigidbody2D rigidBody;
    private Vector2 velocity;
    private Collider2D wheelCollider;
    private WheelJoint2D[] jointMotors;
    private WheelJoint2D wheelMotor1;
    private WheelJoint2D wheelMotor2;

    bool touchingGround = false;
    public bool personInCar = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponentInChildren<Rigidbody2D>();
        jointMotors = gameObject.GetComponentsInChildren<WheelJoint2D>();
        wheelMotor1 = jointMotors[0];
        wheelMotor2 = jointMotors[1];
        wheelCollider = gameObject.GetComponentInChildren<CircleCollider2D>();
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
        if (wheelCollider.IsTouching(GameObject.FindGameObjectWithTag("Ground").GetComponent<EdgeCollider2D>()))
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
        if (touchingGround)
        {

            if (Input.GetKey(right) && !Input.GetKey(left))
            {
                wheelMotor1.useMotor = true;
                wheelMotor2.useMotor = true;
                JointMotor2D MyNewMotor = new JointMotor2D();
                MyNewMotor.motorSpeed = -speed;
                MyNewMotor.maxMotorTorque = 10000;

                wheelMotor1.motor = MyNewMotor;
                wheelMotor2.motor = MyNewMotor;
            }

            if (!Input.GetKey(right) && Input.GetKey(left))
            {
                wheelMotor1.useMotor = true;
                wheelMotor2.useMotor = true;
                JointMotor2D MyNewMotor = new JointMotor2D();
                MyNewMotor.motorSpeed = speed;
                MyNewMotor.maxMotorTorque = 10000;

                wheelMotor1.motor = MyNewMotor;
                wheelMotor2.motor = MyNewMotor;
            }
        }
    }

    void keyJump()
    {
        if (touchingGround)
        {
            jumpHeight = 0;

        }
        else if (jumpHeight <= 21)
        {
            jumpHeight++;
        }

        float verticalVelocity = velocity.y;
        if (Input.GetKey(up) && verticalVelocity <= 7 * maxJumpHeight && jumpHeight <= 20)
        {
            Vector2 horizontalForce = new Vector2(0.0f, maxJumpHeight);
            rigidBody.AddForce(horizontalForce);
        }
    }
}
