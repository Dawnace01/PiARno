using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingExtractSound : MonoBehaviour
{
    private bool isItPlaying = false;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void manageSound()
    {
        if(isItPlaying == false)
        {
            source.Play();
            isItPlaying = true;
        } else
        {
            source.Stop();
            isItPlaying = false;
        }
    }

}
