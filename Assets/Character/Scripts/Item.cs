using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Speed, Durability, Jump, None };
    public ItemType itemType = ItemType.Speed;
    void Start()
    {
        // Randomly choose an item type
        int t = Mathf.FloorToInt(Random.Range(0, 3));

        // Set colour (just for now)
        Color c;
        if (t == 0)
        {
            itemType = ItemType.Speed;
            c = Color.green;
        }
        else if (t == 1)
        {
            itemType = ItemType.Durability;
            c = Color.red;
        }
        else
        {
            itemType = ItemType.Jump;
            c = Color.blue;
        }

        transform.GetChild(0).GetComponent<SpriteRenderer>().material.color = c;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Collides with either Player or Player2
        if (collision.gameObject.tag.Contains("Player"))
        {
            // Check if the player that it has collided with this object and if the player has space to pick it up
            if (collision.GetComponent<PlayerMovement>().CollectedItem(itemType))
            {
                // Remove this object
                Destroy(gameObject);
            }

        }
    }


}
