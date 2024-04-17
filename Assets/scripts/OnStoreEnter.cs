using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStoreEnter : MonoBehaviour
{
    [SerializeField] private Transform npc;
    private Animator npcAnimator;
    private NPCInteractible npcInteractable;

    private void Start()
    {
        npcInteractable = npc.GetComponent<NPCInteractible>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player Entered the Trigger!");
                CharacterController characterController = other.GetComponent<CharacterController>();
                characterController.enabled = false;

                StartCoroutine(WaitOneSecond_Loop(other.transform));
                
            }
        }
    }

    IEnumerator WaitOneSecond_Loop(Transform player)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        npcInteractable.Interact(player, "Hello, welcome to the store!");
    }
}
