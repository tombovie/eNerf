using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSwap : MonoBehaviour
{
    private GameObject newHead; // Reference to the new left shoe GameObject prefab

    public GameObject oldHead; // Reference to the old left shoe GameObject

    public Transform neckbone; // Reference to the bone representing the left foot

    public Vector3 headOffset; // Offset for positioning the left shoe

    public Vector3 headRotation; // Rotation for the left shoe

    public Vector3 headScale = Vector3.one; // Scale factor for the shoes

    public void SwapShoes()
    {
        //get name from local data
        String currentCharacter = PlayerPrefs.GetString("character");
        //fetch current players head
        newHead = (GameObject) Resources.Load(currentCharacter + "/" + currentCharacter + "_head");
        // Instantiate and position the left shoe
        GameObject newnewhead = Instantiate(newHead, neckbone);
        newnewhead.transform.localPosition = headOffset;
        newnewhead.transform.localRotation = Quaternion.Euler(headRotation);
        newnewhead.transform.localScale = headScale;
        newnewhead.transform.SetParent(neckbone, false); // Parent to the left foot bone
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
