using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public Transform trThing;

    private const int GRID_WIDTH = 18;
    private const int GRID_HEIGHT = 8;
    private const float CELL_SIZE = 0.003f;

    private Vector2 cellCurrent;
    private Vector2 cellTarget;

    void Start()
    {
        cellCurrent = new Vector2(0, 5);
        cellTarget = cellCurrent;
        trThing.localPosition = new Vector3(-cellCurrent.x, 0, -cellCurrent.y) * CELL_SIZE;
    }

    void Update()
    {
        // debug move with keys
        if (Input.GetKeyDown(KeyCode.I)) { MoveUp(); }
        if (Input.GetKeyDown(KeyCode.K)) { MoveDown(); }
        if (Input.GetKeyDown(KeyCode.J)) { MoveLeft(); }
        if (Input.GetKeyDown(KeyCode.L)) { MoveRight(); }

        trThing.localPosition = Vector3.Lerp(trThing.localPosition, new Vector3(-cellCurrent.x, 0, -cellCurrent.y) * CELL_SIZE, 0.15f);
    }

    public void MoveUp() { Move(Vector2Int.down); }
    public void MoveDown() { Move(Vector2Int.up); }
    public void MoveLeft() { Move(Vector2Int.left); }
    public void MoveRight() { Move(Vector2Int.right); }

    public void Move(Vector2Int dir)
    {
        Vector2 cellNew = cellCurrent + dir;

        // TODO: verify if move is valid
        if (cellNew.x >= GRID_WIDTH || cellNew.x < 0 || cellNew.y >= GRID_HEIGHT || cellNew.y < 0) { return; }

        cellCurrent = cellNew;
        //cellTarget = cellNew;
    }
}
