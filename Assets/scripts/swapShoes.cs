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

    public GameObject leftShoe3;
    public GameObject rightShoe3;

    private GameObject leftShoe;
    private GameObject rightShoe;

    private GameObject currentLeftShoe;
    private GameObject currentRightShoe;

    private GameObject currentShoePrefab;
    private Vector3 currentShoePrefabPosition;
    private Quaternion currentShoePrefabRotation;




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
        // For testing:
        /*leftShoe = leftShoe1;
        rightShoe = rightShoe1;*/

        // Working:
        if (currentLeftShoe != null)
            oldLeftShoe = currentLeftShoe;
        if (currentRightShoe != null)
            oldRightShoe = currentRightShoe;

        // Disable the old shoes
        if (oldLeftShoe != null && leftShoe != null)
            //oldLeftShoe.SetActive(false);
            Destroy(oldLeftShoe);
        if (oldRightShoe != null && rightShoe != null)
            //oldRightShoe.SetActive(false); 
            Destroy(oldRightShoe);

        // Instantiate and position the left shoe
        if (leftShoe != null)
        {
            currentLeftShoe = Instantiate(leftShoe, leftFootBone);
            currentLeftShoe.transform.localPosition = leftShoeOffset;
            currentLeftShoe.transform.localRotation = Quaternion.Euler(leftShoeRotation);
            currentLeftShoe.transform.localScale = shoeScale;
            currentLeftShoe.transform.SetParent(leftFootBone, false); // Parent to the left foot bone
        }
        
        // Instantiate and position the right shoe
        if (rightShoe != null)
        {
            currentRightShoe = Instantiate(rightShoe, rightFootBone);
            currentRightShoe.transform.localPosition = rightShoeOffset;
            currentRightShoe.transform.localRotation = Quaternion.Euler(rightShoeRotation);
            currentRightShoe.transform.localScale = shoeScale;
            currentRightShoe.transform.SetParent(rightFootBone, false); // Parent to the right foot bone
        }
    }

    private void HideCurrentShoePrefab()
    {
        if (currentShoePrefab != null) { currentShoePrefab.SetActive(false); }
    }

    private void ActivateCurrentShoePrefab()
    {
        if (currentShoePrefab != null) {
            currentShoePrefab.transform.position = currentShoePrefabPosition;
            currentShoePrefab.transform.rotation = currentShoePrefabRotation;
            currentShoePrefab.SetActive(true);
        }
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
                
                HideCurrentShoePrefab();
                break;
            }
        }
    }

    private void OnObjectGrabbed(isGrabbed grappedObject)
    {
        if (currentShoePrefab != null && currentShoePrefab.activeSelf == false)
        {
            ActivateCurrentShoePrefab();
        }
        currentShoePrefab = grappedObject.gameObject;
        currentShoePrefabPosition = grappedObject.transform.position;
        currentShoePrefabRotation = grappedObject.transform.rotation;   
        

        
        Debug.Log("The " + grappedObject.gameObject.name + " object was grabbed!");
        if (grappedObject.gameObject.name == "Adidas right dummy bram")
        {
            leftShoe = leftShoe1;
            rightShoe = rightShoe1;
            /*currentShoePrefabPosition = new Vector3(3.08200002f, 0.959999979f, -3.97600007f);
            currentShoePrefabRotation = new Quaternion(0f, 0f, 0f, 1f);*/
        }
        if (grappedObject.gameObject.name == "Nike air force left rigged")
        {
            leftShoe = leftShoe2;
            rightShoe = rightShoe2;
            /*currentShoePrefabPosition = new Vector3(2.28499985f, 1.15900004f, -3.7869997f);
            currentShoePrefabRotation = new Quaternion(0f, 0.539707065f, 0f, 0.841852903f);*/
        }
        if (grappedObject.gameObject.name == "Nike air max right dummy")
        {
            leftShoe = leftShoe3;
            rightShoe = rightShoe3;
            /*currentShoePrefabPosition = new Vector3(1.29199982f, 1.02199996f, -3.59317207f);
            currentShoePrefabRotation = new Quaternion(0f, 0.275808901f, 0, 0.961212456f);*/
        }     
    }
}

