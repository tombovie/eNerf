using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations.Rigging;
//using UnityEditor.Animations.Rigging;
using UnityEngine.Playables;
using UnityEngine.UIElements;
using System.Linq;

public class buildVRInteraction : MonoBehaviour
{
    [SerializeField] GameObject body;
    [SerializeField] private GameObject leftHandBones;
    [SerializeField] private GameObject rightHandBones;
    [SerializeField] private GameObject rightFoot;
    [SerializeField] private GameObject leftFoot;
    [SerializeField] GameObject neck;

    [SerializeField] GameObject headVRTarget;
    [SerializeField] GameObject RightHandVRTarget;
    [SerializeField] GameObject LeftHandVRTarget;


    private GameObject rightForeArmBones;
    private GameObject leftForeArmBones;
    private GameObject rightLeg;
    private GameObject leftLeg;
    GameObject IKTargetRightArm;
    GameObject IKTargetLeftArm;
    GameObject IKTargetRightLeg;
    GameObject IKTargetLeftLeg;

    GameObject HeadTarget;

    // Start is called before the first frame update
    void Start()
    {
        // Create empty GameObject with name 
        GameObject VRIKRig = new GameObject("VR IK Rig");
        VRIKRig.transform.parent = transform; //Parent is current Gameobject
        VRIKRig.transform.localPosition = new Vector3(0f, 0f, 0f);
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
        ikConstraint.data.targetPositionWeight = 1;
        ikConstraint.data.targetRotationWeight = 1;
        ikConstraint.data.hintWeight = 1;

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
            IKTargetRightArm = new GameObject("Right Arm IK_target");
            IKTargetRightArm.transform.parent = RightArmIK.transform;
            ikConstraint.data.target = IKTargetRightArm.transform;

            GameObject IKHint = new GameObject("Right Arm IK_hint");
            IKHint.transform.parent = RightArmIK.transform;
            ikConstraint.data.hint = IKHint.transform;

            IKTargetRightArm.transform.position = rightHandBones.transform.position;
            IKTargetRightArm.transform.rotation = rightHandBones.transform.rotation;

            IKHint.transform.position = new Vector3(0.5f, -0.5f, -0.5f);

            // Set weight to 1 (full influence)
            ikConstraint.weight = 1f;

        }
        else
        {
            Debug.LogWarning("Right hand bones reference not assigned.");
        }




        // Create Left arm IK
        GameObject LeftArmIK = new GameObject("Left Arm IK");
        LeftArmIK.transform.parent = VRIKRig.transform; //Parent is current Gameobject
        // Add TwoBoneIKConstraint component to the GameObject
        TwoBoneIKConstraint ikConstraintLeft = LeftArmIK.AddComponent<TwoBoneIKConstraint>();
        ikConstraintLeft.data.targetPositionWeight = 1;
        ikConstraintLeft.data.targetRotationWeight = 1;
        ikConstraintLeft.data.hintWeight = 1;
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
            IKTargetLeftArm = new GameObject("Left Arm IK_target");
            IKTargetLeftArm.transform.parent = LeftArmIK.transform;
            ikConstraintLeft.data.target = IKTargetLeftArm.transform;

            GameObject IKHintLeft = new GameObject("Left Arm IK_hint");
            IKHintLeft.transform.parent = LeftArmIK.transform;
            ikConstraintLeft.data.hint = IKHintLeft.transform;

            IKTargetLeftArm.transform.position = leftHandBones.transform.position;
            IKTargetLeftArm.transform.rotation = leftHandBones.transform.rotation;

            IKHintLeft.transform.position = new Vector3(-0.5f, -0.5f, -0.5f);

