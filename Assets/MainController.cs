using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MainController : MonoBehaviour
{
    public bool isUsingKeyboard = false; // Now public
    PlayerController playerController;
    CharacterController characterController;
    IKTargetFollowVRRig iKTargetFollowVR;
    AnimateOnInput animateOnINput;
    BoneRenderer boneRenderer;
    Animator Animator;
    RigBuilder rb;

    [SerializeField] RuntimeAnimatorController VRAnimator;
    [SerializeField] RuntimeAnimatorController WalkingAnimator;


    void Start()
    {
        playerController = GetComponent<PlayerController>();
        characterController = GetComponent<CharacterController>();
        iKTargetFollowVR = GetComponent<IKTargetFollowVRRig>();
        animateOnINput = GetComponent<AnimateOnInput>();
        boneRenderer = GetComponent<BoneRenderer>();
        Animator = GetComponent<Animator>();
        rb = GetComponent<RigBuilder>();
    }

    void Update()
    {
        //Debug.Log(GetUsingKeyboard());
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) 
        {
            SetUsingKeyboard(true);
            playerController.enabled = true;
            characterController.enabled = true;
            iKTargetFollowVR.enabled = false;
            animateOnINput.enabled = false;
            boneRenderer.enabled = false;
            rb.enabled = false;
            Animator.runtimeAnimatorController = WalkingAnimator;
        }
        else
        {
            SetUsingKeyboard(false);
            playerController.enabled = false;
            characterController.enabled = false;
            iKTargetFollowVR.enabled = true;
            animateOnINput.enabled = true;
            boneRenderer.enabled = true;
            rb.enabled = true;
            Animator.runtimeAnimatorController = VRAnimator;
        }
    }
    public void SetUsingKeyboard(bool value) { isUsingKeyboard = value; }
    public bool GetUsingKeyboard() { return isUsingKeyboard; }
}
