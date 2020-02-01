using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStateListener : MonoBehaviour
{
    public GameObject car;
    public GameObject player;

    public bool isInsideCar;

    public KeyCode accelerate, decelerate, jump, toggleCar, interact;

    public enum PlayerTag { Player, Player2 };
    public PlayerTag playerType;

    // Start is called before the first frame update
    void Start()
    {
        isInsideCar = true;
        player.SetActive(false);

        // Update the player tag and layer to correspond with Player or Player2
        transform.tag = playerType.ToString();
        gameObject.layer = LayerMask.NameToLayer(playerType.ToString());

        // Set the player key controls
        if (playerType == PlayerTag.Player)
        {
            accelerate = KeyCode.D;
            decelerate = KeyCode.A;
            jump = KeyCode.W;
            toggleCar = KeyCode.Q;
            interact = KeyCode.E;
        }
        else
        {
            accelerate = KeyCode.RightArrow;
            decelerate = KeyCode.LeftArrow;
            jump = KeyCode.UpArrow;
            toggleCar = KeyCode.I;
            interact = KeyCode.O;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInsideCar)
        {
            // Get out of car
            if (Input.GetKeyDown(toggleCar))
            {
                // Set engine to off

                player.SetActive(true);
                player.GetComponent<PlayerMovement>().GetOutOfCar();
                isInsideCar = false;
            }
        }
        else
        {
            // Get in car
            if (Input.GetKeyDown(toggleCar))
            {
                // Set engine to on

                player.SetActive(false);
                isInsideCar = true;
            }
        }
    }




    public void UpgradeCar()
    {
        // Check player and car collision first
        if(player.GetComponent<BoxCollider2D>().IsTouching(car.GetComponent<CapsuleCollider2D>()))
        {
            // TODO
            print("press to upgrade");

            if (Input.GetKeyDown(player.GetComponent<CarStateListener>().interact))
            {
                print("CAr should be upgraded");

            }

        }

    }
}
