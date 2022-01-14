using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualPiano : MonoBehaviour
{
    Material m_Material;
    AudioSource s_Sound;
    bool once = true;

    void Start()
    {
        //Fetch the Material from the Renderer of the GameObject
        m_Material = GetComponent<MeshRenderer>().material;
        s_Sound = GetComponent<AudioSource>();
    }

    void OnMouseOver()
    {
        startScript();
    }

    void OnMouseExit()
    {
        stopScript();
    }

    private void OnMouseDown()
    {
        playSound();
    }

    public void playSound()
    {
        if (s_Sound != null)
            s_Sound.Play();
    }

    private void OnMouseUp()
    {
        stopSound();
    }

    public void stopSound()
    {
        if (s_Sound != null)
            s_Sound.Stop();
    }

    public void startScript()
    {
        // Change the Color of the GameObject when the mouse hovers over it
        if (once && m_Material != null)
        {
            m_Material.color = Color.cyan;
            //Debug.Log(transform.name);
            once = false;
        }
    }

    public void stopScript()
    {
        if (m_Material == null)
            return;

        //Change the Color back to white when the mouse exits the GameObject
        if (transform.parent.name == "Blanches")
            m_Material.color = Color.white;

        else if (transform.parent.name == "Noires")
            m_Material.color = Color.black;

        once = true;
    }
}
