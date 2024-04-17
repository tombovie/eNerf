using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopAudioController : MonoBehaviour
{
    public AudioSource shopAudio;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !shopAudio.isPlaying)
        {
            Debug.Log("player detected!");
            shopAudio.UnPause();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && shopAudio.isPlaying)
        {
            Debug.Log("player byebye!");
            shopAudio.Pause();
        }
    }
}
