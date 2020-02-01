using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject itemPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnItem();
        }
    }

    public void SpawnItem()
    {
        GameObject g = Instantiate(itemPrefab) as GameObject;
        g.transform.position = FindRandomPointOnGround();

        // Check if item is touching another item, and if so move this one somewhere else 

        g.transform.SetParent(transform.Find("Items"));
    }

    public Vector2 FindRandomPointOnGround()
    {
        return new Vector2(Random.Range(0, 50), 10);
    }
}
