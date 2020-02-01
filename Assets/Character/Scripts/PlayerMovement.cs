using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement variables
    public float DEFAULT_SPEED = 16;
    public float JUMP_POWER = 12;
    private Vector2 speed;

    [Space(16)]
    public const int MAX_BOOST = 200;
    [Range(0, MAX_BOOST)]
    public int speedBoost;
    [Range(0, MAX_BOOST)]
    public int durabilityBoost;
    [Range(0, MAX_BOOST)]
    public int jumpBoost;
    public int BOOST_UPGRADE = 20;

    [Space(16)]
    public string playerName;

    public enum PlayerTag { Player, Player2 };
    public PlayerTag player;

    private KeyCode accelerate, decelerate, jump;

    // Start is called before the first frame update
    private void Start()
    {
        speed = new Vector2();

        // Update the player tag to correspond with p1 or p2
        transform.tag = player.ToString();
        gameObject.layer = LayerMask.NameToLayer(player.ToString());

        // Set the player key controls
        if (player == PlayerTag.Player)
        {
            accelerate = KeyCode.D;
            decelerate = KeyCode.A;
            jump = KeyCode.W;
        }
        else
        {
            accelerate = KeyCode.RightArrow;
            decelerate = KeyCode.LeftArrow;
            jump = KeyCode.UpArrow;
        }

        speedBoost = 100;
        durabilityBoost = 100;
        jumpBoost = 100;
    }

    // Update is called once per frame
    private void Update()
    {
        // Player has pressed jump
        if (Input.GetKeyDown(jump))
        {
            // TODO Need to check that the player is on the ground 

            transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JUMP_POWER * ((float)jumpBoost / (float)MAX_BOOST)), ForceMode2D.Impulse);
        }

        UpdatePlayerSpeed();

        Vector2 direction = new Vector2();
        direction.x += speed.x * Time.deltaTime;
        transform.Translate(direction);
    }



    private void UpdatePlayerSpeed()
    {
        // Function that updates the players speed from user input
        bool movedRight = false, movedLeft = false;

        if (Input.GetKey(accelerate))
        {
            speed.x = DEFAULT_SPEED * ((float)speedBoost / (float)MAX_BOOST);
            movedRight = true;
        }
        if (Input.GetKey(decelerate))
        {
            speed.x = -DEFAULT_SPEED * ((float)speedBoost / (float)MAX_BOOST);
            movedLeft = true;
        }

        // Reset speed when not moving
        if (!(movedLeft || movedRight))
        {
            speed.x = 0;
        }
    }


    public void CollectedItem(Item.ItemType type)
    {
        Debug.Log(playerName + " Has collided with item " + type);

        if (type == Item.ItemType.Speed)
        {
            speedBoost += BOOST_UPGRADE;
        }
        else if (type == Item.ItemType.Durability)
        {
            durabilityBoost += BOOST_UPGRADE;
        }
        else
        {
            jumpBoost += BOOST_UPGRADE;
        }
    }



}
