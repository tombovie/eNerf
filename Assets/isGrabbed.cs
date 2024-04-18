using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class isGrabbed : MonoBehaviour
{
    public delegate void Grabbed(isGrabbed grappedObject);
    public event Grabbed OnGrabbed;
    public event Grabbed OnRelease;
    public bool grabbed;

    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.onSelectEntered.AddListener(OnGrabEnter);
        grabInteractable.onSelectExited.AddListener(OnGrabExit);
    }

    private void OnDisable()
    {
        grabInteractable.onSelectEntered.RemoveListener(OnGrabEnter);
        grabInteractable.onSelectExited.RemoveListener(OnGrabExit);
    }

    private void OnGrabEnter(XRBaseInteractor interactor)
    {
        OnGrabbed?.Invoke(this); // Raise the custom event when grabbed
        grabbed = true;
    }

    private void OnGrabExit(XRBaseInteractor interactor) // No changes here
    {
        OnRelease?.Invoke(this); // Raise the custom event when grab exits
        grabbed = false;
    }
}
