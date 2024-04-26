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
    [Range(0, 1)]
    public float turnSmoothness = 0.1f;
    public VRMap head = new VRMap();
    public VRMap leftHand = new VRMap();
    public VRMap rightHand = new VRMap();

    public Vector3 headBodyPositionOffset;
    public float headBodyYawOffset;
    private GameObject XR_Origin_camera;

    PlayerController PlayerControllerScript;

    private void Start()
    {
        PlayerControllerScript = GetComponent<PlayerController>();
        // Access the XR Origin instance in the scene
        XR_Origin_camera = GameObject.FindWithTag("MainCamera");
        if (XR_Origin_camera == null)
        {
            // assign vr targets
            Debug.LogWarning("XR Origin not found");
        }
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
        float headRotationX = XR_Origin_camera.transform.localEulerAngles.x;
        float headRotationY = transform.localEulerAngles.y;
        Debug.Log("headrotation x value: " + headRotationX);
        Debug.Log("headrotation y value: " + headRotationY);

        if (CheckIfHeadDown(headRotationX))
        {
            //update z value of head offset
            if (headRotationY > 315 || headRotationY <= 45) //eerste kwadrant (vooraanzicht)
            {
                if (CheckIfHeadDown(headRotationX))
                {

                    headBodyPositionOffset.z = -0.2f;
                }
            }
            if (headRotationY > 45 && headRotationY <= 135) //tweede kwadrant (zijaanzicht-rechts)
            {
                if (CheckIfHeadDown(headRotationX))
                {

                    headBodyPositionOffset.x = -0.2f;
                }
            }
            if (headRotationY > 135 && headRotationY <= 225) //derde kwadrant (achteraanzicht)
            {
                if (CheckIfHeadDown(headRotationX))
                {

                    headBodyPositionOffset.z = 0.2f;
                }
            }
            if (headRotationY > 225 && headRotationY <= 315) //vierde kwadrant (zijaanzicht-links)
            {
                if (CheckIfHeadDown(headRotationX))
                {
                    headBodyPositionOffset.x = 0.2f;
                }
            }
        }
        else
        {
            headBodyPositionOffset.x = 0f;
            headBodyPositionOffset.z = 0f;
        }
    }

    private bool CheckIfHeadDown(float angle)
    {
        if (angle > 45 && angle < 180)
        {        
            return true;
        }
        else
        {
            return false;
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
