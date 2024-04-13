using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Nearby : MonoBehaviour
{
    private float range = 0.7f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, range);
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out NPCInteractible interactible))
                {
                    interactible.Interact(transform);
                }
            }
        }
    }

    public NPCInteractible GetInteractibleObject()
    {
        List<NPCInteractible> NPCInteractibleList = new List<NPCInteractible>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out NPCInteractible interactible))
            {
                NPCInteractibleList.Add(interactible);
            }
        }

        NPCInteractible closestNPC = null;
        foreach(NPCInteractible interactible in NPCInteractibleList)
        {
            if (closestNPC == null)
            {
                closestNPC = interactible;
            }
            else
            {
                if (Vector3.Distance(transform.position, interactible.transform.position) < 
                    Vector3.Distance(transform.position, closestNPC.transform.position))
                {
                    //Closer
                    closestNPC = interactible;
                }
            }
        }

        return closestNPC;
    }
}
