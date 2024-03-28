using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSwap : MonoBehaviour
{
    public GameObject Head; // Reference to the new left shoe GameObject prefab

    public GameObject oldHead; // Reference to the old left shoe GameObject

    public Transform bone; // Reference to the bone representing the left foot

    public Vector3 leftShoeOffset; // Offset for positioning the left shoe
    public Vector3 rightShoeOffset; // Offset for positioning the right shoe

    public Vector3 leftShoeRotation; // Rotation for the left shoe
    public Vector3 rightShoeRotation; // Rotation for the right shoe

    public Vector3 shoeScale = Vector3.one; // Scale factor for the shoes

    public void SwapShoes()
    {
        // Instantiate and position the left shoe
        GameObject newnewhead = Instantiate(Head, bone);
        newnewhead.transform.localPosition = leftShoeOffset;
        newnewhead.transform.localRotation = Quaternion.Euler(leftShoeRotation);
        newnewhead.transform.localScale = shoeScale;
        newnewhead.transform.SetParent(bone, false); // Parent to the left foot bone
        newnewhead.layer = 7;


        // Disable the old shoes
        if (oldHead != null)
            oldHead.SetActive(false);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapShoes();
        }
    }
}
