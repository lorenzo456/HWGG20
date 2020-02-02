using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject itemPrefab;

    public int totalItems = 10;
    public int minX = 0, maxX = 20;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < totalItems; i++)
        {
            SpawnItem();
        }
    }

    private void SpawnItem()
    {
        GameObject g = Instantiate(itemPrefab) as GameObject;
        g.transform.position = new Vector2(Random.Range(minX, maxX), 5);
        g.SetActive(true);

        // Check if item is touching another item, and if so move this one somewhere else 

        g.transform.SetParent(transform.Find("Items"));
    }

}
