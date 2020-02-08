using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement variables
    public float DEFAULT_SPEED = 10;
    public float JUMP_POWER = 8;
    private Vector2 speed;

    // Reference to the correct car
    public GameObject playerObject;
    private Player player;

    public Item.ItemType item = Item.ItemType.None;
    public GameObject held;
    public Sprite Hspd, Hdur, Hjmp;

    public bool isFacingRight = true;

    // Start is called before the first frame update
    private void Start()
    {
        speed = new Vector2();
        player = playerObject.GetComponent<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!player.isInsideCar)
        {
            // Player has pressed jump
            if (Input.GetKeyDown(player.controller.jump))
            {
                // Check that the player is on the ground 
                if (GetComponent<Rigidbody2D>().IsTouching(GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider2D>()))
                {
                    // Apply upward force (percentage of boost)
                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JUMP_POWER), ForceMode2D.Impulse);
                }
            }

            UpdatePlayerSpeed();

            // Apply the direction
            Vector2 direction = new Vector2();
            direction.x += speed.x * Time.deltaTime;
            transform.Translate(direction);
        }
    }


    private void UpdatePlayerSpeed()
    {
        // Function that updates the players speed from user input
        bool movedRight = false, movedLeft = false;

        if (Input.GetKey(player.controller.accelerate))
        {
            speed.x = DEFAULT_SPEED;
            movedRight = true;
            isFacingRight = true;
        }
        if (Input.GetKey(player.controller.decelerate))
        {
            speed.x = -DEFAULT_SPEED;
            movedLeft = true;
            isFacingRight = false;
        }

        GetComponentInChildren<Animator>().SetBool("inAir", !GetComponent<Rigidbody2D>().IsTouching(GameObject.FindGameObjectWithTag("Ground").GetComponent<Collider2D>()));
        GetComponentInChildren<Animator>().SetBool("isLeft", movedLeft);
        GetComponentInChildren<Animator>().SetBool("isRight", movedRight);
        bool holdingItem = item != Item.ItemType.None;
        GetComponentInChildren<Animator>().SetBool("hasItem", holdingItem);

        if (holdingItem)
        {
            Sprite s;
            if (item.Equals(Item.ItemType.Speed))
            {
                s = Hspd;
            }
            else if (item.Equals(Item.ItemType.Durability))
            {
                s = Hdur;
            }
            else
            {
                s = Hjmp;
            }

            held.GetComponent<SpriteRenderer>().sprite = s;
            held.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            held.GetComponent<SpriteRenderer>().enabled = false;
        }

        // Reset speed when not moving
        if (!(movedLeft || movedRight))
        {
            speed.x = 0;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check the correct vehicle
        if ((collision.gameObject.tag.Equals("Car") && transform.tag.Equals("Player")) || (collision.gameObject.tag.Equals("Car2") && transform.tag.Equals("Player2")))
        {
            if (item != Item.ItemType.None)
            {
                // Display car to upgrade message here
                //print("car can be upgraded");
                if (Input.GetKeyDown(player.controller.interact))
                {
                    player.UpgradeCar(item);
                    item = Item.ItemType.None;
                }
            }
            else
            {
                // display get into car message
                //print("get into car");
            }

            // Get into it
            if (Input.GetKeyDown(player.GetComponent<Player>().controller.toggleCar))
            {
                player.GetIntoCar();
            }
        }
        if (collision.gameObject.CompareTag("Item"))
        {
            if (item == Item.ItemType.None)
            {
                // TODO set hud display visible here
                //Debug.Log("Press " + player.GetComponent<CarStateListener>().interact + " to pick up the scrap");

                if (Input.GetKey(player.controller.interact))
                {
                    // Set item
                    GameObject g = collision.gameObject;
                    //print("Picked up item " + g.GetComponent<Item>());
                    item = g.GetComponent<Item>().itemType;

                    Destroy(collision.gameObject);
                }
            }
            else
            {
                // TODO set hud display visible here
                // Debug.Log("You can't pick this up");
            }
        }

    }



}
