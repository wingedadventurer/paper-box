using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Image imageWithMaterial;

    [HideInInspector] public float radius;

    private Material material;

    private void Awake()
    {
        material = imageWithMaterial.material;
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
        animator.SetTrigger("interact");
    }
}
