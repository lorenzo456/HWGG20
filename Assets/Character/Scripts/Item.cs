using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Speed, Durability, Jump };
    public ItemType itemType = ItemType.Speed;
    void Start()
    {
        // Randomly choose an item type
        int t = Mathf.FloorToInt(Random.Range(0, 3));
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

        GetComponent<SpriteRenderer>().material.color = c;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Collides with either Player or Player2
        if (collision.gameObject.tag.Contains("Player"))
        {
            // Tell the player that it has collided with this object
            collision.GetComponent<PlayerMovement>().CollectedItem(itemType);
            // Remove this object
            Destroy(this.gameObject);
        }
    }


}
