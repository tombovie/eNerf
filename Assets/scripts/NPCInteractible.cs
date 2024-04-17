using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractible : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private PlayerInteractUI playerInteractUI;
    Animator animator;
    private NPCHeadLookAt npcHeadLookAt;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }

    public void Interact(Transform InteractingPerson, string text)
    {
        playerInteractUI.HideWhileTalking();
        ChatBubble.Create(transform.transform, new Vector3(0f, 1.9f, -0.2f), InteractingPerson, text);
        //Debug.Log("Interact!");
        animator.SetTrigger("Talking");

        float personHeight = 0.018f;
        npcHeadLookAt.lookAtPosition(InteractingPerson.position + Vector3.up * personHeight);
    }

    public void Greet(Transform InteractingPerson, string text)
    {
        playerInteractUI.HideWhileTalking();
        ChatBubble.Create(transform.transform, new Vector3(0f, 1.9f, -0.2f), InteractingPerson, text);
        //Debug.Log("Interact!");
        animator.SetTrigger("Greeting");

        float personHeight = 0.018f;
        npcHeadLookAt.lookAtPosition(InteractingPerson.position + Vector3.up * personHeight);
    }

    public string GetInteractText()
    {
        return interactText;    
    }
}
