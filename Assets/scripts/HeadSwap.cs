using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSwap : MonoBehaviour
{
    private GameObject newHead; // Reference to the new head GameObject prefab

    public GameObject oldHead; // Reference to the old head GameObject

    public Transform neckbone; // Reference to the neck bone

    public Vector3 headOffset; // Offset for positioning the head

    public Vector3 headRotation; // Rotation for the head

    public Vector3 headScale = Vector3.one; // Scale factor for the head

    public void SwapHead()
    {
        //get name from local data
        String currentCharacter = PlayerPrefs.GetString("character");
        //fetch current players head
        newHead = (GameObject)Resources.Load(currentCharacter + "/" + currentCharacter + "_head");

        // Instantiate and position head
        GameObject newnewhead = Instantiate(newHead, neckbone);
        newnewhead.transform.localPosition = headOffset;
        newnewhead.transform.localRotation = Quaternion.Euler(headRotation);
        newnewhead.transform.localScale = headScale;
        newnewhead.transform.SetParent(neckbone, false); 
        newnewhead.layer = 7;


        // Disable the old head
        if (oldHead != null)
            oldHead.SetActive(false);
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwapHead();
        }
    }
}
