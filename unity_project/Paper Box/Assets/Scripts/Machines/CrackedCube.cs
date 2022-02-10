using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackedCube : MonoBehaviour
{
    [SerializeField] private GameObject goCube;
    [SerializeField] private GameObject goPieces;
    [SerializeField] private GameObject prefabCubePiece;
    [SerializeField] private Transform[] cubeSpawns;

    private void Start()
    {
        goPieces.SetActive(false);
    }

    public void Smash()
    {
        goCube.SetActive(false);
        goPieces.SetActive(true);

        foreach (Transform t in cubeSpawns)
        {
            GameObject cube = Instantiate(prefabCubePiece, transform, true);
            cube.transform.position = t.position;
            cube.transform.rotation = t.rotation;
            t.gameObject.SetActive(false);
        }
    }
}
