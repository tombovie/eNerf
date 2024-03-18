using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class buildVRInteraction : MonoBehaviour
{
    [SerializeField] private GameObject leftHandBones;
    [SerializeField] private GameObject rightHandBones;
    private GameObject rightForeArmBones;
    private GameObject leftForeArmBones;
    GameObject IKTarget;
    GameObject IKTargetLeft;

    // Start is called before the first frame update
    void Start()
    {
        // Create empty GameObject with name 
        GameObject VRIKRig = new GameObject("VR IK Rig");
        VRIKRig.transform.parent = transform; //Parent is current Gameobject
        Rig rig = VRIKRig.AddComponent<Rig>();

        // Add a RigBuilder component to the parent GameObject (this GameObject)
        RigBuilder rigBuilder = gameObject.AddComponent<RigBuilder>();


        // Add the Rig to the RigBuilder
        RigLayer rigLayer = new RigLayer(rig);
        rigBuilder.layers.Add(rigLayer);




        // Create Right arm IK
        GameObject RightArmIK = new GameObject("Right Arm IK");
        RightArmIK.transform.parent = VRIKRig.transform; //Parent is current Gameobject
        // Add TwoBoneIKConstraint component to the RightArmIK GameObject
        TwoBoneIKConstraint ikConstraint = RightArmIK.AddComponent<TwoBoneIKConstraint>();
        // Check if the tipTransform reference is assigned
        // Set the tip variable of the TwoBoneIKConstraint to the right hand
        if (rightHandBones != null)
        {
            // Set the tip variable of the TwoBoneIKConstraint to the transform of rightHandBones
            ikConstraint.data.tip = rightHandBones.transform;
            // Run auto setup from tip transform
            rightForeArmBones = rightHandBones.transform.parent.gameObject;
            ikConstraint.data.mid = rightForeArmBones.transform;
            ikConstraint.data.root = rightForeArmBones.transform.parent;

            // Set the target and hint
            IKTarget = new GameObject("Right Arm IK_target");
            IKTarget.transform.parent = RightArmIK.transform;
            ikConstraint.data.target = IKTarget.transform;

            GameObject IKHint = new GameObject("Right Arm IK_hint");
            IKHint.transform.parent = RightArmIK.transform;
            ikConstraint.data.hint = IKHint.transform;

            IKTarget.transform.position = rightHandBones.transform.position;
            IKTarget.transform.rotation = rightHandBones.transform.rotation;

            IKHint.transform.position = new Vector3(0.5f, -0.5f, -0.5f);

            // Set weight to 1 (full influence)
            ikConstraint.weight = 1f;

        }
        else
        {
            Debug.LogWarning("Right hand bones reference not assigned.");
        }




        // Create Right arm IK
        GameObject LeftArmIK = new GameObject("Left Arm IK");
        LeftArmIK.transform.parent = VRIKRig.transform; //Parent is current Gameobject
        // Add TwoBoneIKConstraint component to the RightArmIK GameObject
        TwoBoneIKConstraint ikConstraintLeft = LeftArmIK.AddComponent<TwoBoneIKConstraint>();
        // Check if the tipTransform reference is assigned
        // Set the tip variable of the TwoBoneIKConstraint to the right hand
        if (leftHandBones != null)
        {
            // Set the tip variable of the TwoBoneIKConstraint to the transform of rightHandBones
            ikConstraintLeft.data.tip = leftHandBones.transform;
            // Run auto setup from tip transform
            leftForeArmBones = leftHandBones.transform.parent.gameObject;
            ikConstraintLeft.data.mid = leftForeArmBones.transform;
            ikConstraintLeft.data.root = leftForeArmBones.transform.parent;

            // Set the target and hint
            IKTargetLeft = new GameObject("Left Arm IK_target");
            IKTargetLeft.transform.parent = LeftArmIK.transform;
            ikConstraintLeft.data.target = IKTargetLeft.transform;

            GameObject IKHintLeft = new GameObject("Left Arm IK_hint");
            IKHintLeft.transform.parent = LeftArmIK.transform;
            ikConstraintLeft.data.hint = IKHintLeft.transform;

            IKTargetLeft.transform.position = leftHandBones.transform.position;
            IKTargetLeft.transform.rotation = leftHandBones.transform.rotation;

            IKHintLeft.transform.position = new Vector3(-0.5f, -0.5f, -0.5f);

            // Set weight to 1 (full influence)
            ikConstraintLeft.weight = 1f;

        }
        else
        {
            Debug.LogWarning("Right hand bones reference not assigned.");
        }

        // **Corrected Line:**
        // Use RigBuilder.Build() to create the rig instead of ConstraintJob.AlignTransform
        rigBuilder.Build();




    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
