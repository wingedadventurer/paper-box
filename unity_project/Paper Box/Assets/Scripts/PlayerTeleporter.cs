using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] private GameObject goPlayer;

    [SerializeField] private Transform[] posPlayerTeleports;

    public static PlayerTeleporter instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        TeleportToRoom(0);
    }

    public void TeleportToRoom(int indexRoom)
    {
        goPlayer.transform.position = posPlayerTeleports[indexRoom].position;
        //goPlayer.transform.rotation = posPlayerTeleports[indexRoom].rotation;
        Physics.SyncTransforms();
    }
}
