using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject player;
    [HideInInspector]
    public PlayerMovement playerMovement;
    public GameObject car;
    [HideInInspector]
    public Car_Movement_Plus carMovement;

    public bool isInsideCar;

    public class Controller
    {
        public KeyCode accelerate, decelerate, jump, toggleCar, interact;
    }

    [HideInInspector]
    public Controller controller = new Controller();

    public enum PlayerTag { Player, Player2 };
    public PlayerTag playerType;

    bool ignoreGetInCar = false;

    private Item.ItemType item;

    // Start is called before the first frame update
    void Start()
    {
        // Update the player tag and layer to correspond with Player or Player2
        transform.tag = playerType.ToString();
        gameObject.layer = LayerMask.NameToLayer(playerType.ToString());

        playerMovement = player.GetComponent<PlayerMovement>();
        carMovement = car.GetComponent<Car_Movement_Plus>();

        GetIntoCar();

        SetKeys();
        //q.OnQuickTimeFinished += QuickTimeFinished;
    }





    private void OnDestroy()
    {
        //q.OnQuickTimeFinished -= QuickTimeFinished;
    }

    private void SetKeys()
    {
        // Set the player key controls
        if (playerType == PlayerTag.Player)
        {
            controller.accelerate = KeyCode.D;
            controller.decelerate = KeyCode.A;
            controller.jump = KeyCode.W;
            controller.toggleCar = KeyCode.Q;
            controller.interact = KeyCode.E;
        }
        else
        {
            controller.accelerate = KeyCode.RightArrow;
            controller.decelerate = KeyCode.LeftArrow;
            controller.jump = KeyCode.UpArrow;
            controller.toggleCar = KeyCode.I;
            controller.interact = KeyCode.O;
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
                if (Input.GetKeyDown(controller.toggleCar))
                {
                    //print("getting out of car");
                    // Set engine to off
                    getOutCar();
                }
            }
        }
        ignoreGetInCar = false;
    }


    public void GetIntoCar()
    {
        player.SetActive(false);
        isInsideCar = true;
        carMovement.personInCar = true;
        ignoreGetInCar = true;
    }

    public void getOutCar()
    {
        player.SetActive(true);
        player.transform.position = car.transform.GetChild(0).position;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        isInsideCar = false;

        carMovement.personInCar = false;
    }

    public void UpgradeCar(Item.ItemType item)
    {
        this.item = item;
        // Remove keys
        //accelerate = KeyCode.None; decelerate = KeyCode.None; jump = KeyCode.None; toggleCar = KeyCode.None; interact = KeyCode.None;


        Debug.Log("Starting quick time!");
        //quickTime.transform.position = transform.position;
        //quickTime.SetActive(true);
        //q.StartGame();

        QuickTimeFinished();
    }

    public void QuickTimeFinished()
    {
        //quickTime.SetActive(false);
        float score = 0.5f;

        if (item.Equals(Item.ItemType.Speed))
        {
            //c.repairSpeed(score);
        }
        else if (item.Equals(Item.ItemType.Durability))
        {
            //c.repairDurabilty(score);
        }
        else if (item.Equals(Item.ItemType.Jump))
        {
            //c.repairJump(score);
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
