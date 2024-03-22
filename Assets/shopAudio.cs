using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopAudio : MonoBehaviour
{

    public AudioSource shopMusic;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "character" && !shopMusic.isPlaying)
        {
            shopMusic.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "character" && shopMusic.isPlaying)
        {
            shopMusic.Stop();
        }
    }

}
