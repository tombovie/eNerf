using System.Collections;
using System.Collections.Generic;
/*using UnityEditorInternal;*/
using UnityEngine;

public class OnStoreEnter : MonoBehaviour
{
    [SerializeField] private Transform npc;
    [SerializeField] private PlayerInteractUI playerInteractUI;
    [SerializeField] private GameObject startcanvasContainer;

    private Animator npcAnimator;
    private NPCInteractible npcInteractable;
    private AudioSource welcomeAudio;
    private AudioSource doorAudio;

    private void Start()
    {
        npcInteractable = npc.GetComponent<NPCInteractible>();
        welcomeAudio = transform.GetChild(0).GetComponent<AudioSource>();
        doorAudio = transform.GetChild(1).GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(WaitOneSecond_Loop(other.transform));   
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        playerInteractUI.allowedToPlay();
        // Disable the collider so that you receive the welcome only once
        transform.gameObject.SetActive(false);

        HideStartCanvas();
        /*CharacterController characterController = other.GetComponent<CharacterController>();
        characterController.enabled = false;*/
    }

    IEnumerator WaitOneSecond_Loop(Transform player)
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        doorAudio.Play();
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        welcomeAudio.Play();
        npcInteractable.Greet(player, "Hello, welcome to the store!");
        elapsedTime = 0f;
        while (elapsedTime < 5f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        ShowStartCanvas();
    }

    private void ShowStartCanvas()
    {
        startcanvasContainer.SetActive(true);
    }
    private void HideStartCanvas()
    {
        startcanvasContainer.SetActive(false);
    }


}
