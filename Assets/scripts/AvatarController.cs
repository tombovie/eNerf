using UnityEngine;
//From https://youtu.be/v47lmqfrQ9s?si=yjBdbnTdoESY9vPJ
[System.Serializable]
public class MapTransform
{
    public Transform vrTarget;
    public Transform IKTarget;
    public Vector3 trackingPositionOffset;
    public Vector3 trackingRotationOffset;

    public void MapVRAvatar()
    {
        IKTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        IKTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}
public class AvatarController : MonoBehaviour
{
    [SerializeField] private MapTransform head;
    [SerializeField] private MapTransform leftHand;
    [SerializeField] private MapTransform rightHand;

    [SerializeField] private float turnSmoothness = 0.1f;

    [SerializeField] private Vector3 headBodyPositionOffset;
    [SerializeField] private float headBodyYawOffset;

    void Update()
    {
        //Debug.Log(head.IKTarget.position);
        transform.position = head.IKTarget.position + headBodyPositionOffset;
        float yaw = head.vrTarget.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, yaw, transform.eulerAngles.z), turnSmoothness);

        head.MapVRAvatar();
        leftHand.MapVRAvatar();
        rightHand.MapVRAvatar();
    }
}
