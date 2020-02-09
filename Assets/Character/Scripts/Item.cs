using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Speed, Durability, Jump, None };
    public ItemType itemType = ItemType.None;

    public Sprite spee, jmp, dur;
    private string ID = "Item";

    private void Start()
    {
        transform.tag = ID;
        gameObject.layer = LayerMask.NameToLayer(ID);
        transform.Find("Sprite").gameObject.layer = LayerMask.NameToLayer("OnlyGround");

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
        transform.Find("Sprite").GetComponent<SpriteRenderer>().sprite = s;
    }

}
