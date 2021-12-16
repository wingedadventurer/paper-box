using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public DataItem data;

    private Game game;

    private void Awake()
    {
        game = FindObjectOfType<Game>();
    }

    public void OnInteracted()
    {
        game.OnItemCollected(data);
        Destroy(gameObject);
    }
}
