using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Item : MonoBehaviour
{
    public DataItem data;

    private void Awake()
    {
        GetComponent<Interactable>().AddListener(OnInteracted);
    }

    public void OnInteracted()
    {
        Game.instance.OnItemCollected(data);
        Destroy(gameObject);
    }
}
