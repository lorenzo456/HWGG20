﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car_Movement_Plus : MonoBehaviour
{

    float statDurability = 1.0f;
    float statSpeed = 1.0f;
    float statJump = 1.0f;



    public enum KeyMovement { up, left, right };
    public KeyMovement key;

    KeyCode right;
    KeyCode left;
    KeyCode up;

    public float speed = 500.0f;
    public float maxJumpHeight = 40.0f;
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

    [Header("Unity stuff")]
    public Image BlueBar;
    public Image GreenBar;
    public Image RedBar;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponentInChildren<Rigidbody2D>();
        jointMotors = gameObject.GetComponentsInChildren<WheelJoint2D>();
        wheelMotor1 = jointMotors[0];
        wheelMotor2 = jointMotors[1];
        wheelCollider = gameObject.GetComponentInChildren<CircleCollider2D>();
        velocity = rigidBody.velocity;

        if (!player2)
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

    //Fill the UI bars for speed jump & durability
    
    //durability, did not see anything in this script that caused durability to go down? 
    void BlueBarFill()
    {
        BlueBar.fillAmount = statDurability;
    }

    //speed or fuel
    void RedBarFill()
    {
        RedBar.fillAmount = statSpeed;
    }

    // Jump
    void GreenBarFill()
    {
        GreenBar.fillAmount = statJump;
    }


    // Update is called once per frame
    void Update()
    {
        if (wheelCollider.IsTouching(GameObject.Find("collider").GetComponent<Collider2D>()))
        {
            touchingGround = true;
        }
        else
        {
            touchingGround = false;
        }

        if (personInCar)
        {
            Movement();
            Jump();
        }
        else
        {
            wheelMotor1.motor = NewMotor(0);
            wheelMotor2.motor = NewMotor(0);
        }

        ResetPosition();
    }

    void Movement()
    {

        if (Input.GetKey(right) && !Input.GetKey(left))
        {
            wheelMotor1.motor = NewMotor((-0.5f * speed) + -(statSpeed * speed));
            wheelMotor2.motor = NewMotor((-0.5f * speed) + -(statSpeed * speed));
            statSpeed = statSpeed - 0.05f * Time.deltaTime;
        }

        else if (!Input.GetKey(right) && Input.GetKey(left))
        {
            wheelMotor1.motor = NewMotor((0.5f * speed) + (statSpeed * speed));
            wheelMotor2.motor = NewMotor((0.5f * speed) + (statSpeed * speed));
            statSpeed = statSpeed - 0.05f * Time.deltaTime;
            if (statSpeed < 0)
            {
                statSpeed = 0;
            }
        }
        else
        {
            wheelMotor1.motor = NewMotor(0);
            wheelMotor2.motor = NewMotor(0);
        }
        RedBarFill();
    }

    void Jump()
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
        if (Input.GetKey(up) && verticalVelocity <= (7 * (statJump * maxJumpHeight)) && jumpHeight <= 20)
        {
            Vector2 horizontalForce = new Vector2(0.0f, (statJump * maxJumpHeight));
            rigidBody.AddForce(horizontalForce);
            if (jumpHeight == 1)
            {
                statJump = statJump - 0.05f;
            }
            if (statJump <= 0.0f)
            {
                statJump = 0.0f;
            }
        }
        GreenBarFill();
    }

    JointMotor2D NewMotor(float mSpeed)
    {
        JointMotor2D MyNewMotor = new JointMotor2D();
        MyNewMotor.motorSpeed = mSpeed;
        MyNewMotor.maxMotorTorque = 10000;

        return MyNewMotor;
    }

    void repairJump(int value)
    {
        statJump = statJump + value;

        if (statJump > 1)
        {
            statJump = 1.0f;
        }

        GreenBarFill();
    }

    void repairDurabilty(int value)
    {
        statDurability = statDurability + value;

        if (statDurability > 1)
        {
            statDurability = 1.0f;
        }

        BlueBarFill();

    }

    void repairSpeed(int value)
    {
        statSpeed = statSpeed + value;

        if (statSpeed > 1)
        {
            statSpeed = 1.0f;
        }

        RedBar.fillAmount = statSpeed;
    }

    void ResetPosition()
    {
        GameObject body = gameObject.transform.GetChild(0).gameObject;
        if (body.transform.eulerAngles.z >= 45 && body.transform.eulerAngles.z <= 90)
        {
            body.transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, body.transform.eulerAngles.y, 45.0f);
        }
        /*else if (body.transform.eulerAngles.z >= 315 && body.transform.eulerAngles.z <= 360)
        {
            body.transform.eulerAngles = new Vector3(body.transform.eulerAngles.x, body.transform.eulerAngles.y, 315.0f);
        }*/
    }
}
