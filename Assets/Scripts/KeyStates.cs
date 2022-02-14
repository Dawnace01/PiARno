using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PianoUtilities;

namespace PianoUtilities
{
    public enum Hand
    {
        RIGHT,
        LEFT,
        NONE
    }

    public enum Fingering
    {
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        NONE
    }
}


public class KeyStates : MonoBehaviour
{
    [SerializeField]
    public bool isKeyPressed = false, isProgrammedKeyPressed = false; /* false = not pressed, true = pressed */
    public Hand keyCurrentHand = Hand.NONE, keyProgrammedHand = Hand.NONE; 
    public Fingering keyCurrentFinger = Fingering.NONE, keyProgrammedFinger = Fingering.NONE;
    public bool _isError = true;
    public bool isPlayerMode = false; // si faux, la correction d'erreur est stoppée (mode "jeu libre")

    public GameObject key;

    private AudioSource _source;

    /** colors used **/
    public Color keyOriginalColor = Color.white;
    public Color keyCorrectColor = Color.cyan;
    public Color keyErrorColor = Color.red;
    public Color keyNotPlayerModeColor = new Color(239, 156, 2);

    public int cptError = 0;
    public int cptTotal = 0;
    public bool isCollision = false;

    // Start is called before the first frame update
    void Start()
    {
        _source = key.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _isError = isError();
        setColor();
        if (_isError)
        {
            cptError++;
            cptTotal++;
        }
        else if (isCollision)
        {
            cptTotal++;
        }
    }

    IEnumerator wait()
    {
        setColor();
        _isError = isError();

        if (isProgrammedKeyPressed && !_source.isPlaying)
            _source.Play();

        else if (!isProgrammedKeyPressed && _source.isPlaying)
            _source.Stop();
        yield return new WaitForSeconds(.1f);
        StartCoroutine("wait");
    }

    private bool isError()
    {
        if (!isPlayerMode)
            return false;

        return (isKeyPressed != isProgrammedKeyPressed)
            || (keyCurrentHand != keyProgrammedHand)
            || (keyCurrentFinger != keyProgrammedFinger);
    }

    public void OnCollisionEnter(Collision collision)
    {
        
        string[] tabNames;
        string name = collision.gameObject.name;
        if (this.name.Contains("/"))
        {
            tabNames = this.name.Split('/');
            foreach (string n in tabNames)
            {
                if (n.Equals(name))
                {
                    _source.Play();
                    isCollision = true;
                }
            }
        }
        else if (name.Equals(this.name))
        {
            _source.Play();
        }
        else
        {
            _source.Stop();
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        isCollision = false;
        isProgrammedKeyPressed = false;
        keyProgrammedFinger = Fingering.NONE;
        keyProgrammedHand = Hand.NONE;

        _source.Stop();
    }

    public void OnCollisionStay(Collision collision)
    {
        string[] tabNames;
        string name = collision.gameObject.name;
        if (this.name.Contains("/"))
        {
            tabNames = this.name.Split('/');
            foreach (string n in tabNames)
            {
                if (n.Equals(name))
                {
                    isProgrammedKeyPressed = true;
                    keyProgrammedFinger = collision.gameObject.GetComponent<Tile>().finger;
                    keyProgrammedHand = collision.gameObject.GetComponent<Tile>().hand;

                    if (!_source.isPlaying)
                    {
                        _source.Play();
                    }
                }
            }
        }
        else if (name.Equals(this.name))
        {
            isProgrammedKeyPressed = true;
            keyProgrammedFinger = collision.gameObject.GetComponent<Tile>().finger;
            keyProgrammedHand = collision.gameObject.GetComponent<Tile>().hand;

            if (!_source.isPlaying)
            {
                _source.Play();
            }
        }
        else
        {
            isProgrammedKeyPressed = false;
            keyProgrammedFinger = Fingering.NONE;
            keyProgrammedHand = Hand.NONE;
        }
        
    }

    private void setColor()
    {
        MeshRenderer tempRender;
        if (!isPlayerMode && isKeyPressed)
        {
            if (!key.TryGetComponent<MeshRenderer>(out tempRender))
            {
                tempRender = key.GetComponentInChildren<MeshRenderer>();
            }

            tempRender.material.color = keyNotPlayerModeColor;
        }

        else if (_isError)
        {
            if (!key.TryGetComponent<MeshRenderer>(out tempRender))
            {
                tempRender = key.GetComponentInChildren<MeshRenderer>();
            }

            tempRender.material.color = keyErrorColor;
        }

        else if (!_isError && isKeyPressed)
        {
            if (!key.TryGetComponent<MeshRenderer>(out tempRender))
            {
                tempRender = key.GetComponentInChildren<MeshRenderer>();
            }

            tempRender.material.color = keyCorrectColor;
        }

        else
        {
            if (!key.TryGetComponent<MeshRenderer>(out tempRender))
            {
                tempRender = key.GetComponentInChildren<MeshRenderer>();
            }

            tempRender.material.color = keyOriginalColor;
        }
    }

    public void setKeyPressed()
    {
        isKeyPressed = true;
    }

    public void setNotKeyPressed()
    {
        isKeyPressed = false;
    }
}
