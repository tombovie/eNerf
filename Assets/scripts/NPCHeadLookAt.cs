using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
//Adapted from: https://www.youtube.com/watch?v=LdoImzaY6M4

public class NPCHeadLookAt : MonoBehaviour
{
    [SerializeField] private Rig rig;
    [SerializeField] private Transform HeadLookAtTransform;

    private bool isLookingAtPosition;

    private void Update()
    {
        float targetWeight = isLookingAtPosition ? 1f: 0f;
        float lerpSpeed = 2f;
        rig.weight = Mathf.Lerp(rig.weight, targetWeight, Time.deltaTime * lerpSpeed);  
    }

    public void lookAtPosition(Vector3 lookAtPosition)
    {
        isLookingAtPosition = true;
        HeadLookAtTransform.position = lookAtPosition;
    }

}
