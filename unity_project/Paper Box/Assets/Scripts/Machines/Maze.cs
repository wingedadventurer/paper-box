using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public Transform trThing;
    public Transform trRaycast;

    private const int GRID_WIDTH = 18;
    private const int GRID_HEIGHT = 8;
    private const float CELL_SIZE = 0.3f; // [m]

    private Vector2 cellCurrent;
    private int mask;
    private Vector2Int dirQueued;

    void Start()
    {
        cellCurrent = new Vector2(0, 5);
        trThing.localPosition = new Vector3(-cellCurrent.x, 0, -cellCurrent.y) * CELL_SIZE;
        mask = 1 << LayerMask.NameToLayer("MazeWall");
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

    public void MoveUp() { dirQueued = Vector2Int.down; }
    public void MoveDown() { dirQueued = Vector2Int.up; }
    public void MoveLeft() { dirQueued = Vector2Int.left; }
    public void MoveRight() { dirQueued = Vector2Int.right; }

    private void FixedUpdate()
    {
        if (dirQueued != Vector2Int.zero)
        {
            Move(dirQueued);
            dirQueued = Vector2Int.zero;
        }
    }

    public void Move(Vector2Int dir)
    {
        Vector2 cellNew = cellCurrent + dir;

        // prevent moving outside the grid size
        if (cellNew.x >= GRID_WIDTH || cellNew.x < 0 || cellNew.y >= GRID_HEIGHT || cellNew.y < 0) { return; }

        // prevent moving through walls
        Ray ray = new Ray(trRaycast.position, new Vector3(-dir.x, -dir.y, 0));
        RaycastHit hit;
        Physics.Raycast(ray, out hit, CELL_SIZE, mask);
        if (hit.collider) { return; }


        cellCurrent = cellNew;
    }
}
