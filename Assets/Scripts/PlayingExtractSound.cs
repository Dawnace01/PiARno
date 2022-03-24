using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingExtractSound : MonoBehaviour
{
    private bool isItPlaying = false;
    public AudioSource source;
    private AudioClip audio;

    public void manageSound(AudioClip audioClip)
    {
        if(isItPlaying == false)
        {
            source.PlayOneShot(audioClip);
            audio = audioClip;
            isItPlaying = true;
        } else if (isItPlaying == true && audio != audioClip)
        {
            source.Stop();
            source.PlayOneShot(audioClip);
            audio = audioClip;
            isItPlaying = true;
        } else
        {
            source.Stop();
            isItPlaying = false;
        }
    }

}
