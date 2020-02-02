using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarStateListener : MonoBehaviour
{
    public GameObject car;
    public GameObject player;

    public GameObject quickTime;

    public bool isInsideCar;

    public KeyCode accelerate, decelerate, jump, toggleCar, interact;

    public enum PlayerTag { Player, Player2 };
    public PlayerTag playerType;

    bool ignoreGetInCar = false;

    private Car_Movement_Plus c;
    private QuickTime q;
    private Item.ItemType item;

    // Start is called before the first frame update
    void Start()
    {
        isInsideCar = true;
        player.SetActive(false);

        // Update the player tag and layer to correspond with Player or Player2
        transform.tag = playerType.ToString();
        gameObject.layer = LayerMask.NameToLayer(playerType.ToString());

        SetKeys();

        Car_Movement_Plus c = car.GetComponent<Car_Movement_Plus>();
        QuickTime q = quickTime.GetComponent<QuickTime>();
    }

    private void SetKeys()
    {
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
            if (!ignoreGetInCar)
            {
                // Get out of car
                if (Input.GetKeyDown(toggleCar))
                {
                    //print("getting out of car");
                    // Set engine to off

                    player.SetActive(true);
                    player.transform.position = car.transform.GetChild(0).position;
                    player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    isInsideCar = false;

                    car.GetComponent<Car_Movement_Plus>().personInCar = false;
                }
            }
        }
        ignoreGetInCar = false;
    }


    public void GetIntoCar()
    {
        player.SetActive(false);
        isInsideCar = true;
        car.GetComponent<Car_Movement_Plus>().personInCar = true;
        ignoreGetInCar = true;
    }

    public void UpgradeCar(Item.ItemType item)
    {
        this.item = item;
        // Remove keys
        accelerate = KeyCode.None; decelerate = KeyCode.None; jump = KeyCode.None; toggleCar = KeyCode.None; interact = KeyCode.None;


        Debug.Log("Starting quick time!");
        quickTime.transform.position = transform.position;
        quickTime.SetActive(true);
        q.StartGame();
    }

    public void QuickTimeFinished()
    {
        quickTime.SetActive(false);
        int score = 50;

        if (item.Equals(Item.ItemType.Speed))
        {
            c.repairSpeed(score);
        }
        else if (item.Equals(Item.ItemType.Durability))
        {
            c.repairDurabilty(score);
        }
        else if (item.Equals(Item.ItemType.Jump))
        {
            c.repairJump(score);
        }
        else
        {
            Debug.Log("Trying to upgrade a car with no item.");
        }
        Debug.Log("Upgraded " + item + " for " + score + " points.");
        item = Item.ItemType.None;
        SetKeys();
    }
}
