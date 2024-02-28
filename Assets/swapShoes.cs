using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class swapShoes : MonoBehaviour
{
    public GameObject leftShoe; // Reference to the new left shoe GameObject
    public GameObject rightShoe; // Reference to the new right shoe GameObject

    public GameObject oldLeftShoe; // Reference to the old left shoe GameObject
    public GameObject oldRightShoe; // Reference to the old right shoe GameObject

    public Transform leftFootBone; // Reference to the bone representing the left foot
    public Transform rightFootBone; // Reference to the bone representing the right foot

    public Vector3 leftShoeOffset; // Offset for positioning the left shoe
    public Vector3 rightShoeOffset; // Offset for positioning the right shoe

    public Vector3 leftShoeRotation; // Rotation for the left shoe
    public Vector3 rightShoeRotation; // Rotation for the right shoe

    public Vector3 shoeScale = Vector3.one; // Scale factor for the shoes

    public void SwapShoes()
    {
        // Instantiate and position the left shoe
        GameObject newLeftShoe = Instantiate(leftShoe, leftFootBone);
        newLeftShoe.transform.localPosition = leftShoeOffset;
        newLeftShoe.transform.localRotation = Quaternion.Euler(leftShoeRotation);
        newLeftShoe.transform.localScale = shoeScale;

        // Instantiate and position the right shoe
        GameObject newRightShoe = Instantiate(rightShoe, rightFootBone);
        newRightShoe.transform.localPosition = rightShoeOffset;
        newRightShoe.transform.localRotation = Quaternion.Euler(rightShoeRotation);
        newRightShoe.transform.localScale = shoeScale;

        SkinnedMeshRenderer skinnedMeshRenderer = oldLeftShoe.GetComponent<SkinnedMeshRenderer>();
        SkinnedMeshRenderer skinnedMeshRenderer2 = oldRightShoe.GetComponent<SkinnedMeshRenderer>();
        skinnedMeshRenderer.enabled = false;
        skinnedMeshRenderer2.enabled = false;

        // Remove the old shoes
        //Destroy(oldLeftShoe);
        //Destroy(oldRightShoe);
    }

    /*public GameObject leftShoe; // Reference to the new left shoe GameObject
    public GameObject rightShoe; // Reference to the new right shoe GameObject

    public Transform leftFootBone; // Reference to the bone representing the left foot
    public Transform rightFootBone; // Reference to the bone representing the right foot

    public void SwapShoes()
    {
        // Instantiate and position the left shoe
        GameObject newLeftShoe = Instantiate(leftShoe, leftFootBone);
        CopyTransform(leftFootBone.GetChild(0), newLeftShoe.transform);

        // Instantiate and position the right shoe
        GameObject newRightShoe = Instantiate(rightShoe, rightFootBone);
        CopyTransform(rightFootBone.GetChild(0), newRightShoe.transform);
    }

    // Function to copy the position and rotation of one transform to another
    private void CopyTransform(Transform source, Transform destination)
    {
        destination.localPosition = source.localPosition;
        destination.localRotation = source.localRotation;
    }*/

    private int count = 0;
    private void Update()
    {
        count++;
        if (count > 200)
        {
            SwapShoes();
            count = 0;
        }
    }
}
