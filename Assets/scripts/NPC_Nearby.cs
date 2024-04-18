using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NPC_Nearby : MonoBehaviour
{
    private float range = 0.9f;
    private bool isHoldingShoe;
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
                        interactible.Buy(transform);
                    }
                }
                break;
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

        // check if the player is holding a shoe and change closestNPC interactText
        if (isHoldingShoe)
        {
            closestNPC.SetInteractText("Do you want to buy \n" + grabbedObject.name);
        }
        else
        {
            closestNPC.SetInteractText("Feel free to \n look around for a shoe");
        }
        return closestNPC;
    }

    private void OnObjectGrabbed(isGrabbed GrabbedObject)
    {
        isHoldingShoe = true;
        grabbedObject = GrabbedObject;
    }

    private void OnObjectRelease(isGrabbed grappedObject)
    {
        isHoldingShoe = false;
        grabbedObject = null;
    }
}
