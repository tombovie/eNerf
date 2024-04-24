using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private AudioSource interactAudio;
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private GameObject containerPriceTagUI; 
    [SerializeField] private NPC_Nearby npc_nearby;
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;

    private bool talking = false;
    private bool hasPlayedAudio = false;
    private bool isAllowedToPlay = false;

    private isGrabbed grabbedObject;

    
    private void Awake()
    {
        // Find all grabbable objects (assuming they have the AdidasScript component)
        var grabbableObjects = FindObjectsOfType<isGrabbed>();
        foreach (var grabbableObject in grabbableObjects)
        {
            grabbableObject.OnGrabbed += OnObjectGrabbed; // Subscribe to the event
            grabbableObject.OnRelease += OnObjectRelease; // Subscribe to release event
        }
    }

    private void Update()
    {
        if (!talking)
        {
            if (npc_nearby.GetInteractibleObject() != null)
            {
                Show(npc_nearby.GetInteractibleObject());
            }
            else
            {
                Hide();
            }
        }
    }

    private void Show(NPCInteractible npcInteractible)
    {
        //On the OnEnterStore, the interact UI should not yet play
        // Once the player left the collider of the OnStoreEnter, the interact UI can be used when the player comes close to the NPC
        if (isAllowedToPlay)
        {
            if (interactAudio != null & hasPlayedAudio == false) { interactAudio.Play(); }
            containerGameObject.SetActive(true);
            // If the player is holding a shoe
            if (grabbedObject != null)
            {
                interactTextMeshProUGUI.text = "\nDo you want to buy\n" + grabbedObject.name;
                containerPriceTagUI.SetActive(true);
            }
            //if not
            else
            {
                interactTextMeshProUGUI.text = "Go away";
            }
            //interactTextMeshProUGUI.text = npcInteractible.GetInteractText();
            hasPlayedAudio = true;
        }
    }

    private void Hide()
    {
        containerGameObject.SetActive(false);
        containerPriceTagUI.SetActive(false);
        hasPlayedAudio = false;
    }

    // Call the coroutine from another function or within Start/Update
    public void HideWhileTalking()
    {
        StartCoroutine(HideForSeconds(containerGameObject));
    }
    private IEnumerator HideForSeconds(GameObject containerGameObject, float duration = 5.5f)
    {
        talking = true;
        containerGameObject.SetActive(false); // Hide the GameObject

        yield return new WaitForSeconds(duration); // Wait for 5 seconds

        containerGameObject.SetActive(true); // Show the GameObject again
        talking = false;
    }

    public void allowedToPlay()
    {
        isAllowedToPlay = true;
    }

    private void OnObjectGrabbed(isGrabbed GrabbedObject)
    {
        grabbedObject = GrabbedObject;
    }

    private void OnObjectRelease(isGrabbed grappedObject)
    {
        grabbedObject = null;
    }

    public void SetNPC_Nearby(NPC_Nearby script)
    {
        npc_nearby = script;
    }
}
