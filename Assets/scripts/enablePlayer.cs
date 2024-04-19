using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enablePlayer : MonoBehaviour
{

    private int bodyTypeIndex, bodyColorIndex;
    private GameObject currentBodyType;
    public GameObject spawnPoint;
    public List<Material> skinColorMaterials;
    public GameObject head_target, right_target, left_target;

    // Start is called before the first frame update
    void Start()
    {
        //fetch local data from startscene
        bodyTypeIndex = PlayerPrefs.GetInt("bodytype");
        bodyColorIndex = PlayerPrefs.GetInt("bodycolor");

        switch (bodyTypeIndex) {
            case 0:
                currentBodyType = (GameObject) Instantiate(Resources.Load("Basic_vrouw/vrouw_slank"), spawnPoint.transform.position, spawnPoint.transform.rotation, transform);
                Debug.Log("body inserted into the scene!");
                break;
            case 1:
                currentBodyType = (GameObject)Instantiate(Resources.Load("Basic_vrouw/vrouw_medium_dik"), spawnPoint.transform.position, spawnPoint.transform.rotation, transform);
                Debug.Log("body inserted into the scene!");
                break;
            case 2:
                currentBodyType = (GameObject)Instantiate(Resources.Load("Basic_vrouw/vrouw_dik"), spawnPoint.transform.position, spawnPoint.transform.rotation, transform);
                Debug.Log("body inserted into the scene!");
                break;
            case 3:
                currentBodyType = (GameObject)Instantiate(Resources.Load("Basic_man/man_slank"), spawnPoint.transform.position, spawnPoint.transform.rotation, transform);
                Debug.Log("body inserted into the scene!");
                break;
            case 4:
                currentBodyType = (GameObject)Instantiate(Resources.Load("Basic_man/man_medium_dik"), spawnPoint.transform.position, spawnPoint.transform.rotation, transform);
                Debug.Log("body inserted into the scene!");
                break;
            case 5:
                currentBodyType = (GameObject)Instantiate(Resources.Load("Basic_man/man_dik"), spawnPoint.transform.position, spawnPoint.transform.rotation, transform);
                Debug.Log("body inserted into the scene!");
                break;
 
            default:
                //default character?

                break;


        }
        //assign VR targets
        AssignTargets();

        //after bodytypes has been instantiated, assign skincolor
        AssignSkinColor();
        
    }

    private void AssignTargets()
    {
        currentBodyType.GetComponent<buildVRInteraction>().setHeadTarget(head_target);
        currentBodyType.GetComponent<buildVRInteraction>().setLeftHandTarget(left_target);
        currentBodyType.GetComponent<buildVRInteraction>().setRightHandTarget(right_target);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AssignSkinColor() 
    {

        //asign skincolor to head
        Transform head = currentBodyType.transform.Find("ch_head");
        //set on correct layer (Layer 7: Head)
        head.gameObject.layer = 7;

        if ( head != null) 
        {
            //Debug.Log("head object found");
            //get materials
            List<Material> materials_head = new List<Material>();
            head.GetComponent<SkinnedMeshRenderer>().GetMaterials(materials_head);
            //create temp material
            Material tempMaterial = null;
            foreach (Material mat in materials_head)
            {
                //Debug.Log("material name: " + mat.name);
                if (mat.name == "skincolor_head (Instance)") 
                {
                    //Debug.Log("skincolor_head material found in material list!");
                    tempMaterial = mat;
                    
                }
            }

            if (tempMaterial != null)
            {
                int index = materials_head.IndexOf(tempMaterial);
                materials_head.Insert(index, skinColorMaterials[bodyColorIndex]);
                materials_head.Remove(tempMaterial);
            }

            head.GetComponent<SkinnedMeshRenderer>().SetMaterials(materials_head);
            //Debug.Log("Material list is of size: " + materials_head.Count + " with new element added :)");
        }

        //assign skincolor to body
        Transform body = currentBodyType.transform.Find("Body");
        if (body != null)
        {
            //Debug.Log("head object found");
            //get materials
            List<Material> materials_body = new List<Material>();
            body.GetComponent<SkinnedMeshRenderer>().GetMaterials(materials_body);
            //create temp material
            Material tempMaterial = null;
            foreach (Material mat in materials_body)
            {
                //Debug.Log("material name: " + mat.name);
                if (mat.name == "skincolor_body (Instance)")
                {
                    //Debug.Log("skincolor_head material found in material list!");
                    tempMaterial = mat;

                }
            }

            if (tempMaterial != null)
            {
                int index = materials_body.IndexOf(tempMaterial);
                materials_body.Insert(index, skinColorMaterials[bodyColorIndex]);
                materials_body.Remove(tempMaterial);
            }

            body.GetComponent<SkinnedMeshRenderer>().SetMaterials(materials_body);
            //Debug.Log("Material list is of size: " + materials_head.Count + " with new element added :)");
        }

        //assign skincolor to Arms
        Transform arms = currentBodyType.transform.Find("Arms");
        if (arms != null)
        {
            //Debug.Log("head object found");
            //get materials
            List<Material> materials_arms = new List<Material>();
            arms.GetComponent<SkinnedMeshRenderer>().GetMaterials(materials_arms);
            //create temp material
            Material tempMaterial = null;
            foreach (Material mat in materials_arms)
            {
                //Debug.Log("material name: " + mat.name);
                if (mat.name == "skincolor_body (Instance)")
                {
                    //Debug.Log("skincolor_head material found in material list!");
                    tempMaterial = mat;

                }
            }

            if (tempMaterial != null)
            {
                int index = materials_arms.IndexOf(tempMaterial);
                materials_arms.Insert(index, skinColorMaterials[bodyColorIndex]);
                materials_arms.Remove(tempMaterial);
            }

            arms.GetComponent<SkinnedMeshRenderer>().SetMaterials(materials_arms);
            //Debug.Log("Material list is of size: " + materials_head.Count + " with new element added :)");
        }

    }
}
