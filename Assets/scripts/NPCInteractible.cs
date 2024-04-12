using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractible : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        Debug.Log("Interact!");
        animator.SetTrigger("isTalking");
    }
}
