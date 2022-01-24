using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    [SerializeField] private GameObject goPlayer;

    [SerializeField] private Transform[] posPlayerTeleports;

    public bool teleportOnStart;

    public static PlayerTeleporter instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (teleportOnStart)
        {
            TeleportToRoom(0);
        }
    }

    public void TeleportToPosition(Vector3 position)
    {
        goPlayer.transform.position = position;
        Physics.SyncTransforms();
    }

    public void TeleportToRoom(int indexRoom)
    {
        goPlayer.transform.position = posPlayerTeleports[indexRoom].position;
        //goPlayer.transform.rotation = posPlayerTeleports[indexRoom].rotation;
        Physics.SyncTransforms();
    }
}
