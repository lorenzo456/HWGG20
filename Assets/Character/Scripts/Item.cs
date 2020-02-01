using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType { Speed, Durability, Jump};
    public ItemType itemType = ItemType.Speed;
    string value;
    void Start()
    {
        if(itemType == ItemType.Speed)
        {
            value = "Speed";
        }else if(itemType == ItemType.Jump)
        {
            value = "Jump";
        }else if(itemType == ItemType.Durability)
        {
            value = "Durability";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Collided with " + this );
            collision.GetComponent<PlayerMovement>().collectedItem(value);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
