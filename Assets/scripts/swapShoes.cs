using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class swapShoes : MonoBehaviour
{
    private AudioSource fitShoeAudio;


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

    private GameObject previousShoePrefab;
    private Vector3 previousShoePrefabPosition;
    private Quaternion previousShoePrefabRotation;




    public GameObject oldLeftShoe; // Reference to the old left shoe GameObject
    public GameObject oldRightShoe; // Reference to the old right shoe GameObject

    public Transform leftFootBone; // Reference to the bone representing the left foot
    public Transform rightFootBone; // Reference to the bone representing the right foot

    public Vector3 leftShoeOffset; // Offset for positioning the left shoe
    public Vector3 rightShoeOffset; // Offset for positioning the right shoe

    public Vector3 leftShoeRotation; // Rotation for the left shoe
    public Vector3 rightShoeRotation; // Rotation for the right shoe

    public Vector3 shoeScale = Vector3.one; // Scale factor for the shoes


    private isGrabbed grabbedObject;
    public bool shoeInHand = false;


    private void Start()
    {
        if (transform.parent != null)
        {
            if (transform.parent.TryGetComponent<AudioSource>(out fitShoeAudio)) { }
        }   
    }

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

    public void SwapShoes()
    {
        /*// For testing:
        leftShoe = leftShoe1;
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

    private void ActivatePreviousShoePrefab()
    {
        if (previousShoePrefab != null)
        {
            previousShoePrefab.transform.position = previousShoePrefabPosition;
            previousShoePrefab.transform.rotation = previousShoePrefabRotation;
            previousShoePrefab.SetActive(true);
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
                if (shoeInHand)
                {
                    Vector3 position = new Vector3(0f, 0f, 0f);
                    quaternion rotation = new Quaternion(0, 0, 0, 1); ;
                    // Check which shoe your holding and assign the correct prefabs that will be swapped
                    Debug.Log("The " + grabbedObject.gameObject.name + " object was grabbed!");
                    if (grabbedObject.gameObject.name == "Adidas right dummy bram")
                    {
                        leftShoe = leftShoe1;
                        rightShoe = rightShoe1;
                        position = new Vector3(1.76579142f, 0.959999979f, -3.89100003f);
                        rotation = new Quaternion(0, 0, 0, 1);
                    }
                    if (grabbedObject.gameObject.name == "Nike air force left rigged")
                    {
                        leftShoe = leftShoe2;
                        rightShoe = rightShoe2;
                        position = new Vector3(0.864791393f, 1.15900004f, -3.77900004f);
                        rotation = new Quaternion(0, 0.539707065f, 0, 0.841852903f);
                    }
                    if (grabbedObject.gameObject.name == "Nike air max right dummy")
                    {
                        leftShoe = leftShoe3;
                        rightShoe = rightShoe3;
                        position = new Vector3(0.737791419f, 1.02199996f, -4.40100002f);
                        rotation = new Quaternion(0, 0.275808901f, 0, 0.961212456f);
                    }



                    // On the first grab, we assign the previousPrefab to the same as the currentPrefab
                    if (previousShoePrefab == null)
                    {
                        previousShoePrefab = grabbedObject.gameObject;
                        previousShoePrefabPosition = position;
                        previousShoePrefabRotation = rotation;
                    }
                    // After the first grab, we move currentPrefab to the previous,
                    // so that we can activate the previous prefab again if the current in swapped
                    else
                    {
                        previousShoePrefab = currentShoePrefab;
                        previousShoePrefabPosition = currentShoePrefabPosition;
                        previousShoePrefabRotation = currentShoePrefabRotation;
                    }
                    // The current prefab is the object we grabbed and we store it's position and rotation, so that we
                    // Can put it back on it's original place after fitting a new shoe
                    currentShoePrefab = grabbedObject.gameObject;
                    currentShoePrefabPosition = position;
                    currentShoePrefabRotation = rotation;



                    




                    // Button.one is pressed on this device -> We swap the shoes with the new shoes
                    SwapShoes();
                    if (fitShoeAudio != null) { fitShoeAudio.Play(); }

                    // We bring back the previous shoe that we temporarally disabled
                    if (currentShoePrefab != null && currentShoePrefab.activeSelf == false)
                    {
                        ActivatePreviousShoePrefab();
                    }
                    // The current shoe prefab we swapped will be temporarally disabled
                    HideCurrentShoePrefab();

                    //You have no longer a shoe in your hand
                    shoeInHand = false;
                }
                
                break;
            }
        }
    }

    private void OnObjectGrabbed(isGrabbed GrabbedObject)
    {
        shoeInHand = true;
        grabbedObject = GrabbedObject;
    }

    private void OnObjectRelease(isGrabbed grappedObject)
    {
        shoeInHand = false;
        grabbedObject = null;
    }
}

