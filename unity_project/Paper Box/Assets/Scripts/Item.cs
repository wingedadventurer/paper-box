using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public DataItem data;

    public void OnInteracted()
    {
        Game.instance.OnItemCollected(data); // TODO: replace with inventory AddItem()
        Destroy(gameObject);
    }
}
