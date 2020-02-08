using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement variables
    public float DEFAULT_SPEED = 10;
    public float JUMP_POWER = 8;

    // Reference to the player script for controls etc
    private Player player;

    [Space(16)]
    public Item.ItemType held = Item.ItemType.None;

    public bool isFacingRight = true;

    // Start is called before the first frame update
    private void Start()
    {
        player = transform.parent.GetComponent<Player>();

        gameObject.layer = LayerMask.NameToLayer(player.playerID);

        // Set all children to the same layer, EXCEPT THE GROUND COLLISION
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);

            if(t.GetComponent<BoxCollider2D>() != null)
            {
                // We have found the sprite 
                // This is the ground collision 
                t.gameObject.layer = LayerMask.NameToLayer("OnlyGround");
            }
            else
            {
                t.gameObject.layer = LayerMask.NameToLayer(player.playerID);
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!player.isInsideCar)
        {
            // Player has pressed jump
            if (Input.GetKeyDown(player.controller.jump))
            {
                // Check that the player is on the ground 
                if (GetComponent<Rigidbody2D>().IsTouching(GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider2D>()))
                {
                    // Apply upward force 
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JUMP_POWER), ForceMode2D.Impulse);
                }
            }

            // Move the player
            float velocityX = GetVelocityX();
            Vector2 direction = new Vector2();
            direction.x += velocityX * Time.deltaTime;
            transform.Translate(direction);
        }
    }


    private float GetVelocityX()
    {
        // Function that updates the players speed from user input
        bool movedRight = false, movedLeft = false;
        float velocityX = 0;

        if (Input.GetKey(player.controller.accelerate))
        {
            velocityX = DEFAULT_SPEED;
            movedRight = true;
            isFacingRight = true;
        }
        if (Input.GetKey(player.controller.decelerate))
        {
            velocityX = -DEFAULT_SPEED;
            movedLeft = true;
            isFacingRight = false;
        }

        // Update the player sprite 
        Animator sprite = GetComponentInChildren<Animator>();
        sprite.SetBool("inAir", !GetComponent<Rigidbody2D>().IsTouching(GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider2D>()));
        sprite.SetBool("isLeft", movedLeft);
        sprite.SetBool("isRight", movedRight);

        bool isHoldingItem = held != Item.ItemType.None;
        sprite.SetBool("hasItem", isHoldingItem);

        int heldItem = 0;
        if (isHoldingItem)
        {
            if (held.Equals(Item.ItemType.Speed))
            {
                heldItem = 1;
            }
            else if (held.Equals(Item.ItemType.Durability))
            {
                heldItem = 2;
            }
            else if (held.Equals(Item.ItemType.Jump))
            {
                heldItem = 3;
            }
        }
        sprite.SetInteger("heldItem", heldItem);

        // Reset speed when not moving
        if (!(movedLeft || movedRight))
        {
            velocityX = 0;
        }

        return velocityX;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check the correct vehicle
        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Car" + player.playerNumber)))
        {
            // Upgrade the car here
            if (held != Item.ItemType.None)
            {
                // TODO Display car to upgrade message here

                if (Input.GetKeyDown(player.controller.interact))
                {
                    player.UpgradeCar(held);
                    held = Item.ItemType.None;
                    return;
                }
            }
            else
            {
                // TODO Display get into car message here
            }

            // Get into it
            if (Input.GetKeyDown(player.GetComponent<Player>().controller.toggleCar))
            {
                player.GetIntoCar();
                return;
            }
        }
        // Check collision with an item
        if (collision.gameObject.CompareTag("Item"))
        {
            // Not holding an item
            if (held == Item.ItemType.None)
            {
                // TODO Display press player.controller.interact to pick up item here

                if (Input.GetKey(player.controller.interact))
                {
                    // Pick up item and delete it off the ground
                    held = collision.gameObject.GetComponent<Item>().itemType;
                    Destroy(collision.gameObject);
                }
            }
            else
            {
                // TODO Display you can't pick up this item
            }
        }

    }



}
