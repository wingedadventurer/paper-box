using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public Animator animator;
    public float radius;

    private Material mat;

    void Start()
    {
        mat = GetComponent<Image>().material;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        mat.SetFloat("_Radius", radius);
    }

    public void SetEquipped(bool value)
    {
        animator.SetBool("equipped", value);
    }

    public void SetCanInteract(bool value)
    {
        animator.SetBool("canInteract", value);
    }

    public void OnInteractSuccess()
    {
        animator.SetTrigger("interactSuccess");
    }

    public void OnInteractFail()
    {
        animator.SetTrigger("interactFail");
    }
}
