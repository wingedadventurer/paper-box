using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeCube : MonoBehaviour
{
    [SerializeField] private Animation anim;
    [SerializeField] private AnimationClip[] clips;
    [SerializeField] private DataItem[] datas;
    [SerializeField] private Interactable[] interactables;
    [SerializeField] private GameObject[] bones;
    [SerializeField] private Transform cubeTransform;

    [SerializeField] private float posY;
    [SerializeField] private float offsetYMax;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float floatPeriod;

    private int placedCount;

    private void Start()
    {
        interactables[0].AddListener(delegate { OnInteract(0); });
        interactables[1].AddListener(delegate { OnInteract(1); });
        interactables[2].AddListener(delegate { OnInteract(2); });
        interactables[3].AddListener(delegate { OnInteract(3); });
        interactables[4].AddListener(delegate { OnInteract(4); });
        interactables[5].AddListener(delegate { OnInteract(5); });

        foreach (AnimationClip ac in clips)
        {
            anim.AddClip(ac, ac.name);
        }

        foreach (GameObject bone in bones)
        {
            bone.SetActive(false);
        }
    }

    private void Update()
    {
        // spin and float the cube
        float offsetY = offsetYMax * Mathf.Sin(Time.time * 2 * Mathf.PI / floatPeriod);
        //cubeTransform.localPosition = new Vector3(0, posY + offsetY, 0);
        cubeTransform.localPosition = new Vector3(0, 0, 0);
        cubeTransform.Translate(Vector3.up * (posY + offsetY), Space.World);
        cubeTransform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
    }

    private void OnInteract(int i)
    {
        if (Inventory.instance.GetEquippedItem() == datas[i])
        {
            Inventory.instance.ConsumeEquippedItem();
            interactables[i].gameObject.SetActive(false);
            bones[i].SetActive(true);
            anim.Play(clips[i].name);

            placedCount++;
            if (placedCount == 6)
            {
                Debug.Log("done");
            }
        }
    }
}
