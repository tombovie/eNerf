using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class isGrabbed : MonoBehaviour
{
    public delegate void Grabbed(isGrabbed grappedObject);
    public event Grabbed OnGrabbed;

    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.onSelectEntered.AddListener(OnGrabEnter);
    }

    private void OnDisable()
    {
        grabInteractable.onSelectEntered.RemoveListener(OnGrabEnter);
    }

    private void OnGrabEnter(XRBaseInteractor interactor)
    {
        OnGrabbed?.Invoke(this); // Raise the custom event when grabbed
    }
}
