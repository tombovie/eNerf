using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class InstructionUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    public bool isShowing;
    public bool inCoRoutine;


    void Update()
    {
        // Get the list of active XR devices (controllers)
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        // Check for Button.one press on any active controller
        foreach (var device in devices)
        {
            if (device.TryGetFeatureValue(CommonUsages.menuButton, out bool isButtonPressed) && isButtonPressed)
            {
                if (!inCoRoutine)
                {
                    StartCoroutine(WaitTwoSecond_Loop());
                }            
            }
        }
    }

    private void Show()
    {
        containerGameObject.SetActive(true);
        isShowing = true;
    }

    private void Hide()
    {
        containerGameObject.SetActive(false);
        isShowing = false;
    }

    IEnumerator WaitTwoSecond_Loop()
    {
        inCoRoutine = true;
        if (isShowing) { Hide(); }
        else { Show(); }

        float elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        inCoRoutine = false;  
    }
}
