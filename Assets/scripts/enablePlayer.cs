using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class enablePlayer : MonoBehaviour
{
    [SerializeField] private PlayerInteractUI playerInteractUI;
    private int bodyTypeIndex, bodyColorIndex;
    private GameObject currentBodyType;
    public GameObject spawnPoint;
    public List<Material> skinColorMaterials;
    public GameObject head_target, right_target, left_target;
    public GameObject XR_Origin;
    private bool playerHeightSetted, startLoop;
    public RuntimeAnimatorController VRRigAnimator;
    public GameObject LoadingScreen;

    // Start is called before the first frame update
    void Start()
    {
        //fetch local data from startscene
        bodyTypeIndex = PlayerPrefs.GetInt("bodytype");
        bodyColorIndex = PlayerPrefs.GetInt("bodycolor");

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("previousSceneIndex",currentSceneIndex);
        Debug.Log(currentSceneIndex);
        if (currentSceneIndex == 1)
        {
            SetBodyTypeIndex();
            //assign VR targets
            AssignTargets();

            //after bodytypes has been instantiated, assign skincolor
            AssignSkinColor();

            //Assign Hand Animations
            AssignHandAnimation();

            //set players height after 2s
            StartCoroutine(WaitOneSecond_Loop());

            playerInteractUI.SetNPC_Nearby(currentBodyType.GetComponent<NPC_Nearby>());
        }
        if (currentSceneIndex == 2)
        {
            SetBodyTypeIndex();
            //assign VR targets
            AssignTargets();

            //after bodytypes has been instantiated, assign skincolor
            AssignSkinColor();

            //Assign Hand Animations
            AssignHandAnimation();

            //set players height after 2s
            StartCoroutine(WaitOneSecond_Loop());

            playerInteractUI.SetNPC_Nearby(currentBodyType.GetComponent<NPC_Nearby>());
            currentBodyType.GetComponent<HeadSwap>().SwapHead();
        }
        if (currentSceneIndex == 3)
        {
            //get name from local data
            String currentCharacter = PlayerPrefs.GetString("character");
            currentBodyType = (GameObject)Instantiate(Resources.Load(currentCharacter + "/" + currentCharacter), spawnPoint.transform.position, spawnPoint.transform.rotation, transform);

            //set players height after 2s
            StartCoroutine(WaitOneSecond_Loop());

            playerInteractUI.SetNPC_Nearby(currentBodyType.GetComponent<NPC_Nearby>());
        }




    }

    private void SetBodyTypeIndex()
    {
        switch (bodyTypeIndex)
        {
            case 0:
                currentBodyType = (GameObject)Instantiate(Resources.Load("Basic_vrouw/vrouw_slank"), spawnPoint.transform.position, spawnPoint.transform.rotation, transform);
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
    }
    private void AssignHandAnimation()
    {
        currentBodyType.GetComponent<Animator>().runtimeAnimatorController = VRRigAnimator;
    }

    private void SetPlayerHeight_legAngle()
    {
        //get leftleg of player
        Transform leftLeg = currentBodyType.transform.Find("Armature/Hips/LeftUpLeg/LeftLeg");
        //get rightLeg of player
        Transform rightLeg = currentBodyType.transform.Find("Armature/Hips/RightUpLeg/RightLeg");

        
        //fetch x-angle of this leg && increase camerayoffset
        /*Debug.Log(leftLeg.transform.rotation.eulerAngles.x);*/
        if (((leftLeg.transform.rotation.eulerAngles.x < 10) || (leftLeg.transform.rotation.eulerAngles.x > 355))
            && ((rightLeg.transform.rotation.eulerAngles.x < 10)) || (rightLeg.transform.rotation.eulerAngles.x > 355))
        {
            playerHeightSetted = true;
            //disable loading screen
            LoadingScreen.SetActive(false);

            /*Vector3 newPos = XR_Origin.transform.position + new Vector3(0f, 0.001f, 0f);
            Vector3 newPos2 = XR_Origin.transform.GetChild(0).position + new Vector3(0f, 0.001f, 0f);
            XR_Origin.transform.position = newPos;
            XR_Origin.transform.GetChild(0).position = newPos2;*/
        }
        /*else if (leftLeg.transform.rotation.eulerAngles.x >= 180 && leftLeg.transform.rotation.eulerAngles.x <= 360)
        {
            // Debug.Log("Decreasing the camerayoffset!"); 
            XR_Origin.GetComponent<XROrigin>().CameraYOffset = XR_Origin.GetComponent<XROrigin>().CameraYOffset - 0.008f;
        }
        else if (leftLeg.transform.rotation.eulerAngles.x < 10 && leftLeg.transform.rotation.eulerAngles.x >= 0)
        {
            // Debug.Log("Decreasing the camerayoffset!"); 
            XR_Origin.GetComponent<XROrigin>().CameraYOffset = XR_Origin.GetComponent<XROrigin>().CameraYOffset - 0.008f;
        }*/
        else //when value is between 0 and 20 = ideaal
        {
            XR_Origin.GetComponent<XROrigin>().CameraYOffset = XR_Origin.GetComponent<XROrigin>().CameraYOffset + 0.003f;
            
        }
    }

    private void SetPlayerHeight()
    {
        Transform leftLegt = currentBodyType.transform.Find("Armature/Hips/LeftUpLeg/LeftLeg/LeftFoot/LeftToeBase");
        if (leftLegt == null)
        {
            leftLegt = currentBodyType.transform.Find("Armature/mixamorig:Hips/mixamorig:LeftUpLeg/mixamorig:LeftLeg/mixamorig:LeftFoot/mixamorig:LeftToeBase");
        }
        if (leftLegt == null)
        {
            Debug.Log("LeftLegt not found!");
        }
        Transform neck = currentBodyType.transform.Find("Armature/mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:Neck");
        if (neck == null)
        {
            neck = currentBodyType.transform.Find("Armature/Hips/Spine/Spine1/Spine2/Neck");
        }
        if (neck == null)
        {
            Debug.Log("neck not found!");
        }
        float length = neck.position.y - leftLegt.position.y;

        //Debug.Log(neck.position.y);
        //Debug.Log(length);

        //Check if player is floating (too large) or crouching (too small)
        if (neck.position.y > 1.5)
        {
            // floating
            XR_Origin.GetComponent<XROrigin>().CameraYOffset = XR_Origin.GetComponent<XROrigin>().CameraYOffset - 0.01f;
        }

        //fetch difference of the length between neck and feet && increase camerayoffset
        if (length < 0.5)
        {
            XR_Origin.GetComponent<XROrigin>().CameraYOffset = XR_Origin.GetComponent<XROrigin>().CameraYOffset + 0.15f;
        }
        if ((length > 0.5) && (length < 1))
        {
            XR_Origin.GetComponent<XROrigin>().CameraYOffset = XR_Origin.GetComponent<XROrigin>().CameraYOffset + 0.08f;
        }
        if (length < 1.35)
        {
            XR_Origin.GetComponent<XROrigin>().CameraYOffset = XR_Origin.GetComponent<XROrigin>().CameraYOffset + 0.01f;
        }
        else
        {
            playerHeightSetted = true;
        }
    }

    IEnumerator WaitOneSecond_Loop()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        startLoop = true;
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
        if (startLoop && !playerHeightSetted)
        {

            SetPlayerHeight(); 
        }
       
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
