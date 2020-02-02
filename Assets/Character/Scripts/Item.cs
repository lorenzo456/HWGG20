using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Speed, Durability, Jump, None };
    public ItemType itemType = ItemType.None;

    public Sprite spee, jmp, dur;
    void Start()
    {
        // Randomly choose an item type
        int t = Mathf.FloorToInt(Random.Range(0, 3));

        // Set colour (just for now)
        Sprite s;
        if (t == 0)
        {
            itemType = ItemType.Speed;
            s = spee;
        }
        else if (t == 1)
        {
            itemType = ItemType.Durability;
            s = dur;
        }
        else
        {
            itemType = ItemType.Jump;
            s = jmp;
        }

        // Set sprite
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = s;
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        print("item colliding with " + collision.tag);
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
    */


}
