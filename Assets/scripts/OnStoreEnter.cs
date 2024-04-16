using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStoreEnter : MonoBehaviour
{
    private bool start;



    void OnTriggerEnter(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player Entered the Trigger!");
                CharacterController characterController = other.GetComponent<CharacterController>();
                characterController.enabled = false;

                Animator npcAnimator = GameAssets.i.NPC.GetComponent<Animator>();
                npcAnimator.SetTrigger("talking");
            }
        }
    }

    IEnumerator WaitOneSecond_Loop()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Code to execute after waiting for 1 second
        Debug.Log("One second has passed!");
    }
}
