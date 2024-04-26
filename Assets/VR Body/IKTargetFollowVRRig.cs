using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class VRMap
{
    public Transform vrTarget;
    public Transform ikTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;
    
    public void Map()
    {
        ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class IKTargetFollowVRRig : MonoBehaviour
{
    [Range(0,1)]
    public float turnSmoothness = 0.1f;
    public VRMap head = new VRMap();
    public VRMap leftHand = new VRMap();
    public VRMap rightHand = new VRMap();

    public Vector3 headBodyPositionOffset;
    public float headBodyYawOffset;

    PlayerController PlayerControllerScript;

    private void Start()
    {
        PlayerControllerScript = GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (head.ikTarget != null && head.vrTarget != null && rightHand.ikTarget != null && rightHand.vrTarget != null && leftHand.ikTarget != null && leftHand.vrTarget != null)
        {
            transform.position = head.ikTarget.position + headBodyPositionOffset;
            float yaw = head.vrTarget.eulerAngles.y;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z), turnSmoothness);

            head.Map();
            leftHand.Map();
            rightHand.Map();
        }


        //fetch head rotation
        float headRotation = head.ikTarget.gameObject.transform.localEulerAngles.x;
        Debug.Log("headrotation x value: " + headRotation);
        //update z value of head offset
        if(headRotation > 45)
        {
            headBodyPositionOffset.z = 0.10f;
        }
        else
        {
            headBodyPositionOffset.z = 0f;
        }
    }
    public void setHeadTarget(Transform vrTarget, Transform ikTarget)
    {
        head.vrTarget = vrTarget;
        head.ikTarget = ikTarget;
    }
    public void setLeftHandTarget(Transform vrTarget, Transform ikTarget)
    {
        leftHand.vrTarget = vrTarget;
        leftHand.ikTarget = ikTarget;
    }
    public void setRightHandTarget(Transform vrTarget, Transform ikTarget)
    {
        rightHand.vrTarget = vrTarget;
        rightHand.ikTarget = ikTarget;
    }

    public void setheadBodyPositionOffset(Vector3 values)
    {
        headBodyPositionOffset = values;
    }
}
