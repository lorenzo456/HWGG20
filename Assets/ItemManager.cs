using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnItem(new Vector2(Random.Range(-50, 50), -5));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnItem(Vector2 position)
    {
        GameObject g = Instantiate(itemPrefab) as GameObject;
        g.transform.position = position;

        g.transform.SetParent(transform.Find("Items"));
    }
}
