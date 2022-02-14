using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicIconMusicExtract : MonoBehaviour
{
    private bool isPlaying = false;
    private ButtonConfigHelper lastButton;

    public void manageIcon(ButtonConfigHelper button)
    {

        
        
        if (isPlaying == false)
        {
            button.SetQuadIconByName("Stop");
            lastButton = button;
            isPlaying = true;
        }
        else if (isPlaying == true && lastButton != button)
        {
            button.SetQuadIconByName("Stop");
            lastButton.SetQuadIconByName("Play");
            lastButton = button;
            isPlaying = true;
        }
        else
        {
            button.SetQuadIconByName("Play");
            button = null;
            isPlaying = false;
        }
    }
}
