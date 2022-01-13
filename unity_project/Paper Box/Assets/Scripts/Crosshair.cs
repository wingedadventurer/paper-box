using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [HideInInspector] public float radius;

    private Animator animator;
    private Material material;

    private void Awake()
    {
        material = GetComponent<Image>().material;
        animator = GetComponent<Animator>();

        Interacting interacting = Interacting.instance;
        interacting.Entered.AddListener( delegate { SetCanInteract(true); } );
        interacting.Exited.AddListener( delegate { SetCanInteract(false); } );
        interacting.Interacted.AddListener(OnInteract);
        
        // TODO: connect events from Inventory item equipped and unequipped to SetEquipped()
    }

    void Update()
    {
        material.SetFloat("_Radius", radius);
    }

    public void SetEquipped(bool value)
    {
        animator.SetBool("equipped", value);
    }

    public void SetCanInteract(bool value)
    {
        animator.SetBool("canInteract", value);
    }

    public void OnInteract()
    {
        animator.SetTrigger("interactFail");
    }
}
