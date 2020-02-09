using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject player;
    [HideInInspector]
    public PlayerMovement playerMovement;
    private GameObject car;
    [HideInInspector]
    public Car_Movement_Plus carMovement;

    [Range(1, 4)]
    public int playerNumber;
    [HideInInspector]
    public string playerID;

    [Space(10)]
    public bool isInsideCar;

    public Controller controller;

    public class Controller
    {
        public KeyCode accelerate, decelerate, jump, flipCar, toggleCar, interact;
        private string player;

        public Controller(string player)
        {
            this.player = player;

            SetKeys();
        }

        public void SetKeys()
        {
            // Set the player key controls
            if (player.Equals("Player1"))
            {
                accelerate = KeyCode.D;
                decelerate = KeyCode.A;
                jump = KeyCode.W;
                flipCar = KeyCode.S;
                toggleCar = KeyCode.Q;
                interact = KeyCode.E;
            }
            else if (player.Equals("Player2"))
            {
                accelerate = KeyCode.RightArrow;
                decelerate = KeyCode.LeftArrow;
                jump = KeyCode.UpArrow;
                flipCar = KeyCode.DownArrow;
                toggleCar = KeyCode.I;
                interact = KeyCode.O;
            }
        }

        public void DisableKeys()
        {
            accelerate = KeyCode.None;
            decelerate = KeyCode.None;
            jump = KeyCode.None;
            flipCar = KeyCode.None;
            toggleCar = KeyCode.None;
            interact = KeyCode.None;
        }
    }

    // Default interaction timeout 30 ticks
    private int defaultInteractTimeout = 8;
    private int interactTimeout = 0;


    // TODO improve controls and add controller support
    // Create players by instantiation (allow 4 players)
    // Fix player animations
    // Allow player to drop item?



    // Must use awake to allow other GameObjects to access the tag of this object as awake is done first
    private void Awake()
    {
        playerID = "Player" + playerNumber;
        // Update the player tag and layer to correspond with Player1 to 4
        transform.tag = playerID;
        gameObject.layer = LayerMask.NameToLayer(playerID);

        // FInd and assign the movement and vehicle
        player = transform.Find("Player").gameObject;
        playerMovement = player.GetComponent<PlayerMovement>();
        car = transform.Find("CAR").gameObject;
        carMovement = car.GetComponent<Car_Movement_Plus>();

        GetIntoCar();

        controller = new Controller(playerID);
        //q.OnQuickTimeFinished += QuickTimeFinished;
    }



    private void OnDestroy()
    {
        //q.OnQuickTimeFinished -= QuickTimeFinished;
    }




    // Update is called once per frame
    private void Update()
    {
        if (interactTimeout > 0)
        {
            interactTimeout--;
        }

        if (isInsideCar)
        {
            // Get out of car
            if (Input.GetKeyDown(controller.toggleCar))
            {
                GetOutCar();
            }

        }

    }

    public void GetIntoCar()
    {
        if (IsValidInteractTime())
        {
            ResetInteractTimeout();

            player.SetActive(false);
            isInsideCar = true;
            carMovement.personInCar = true;
        }

    }

    public void GetOutCar()
    {
        if (IsValidInteractTime())
        {
            ResetInteractTimeout();

            player.SetActive(true);
            isInsideCar = false;
            carMovement.personInCar = false;

            player.transform.position = car.transform.GetChild(0).position;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

    }

    public void UpgradeCar(Item.ItemType item)
    {
        // Remove keys
        //controller.DisableKeys();


        //Debug.Log("Starting quick time!");
        //quickTime.transform.position = transform.position;
        //quickTime.SetActive(true);
        //q.StartGame();

        QuickTimeFinished(item);
    }

    public void QuickTimeFinished(Item.ItemType item)
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
        //Debug.Log("Upgraded " + item + " for " + score + " points.");

        //controller.SetKeys();
    }



    public bool IsValidInteractTime()
    {
        return interactTimeout <= 0;
    }

    public void ResetInteractTimeout()
    {
        interactTimeout = defaultInteractTimeout;
    }
}
