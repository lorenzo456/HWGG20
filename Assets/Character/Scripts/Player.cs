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

    [Space(16)]
    [Range(1, 4)]
    public int playerNumber;
    [HideInInspector]
    public string playerID;

    [Space(16)]
    public bool isInsideCar;

    public class Controller
    {
        public KeyCode accelerate, decelerate, jump, toggleCar, interact;
    }

    [HideInInspector]
    public Controller controller = new Controller();

    private float defaultInteractTimeout = 0.25f;
    private float interactTimeout = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerID = "Player" + playerNumber;
        // Update the player tag and layer to correspond with Player1 to 4
        transform.tag = playerID;
        gameObject.layer = LayerMask.NameToLayer(playerID);

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
        if (transform.tag.Equals("Player1"))
        {
            controller.accelerate = KeyCode.D;
            controller.decelerate = KeyCode.A;
            controller.jump = KeyCode.W;
            controller.toggleCar = KeyCode.Q;
            controller.interact = KeyCode.E;
        }
        else if (transform.tag.Equals("Player2"))
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
        interactTimeout--;

        if (isInsideCar)
        {
            // Get out of car
            if (Input.GetKeyDown(controller.toggleCar))
            {
                getOutCar();
            }

        }

    }

    public void GetIntoCar()
    {
        if(interactTimeout <= 0)
        {
            player.SetActive(false);
            isInsideCar = true;
            carMovement.personInCar = true;

            interactTimeout = defaultInteractTimeout;
        }

    }

    public void getOutCar()
    {
        if(interactTimeout <= 0)
        {
            player.SetActive(true);
            isInsideCar = false;
            carMovement.personInCar = false;

            player.transform.position = car.transform.GetChild(0).position;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            interactTimeout = defaultInteractTimeout;
        }

    }

    public void UpgradeCar(Item.ItemType item)
    {
        // Remove keys
        //accelerate = KeyCode.None; decelerate = KeyCode.None; jump = KeyCode.None; toggleCar = KeyCode.None; interact = KeyCode.None;


        Debug.Log("Starting quick time!");
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
        Debug.Log("Upgraded " + item + " for " + score + " points.");

        SetKeys();
    }
}
