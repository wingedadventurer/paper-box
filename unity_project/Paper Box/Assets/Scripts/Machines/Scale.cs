using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    const float TILT_MAX = 30;

    public Interactable interactablePlate;
    public Transform transformRod;
    public Transform transformRopesLeft, transformRopesRight;
    public DataItem itemWeight;
    public AnimationClip acContainerOpen;
    public Animation anim;

    public GameObject[] weights;

    private int weightsCollected = 0;

    private void Start()
    {
        UpdateStuff();
        interactablePlate.AddListener(AddWeight);

        anim.AddClip(acContainerOpen, acContainerOpen.name);
    }

    public void AddWeight()
    {
        if (Inventory.instance.GetEquippedItem() == itemWeight)
        {
            Inventory.instance.ConsumeEquippedItem();
            weightsCollected++;
            UpdateStuff();
            if (weightsCollected == 5)
            {
                interactablePlate.gameObject.SetActive(false);

                anim.Play(acContainerOpen.name);
            }
        }
    }

    private void UpdateStuff()
    {
        for (int i = 0; i < 5; i++)
        {
            weights[i].SetActive(weightsCollected > i);
        }
        transformRod.localEulerAngles = new Vector3(90, -TILT_MAX + (TILT_MAX / 5.0f) * weightsCollected, 0);
        Vector3 v = new Vector3(-180, 0, 0);
        transformRopesLeft.eulerAngles = v;
        transformRopesRight.eulerAngles = v;
    }
}
