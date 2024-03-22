using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMovement : MonoBehaviour
{
    public Transform playerTarget;
    public Transform mirror;
    private Vector3 mirrorPosition;
    private Vector3 mirrorEulerAngles;
    // Start is called before the first frame update
    void Start()
    {
        mirrorPosition = mirror.position;
        mirrorEulerAngles = mirror.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

        //get position of player as if it was child of mirror
        Vector3 localPlayer = mirror.InverseTransformPoint(playerTarget.position);
        Debug.Log(localPlayer);
        //invert position of player along z-axis
        transform.position = mirror.TransformPoint(new Vector3(localPlayer.x, localPlayer.y,  mirrorPosition.z));

        //let camera look at x inverted position
        //Vector3 lookAtMirror = mirror.TransformPoint(new Vector3(-localPlayer.x, localPlayer.y, localPlayer.z));
        //transform.LookAt(lookAtMirror);

        // Get the rotation of the player relative to the mirror
        Quaternion playerRotationRelativeToMirror = Quaternion.Inverse(mirror.rotation) * playerTarget.rotation;

        // Invert the rotation around the y-axis
        Quaternion invertedRotation = Quaternion.Euler(0, -playerRotationRelativeToMirror.eulerAngles.y, 0);
        // Calculate the look-at point based on the player's rotation
        Vector3 lookAtMirror = mirror.position + mirror.forward + invertedRotation * Vector3.back;

        transform.LookAt(lookAtMirror);

        // Fix the x rotation of the camera at zero
        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}

