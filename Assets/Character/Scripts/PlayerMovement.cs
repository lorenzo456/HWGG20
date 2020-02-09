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

    private GameObject heldItem;

    // Start is called before the first frame update
    private void Start()
    {
        player = transform.parent.GetComponent<Player>();

        gameObject.layer = LayerMask.NameToLayer(player.playerID);
        // Set all children to the same layer, EXCEPT THE GROUND COLLISION
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform t = transform.GetChild(i);

            if (t.GetComponent<BoxCollider2D>() != null)
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
            // Allow player to drop held item
            if (Input.GetKeyDown(player.controller.interact))
            {
                // Ensure they are not trying to upgrade the car
                if (!GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.NameToLayer("Car" + player.playerNumber)))
                {
                    if (player.IsValidInteractTime())
                    {
                        // Disable for now as prevents from upgrading car
                        //DropItem();
                    }
                }

            }

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
            Vector2 direction = new Vector2(velocityX * Time.deltaTime, 0);
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
        }
        if (Input.GetKey(player.controller.decelerate))
        {
            velocityX = -DEFAULT_SPEED;
            movedLeft = true;
        }

        // Update the player movement animator
        Animator sprite = transform.Find("Sprite").GetComponent<Animator>();
        sprite.SetBool("inAir", !GetComponent<Rigidbody2D>().IsTouching(GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider2D>()));
        sprite.SetBool("isLeft", movedLeft);
        sprite.SetBool("isRight", movedRight);
        // Held item player sprite
        bool isHoldingItem = heldItem != null;
        sprite.SetBool("hasItem", isHoldingItem);

        // Update the held item animator
        int heldItemID = 0;
        if (isHoldingItem)
        {
            Item held = heldItem.GetComponent<Item>();
            if (held.itemType.Equals(Item.ItemType.Speed))
            {
                heldItemID = 1;
            }
            else if (held.itemType.Equals(Item.ItemType.Durability))
            {
                heldItemID = 2;
            }
            else if (held.itemType.Equals(Item.ItemType.Jump))
            {
                heldItemID = 3;
            }
        }
        // Update the held item
        transform.Find("Held Item").GetComponent<Animator>().SetInteger("heldItem", heldItemID);

        // Reset speed when not moving
        if (!(movedLeft || movedRight))
        {
            velocityX = 0;
        }

        return velocityX;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player.IsValidInteractTime())
        {
            // Check the correct vehicle for this player
            if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Car" + player.playerNumber)))
            {
                // Upgrade the car here
                if (heldItem != null)
                {
                    // TODO Display car to upgrade message here

                    if (Input.GetKeyDown(player.controller.interact))
                    {
                        player.UpgradeCar(heldItem.GetComponent<Item>().itemType);
                        Destroy(heldItem.gameObject);
                        // Just to make sure
                        heldItem = null;
                        player.ResetInteractTimeout();
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
                    player.ResetInteractTimeout();
                    return;
                }
            }
            // Check collision with an item
            if (collision.gameObject.CompareTag("Item"))
            {
                // Not holding an item
                if (heldItem == null)
                {
                    // TODO Display press player.controller.interact to pick up item here

                    if (Input.GetKey(player.controller.interact))
                    {
                        // Pick up item
                        PickUpItem(collision.gameObject);
                        return;
                    }
                }
                else
                {
                    // TODO Display you can't pick up this item
                }
            }
        }

    }


    private void PickUpItem(GameObject item)
    {
        if (heldItem == null)
        {
            heldItem = item;
            player.ResetInteractTimeout();
            heldItem.SetActive(false);
        }
    }

    private void DropItem()
    {
        if (heldItem != null)
        {
            // Set held item position to be that of the sprite above the players head
            heldItem.transform.position = transform.Find("Held Item").position;
            heldItem.SetActive(true);
            player.ResetInteractTimeout();
            heldItem = null;
        }
    }

}