            // Set weight to 1 (full influence)
            ikConstraintLeft.weight = 1f;

        }
        else
        {
            Debug.LogWarning("Left hand bones reference not assigned.");
        }

        // Create Right leg IK
        GameObject RightLegIK = new GameObject("Right Leg IK");
        RightLegIK.transform.parent = VRIKRig.transform; //Parent is current Gameobject
        // Add TwoBoneIKConstraint component to the GameObject
        TwoBoneIKConstraint ikConstraintRightLeg = RightLegIK.AddComponent<TwoBoneIKConstraint>();
        ikConstraintRightLeg.data.targetPositionWeight = 1;
        ikConstraintRightLeg.data.targetRotationWeight = 1;
        ikConstraintRightLeg.data.hintWeight = 1;
        // Check if the tipTransform reference is assigned
        // Set the tip variable of the TwoBoneIKConstraint to the right hand
        if (rightFoot != null)
        {
            // Set the tip variable of the TwoBoneIKConstraint to the transform of rightHandBones
            ikConstraintRightLeg.data.tip = rightFoot.transform;
            // Run auto setup from tip transform
            rightLeg = rightFoot.transform.parent.gameObject;
            ikConstraintRightLeg.data.mid = rightLeg.transform;
            ikConstraintRightLeg.data.root = rightLeg.transform.parent;

            // Set the target and hint
            IKTargetRightLeg = new GameObject("Right Leg IK_target");
            IKTargetRightLeg.transform.parent = RightLegIK.transform;
            ikConstraintRightLeg.data.target = IKTargetRightLeg.transform;

            GameObject IKHintLeft = new GameObject("Right Leg IK_hint");
            IKHintLeft.transform.parent = RightLegIK.transform;
            ikConstraintRightLeg.data.hint = IKHintLeft.transform;

            IKTargetRightLeg.transform.position = rightFoot.transform.position;
            IKTargetRightLeg.transform.rotation = rightFoot.transform.rotation;

            IKHintLeft.transform.position = new Vector3(0.27f, -0.6f, 2.2f);

            // Set weight to 1 (full influence)
            ikConstraintRightLeg.weight = 1f;

        }
        else
        {
            Debug.LogWarning("Right Leg bones reference not assigned.");
        }

        // Create Left leg IK
        GameObject LeftLegIK = new GameObject("Left Leg IK");
        LeftLegIK.transform.parent = VRIKRig.transform; //Parent is current Gameobject
        // Add TwoBoneIKConstraint component to the GameObject
        TwoBoneIKConstraint ikConstraintLeftLeg = LeftLegIK.AddComponent<TwoBoneIKConstraint>();
        ikConstraintLeftLeg.data.targetPositionWeight = 1;
        ikConstraintLeftLeg.data.targetRotationWeight = 1;
        ikConstraintLeftLeg.data.hintWeight = 1;
        // Check if the tipTransform reference is assigned
        // Set the tip variable of the TwoBoneIKConstraint to the right hand
        if (leftFoot != null)
        {
            // Set the tip variable of the TwoBoneIKConstraint to the transform of rightHandBones
            ikConstraintLeftLeg.data.tip = leftFoot.transform;
            // Run auto setup from tip transform
            leftLeg = leftFoot.transform.parent.gameObject;
            ikConstraintLeftLeg.data.mid = leftLeg.transform;
            ikConstraintLeftLeg.data.root = leftLeg.transform.parent;

            // Set the target and hint
            IKTargetLeftLeg = new GameObject("Left Leg IK_target");
            IKTargetLeftLeg.transform.parent = LeftLegIK.transform;
            ikConstraintLeftLeg.data.target = IKTargetLeftLeg.transform;

            GameObject IKHintLeft = new GameObject("Left Leg IK_hint");
            IKHintLeft.transform.parent = LeftLegIK.transform;
            ikConstraintLeftLeg.data.hint = IKHintLeft.transform;

            IKTargetLeftLeg.transform.position = leftFoot.transform.position;
            IKTargetLeftLeg.transform.rotation = leftFoot.transform.rotation;

            IKHintLeft.transform.position = new Vector3(-0.27f, -0.6f, 2.2f);

            // Set weight to 1 (full influence)
            ikConstraintLeftLeg.weight = 1f;

        }
        else
        {
            Debug.LogWarning("Right Leg bones reference not assigned.");
        }

        //Set an IK foot solver script to the left and right leg target
        IKFootSolver LeftLegIKFootSolver = IKTargetLeftLeg.AddComponent<IKFootSolver>();
        if (body != null) { LeftLegIKFootSolver.setBody(body); }
        LeftLegIKFootSolver.setTerainLayerToDefault();
        LeftLegIKFootSolver.setFootRotationOffset(new Vector3(-120f, 180f, 0f));

        IKFootSolver RightLegIKFootSolver = IKTargetRightLeg.AddComponent<IKFootSolver>();
        if (body != null) { RightLegIKFootSolver.setBody(body); }
        RightLegIKFootSolver.setTerainLayerToDefault();
        RightLegIKFootSolver.setFootRotationOffset(new Vector3(-120f, 180f, 0f));

        LeftLegIKFootSolver.setOtherFoot(RightLegIKFootSolver);
        RightLegIKFootSolver.setOtherFoot(LeftLegIKFootSolver);




        // Create Head IK
        GameObject HeadIK = new GameObject("Head IK");
        HeadIK.transform.parent = VRIKRig.transform; //Parent is current Gameobject
        MultiParentConstraint MultiParentConstraintHeadIK = HeadIK.AddComponent<MultiParentConstraint>();
        MultiParentConstraintHeadIK.data.constrainedObject = neck.transform;
        HeadTarget = new GameObject("Head Target");
        HeadTarget.transform.parent = HeadIK.transform;

        MultiParentConstraintHeadIK.data.constrainedPositionXAxis = true;
        MultiParentConstraintHeadIK.data.constrainedPositionYAxis = true;
        MultiParentConstraintHeadIK.data.constrainedPositionZAxis = true;
        MultiParentConstraintHeadIK.data.constrainedRotationXAxis = true;
        MultiParentConstraintHeadIK.data.constrainedRotationYAxis = true;
        MultiParentConstraintHeadIK.data.constrainedRotationZAxis = true;

        //Set the source objects array
        // Assuming you have a reference to the constraint and a Transform to add
        WeightedTransform WeightedTransformHeadTarget = new WeightedTransform { transform = HeadTarget.transform, weight = 1f }; // Set weight as needed
        WeightedTransformArray newSourceObjects = new WeightedTransformArray();
        newSourceObjects.Add(WeightedTransformHeadTarget);
        MultiParentConstraintHeadIK.data.sourceObjects = newSourceObjects;

        //align position and rotation
        HeadTarget.transform.position = neck.transform.position;
        HeadTarget.transform.rotation = neck.transform.rotation;


        //For the VR tracking
        IKTargetFollowVRRig IKTargetFollowRigScript = gameObject.AddComponent<IKTargetFollowVRRig>();
        if (HeadTarget == null || headVRTarget == null)
        {
            Debug.Log("there was an eeror");
        }
        IKTargetFollowRigScript.setHeadTarget(headVRTarget.transform, HeadTarget.transform);
        IKTargetFollowRigScript.setLeftHandTarget(LeftHandVRTarget.transform, IKTargetLeftArm.transform);
        IKTargetFollowRigScript.setRightHandTarget(RightHandVRTarget.transform, IKTargetRightArm.transform);

        IKTargetFollowRigScript.setheadBodyPositionOffset(new Vector3(0f, -0.73f, 0.29f));

        /* // For hands animations
         AnimateOnInput animateOnInput = gameObject.AddComponent<AnimateOnInput>();
         List<AnimationInput> newAnimationsInput = new List<AnimationInput>();
         AnimationInput animationInput1 = new AnimationInput();




         animateOnInput.animationInputs = newAnimationsInput;*/




        // **Corrected Line:**
        // Use RigBuilder.Build() to create the rig instead of ConstraintJob.AlignTransform
        rigBuilder.Build();





    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setHeadTarget(GameObject vrTarget)
    {
        headVRTarget = vrTarget;
    }
    public void setLeftHandTarget(GameObject vrTarget)
    {
        LeftHandVRTarget = vrTarget;
    }
    public void setRightHandTarget(GameObject vrTarget)
    {
        RightHandVRTarget = vrTarget;
    }
}
