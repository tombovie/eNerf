using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class swapShoes : MonoBehaviour
{
    public GameObject leftShoe1; // Reference to the new left shoe GameObject prefab
    public GameObject rightShoe1; // Reference to the new right shoe GameObject prefab

    public GameObject leftShoe2;
    public GameObject rightShoe2;

    private GameObject leftShoe;
    private GameObject rightShoe;

    private GameObject currentLeftShoe;
    private GameObject currentRightShoe;

    public GameObject oldLeftShoe; // Reference to the old left shoe GameObject
    public GameObject oldRightShoe; // Reference to the old right shoe GameObject

    public Transform leftFootBone; // Reference to the bone representing the left foot
    public Transform rightFootBone; // Reference to the bone representing the right foot

    public Vector3 leftShoeOffset; // Offset for positioning the left shoe
    public Vector3 rightShoeOffset; // Offset for positioning the right shoe

    public Vector3 leftShoeRotation; // Rotation for the left shoe
    public Vector3 rightShoeRotation; // Rotation for the right shoe

    public Vector3 shoeScale = Vector3.one; // Scale factor for the shoes

    private void Awake()
    {
        // Find all grabbable objects (assuming they have the AdidasScript component)
        var grabbableObjects = FindObjectsOfType<isGrabbed>();
        foreach (var grabbableObject in grabbableObjects)
        {
            grabbableObject.OnGrabbed += OnObjectGrabbed; // Subscribe to the event
        }
    }

    public void SwapShoes()
    {
        // Instantiate and position the left shoe
        currentLeftShoe = Instantiate(leftShoe, leftFootBone);
        currentLeftShoe.transform.localPosition = leftShoeOffset;
        currentLeftShoe.transform.localRotation = Quaternion.Euler(leftShoeRotation);
        currentLeftShoe.transform.localScale = shoeScale;
        currentLeftShoe.transform.SetParent(leftFootBone, false); // Parent to the left foot bone

        // Instantiate and position the right shoe
        currentRightShoe = Instantiate(rightShoe, rightFootBone);
        currentRightShoe.transform.localPosition = rightShoeOffset;
        currentRightShoe.transform.localRotation = Quaternion.Euler(rightShoeRotation);
        currentRightShoe.transform.localScale = shoeScale;
        currentRightShoe.transform.SetParent(rightFootBone, false); // Parent to the right foot bone

        // Disable the old shoes
        if (oldLeftShoe != null)
            oldLeftShoe.SetActive(false);
        if (oldRightShoe != null)
            oldRightShoe.SetActive(false);

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapShoes();
        }
        // Get the list of active XR devices (controllers)
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        // Check for Button.one press on any active controller
        foreach (var device in devices)
        {
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out bool isButtonPressed) && isButtonPressed)
            {
                // Button.one is pressed on this device
                SwapShoes();
                break;
            }
        }
    }

    private void OnObjectGrabbed(isGrabbed grappedObject)
    {
        Debug.Log("The " + grappedObject.gameObject.name + " object was grabbed!");
        if (grappedObject.gameObject.name == "Adidas right dummy bram")
        {
            leftShoe = leftShoe1;
            rightShoe = rightShoe1;
            if (leftShoe != null)
            {

            }
        }
        if (grappedObject.gameObject.name == "Nike air force left rigged")
        {
            leftShoe = leftShoe2;
            rightShoe = rightShoe2;
        }
    }
}

