using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NPC_Nearby : MonoBehaviour
{
    private float range = 1.1f;

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
                    interactible.Interact(transform, "Thank you for buying!");
                }
            }
        }

        //Check if vr button B is pressed
        // Get the list of active XR devices (controllers)
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        // Check for Button.one press on any active controller
        foreach (var device in devices)
        {
            if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isButtonPressed) && isButtonPressed)
            {
                // Button.one is pressed on this device -> We swap the shoes with the new shoes
                Collider[] colliders = Physics.OverlapSphere(transform.position, range);
                foreach (Collider collider in colliders)
                {
                    if (collider.TryGetComponent(out NPCInteractible interactible))
                    {
                        if (interactible.GetAction().Equals("Talk"))
                        {
                            interactible.Talk(transform);
                        }
                        else if (interactible.GetAction().Equals("Buy"))
                        {
                            interactible.Buy(transform);
                        }
                        else { Debug.Log("Not a valid action");  }
                    }
                }
                break;
            }
        }
    }

    // Checks if there is an ncp nearby and if there are multiple, it chooses the closest
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
