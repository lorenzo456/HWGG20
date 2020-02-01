using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Speed, Durability, Jump };
    public ItemType itemType = ItemType.Speed;
    string value;
    void Start()
    {
        // Randomly choose an item type
        int t = Mathf.FloorToInt(Random.Range(0, 3));
        if (t == 0)
        {
            itemType = ItemType.Speed;
        }
        else if (t == 1)
        {
            itemType = ItemType.Durability;
        }
        else
        {
            itemType = ItemType.Jump;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Collides with either Player or Player2
        if (collision.gameObject.tag.Contains("Player"))
        {
            // Tell the player that it has collided with this object
            collision.GetComponent<PlayerMovement>().collectedItem(itemType);
            // Remove this object
            Destroy(this.gameObject);
        }
    }


}
