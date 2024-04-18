using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractible : MonoBehaviour
{
    [SerializeField] private string interactText;
    [SerializeField] private PlayerInteractUI playerInteractUI;
    [SerializeField] private AudioSource cashRegister;
    Animator animator;
    private NPCHeadLookAt npcHeadLookAt;

    private string interactUIText;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        npcHeadLookAt = GetComponent<NPCHeadLookAt>();
    }

    public void Greet(Transform InteractingPerson, string text)
    {
        //playerInteractUI.HideWhileTalking();
        ChatBubble.Create(transform.transform, new Vector3(0f, 1.9f, -0.2f), InteractingPerson, text);
        //Debug.Log("Interact!");
        animator.SetTrigger("Greeting");

        float personHeight = 0.018f;
        npcHeadLookAt.lookAtPosition(InteractingPerson.position + Vector3.up * personHeight);
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
    public void Buy(Transform InteractingPerson)
    {
        if (cashRegister != null) { cashRegister.Play();  }
        playerInteractUI.HideWhileTalking();
        ChatBubble.Create(transform.transform, new Vector3(0f, 1.9f, -0.2f), InteractingPerson, "Thank you for buying the shoe!");
        //Debug.Log("Interact!");
        animator.SetTrigger("Talking");

        float personHeight = 0.018f;
        npcHeadLookAt.lookAtPosition(InteractingPerson.position + Vector3.up * personHeight);

        //StartCoroutine(EndGame());
    }

    public string GetInteractText()
    {
        return interactText;    
    }
    public void SetInteractText(string t)
    {
        interactText = t;
    }

    IEnumerator EndGame()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 5f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        SceneTransitionManager.singleton.GoToScene(2);
    }

}
