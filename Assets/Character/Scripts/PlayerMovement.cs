using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement variables
    public float DEFAULT_SPEED = 16;
    public float JUMP_POWER = 12;
    private Vector2 speed;

    // Reference to the correct car
    public GameObject player;

    private Item.ItemType item = Item.ItemType.Speed;

    // Start is called before the first frame update
    private void Start()
    {
        speed = new Vector2();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!player.GetComponent<CarStateListener>().isInsideCar)
        {
            // Player has pressed jump
            if (Input.GetKeyDown(player.GetComponent<CarStateListener>().jump))
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

        if (Input.GetKey(player.GetComponent<CarStateListener>().accelerate))
        {
            speed.x = DEFAULT_SPEED;
            movedRight = true;
        }
        if (Input.GetKey(player.GetComponent<CarStateListener>().decelerate))
        {
            speed.x = -DEFAULT_SPEED;
            movedLeft = true;
        }

        // Reset speed when not moving
        if (!(movedLeft || movedRight))
        {
            speed.x = 0;
        }
    }


    public bool CollectedItem(Item.ItemType type)
    {
        if (!player.GetComponent<CarStateListener>().isInsideCar)
        {
            // Called when an item has collided with this player
            //Debug.Log(playerName + " " + player + " has collided with item " + type);

            if (item == Item.ItemType.None)
            {
                // TODO set hud display visible here
                //Debug.Log("Press " + interact + " to pick up the scrap");

                if (Input.GetKey(player.GetComponent<CarStateListener>().interact))
                {
                    item = type;
                    return true;
                }
            }
            else
            {
                // TODO set hud display visible here
                //Debug.Log("You can't pick this up");
            }
        }
        return false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        // Collides with this players vehicle
        if (collision.gameObject.tag.Contains("Car"))
        {
            // Check the correct vehicle
            if ((collision.gameObject.tag.Equals("Car") && transform.tag.Equals("Player")) || (collision.gameObject.tag.Equals("Car2") && transform.tag.Equals("Player2")))
            {
                if (item != Item.ItemType.None)
                {
                    // DIsplay car to upgrade message here
                    //print("car can be upgraded");
                    print(player.GetComponent<CarStateListener>().interact);

                    if (Input.GetKeyDown(player.GetComponent<CarStateListener>().interact))
                    {
                        item = Item.ItemType.None;
                        player.GetComponent<CarStateListener>().UpgradeCar();
                    }
                }
                else
                {
                    // display get into car message
                    //print("get into car");
                }


                // Get into it
                if (Input.GetKeyDown(player.GetComponent<CarStateListener>().toggleCar))
                {
                    player.GetComponent<CarStateListener>().GetIntoCar();
                }
            }
        }
    }




}
