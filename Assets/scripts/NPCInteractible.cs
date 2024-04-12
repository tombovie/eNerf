using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractible : MonoBehaviour
{
    Animator animator;
    private NPCHeadLookAt npcHeadLookAt;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }

    public void Interact(Transform InteractingPerson)
    {
        ChatBubble.Create(transform.transform, new Vector3(0.4f, 1.7f, 0f), InteractingPerson, "hello my friend");
        //Debug.Log("Interact!");
        animator.SetTrigger("Talking");

        float personHeight = 1.8f;
        npcHeadLookAt.lookAtPosition(InteractingPerson.position + Vector3.up * personHeight);
    }
}
