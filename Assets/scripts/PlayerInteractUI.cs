using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private NPC_Nearby npc_nearby;
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;

    private bool talking = false;

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
        containerGameObject.SetActive(true);
        interactTextMeshProUGUI.text = npcInteractible.GetInteractText();
    }

    private void Hide()
    {
        containerGameObject.SetActive(false);
    }

    // Call the coroutine from another function or within Start/Update
    public void HideWhileTalking()
    {
        StartCoroutine(HideForSeconds(containerGameObject));
    }
    private IEnumerator HideForSeconds(GameObject containerGameObject, float duration = 5f)
    {
        talking = true;
        containerGameObject.SetActive(false); // Hide the GameObject

        yield return new WaitForSeconds(duration); // Wait for 5 seconds

        containerGameObject.SetActive(true); // Show the GameObject again
        talking = false;
    }

    

}
