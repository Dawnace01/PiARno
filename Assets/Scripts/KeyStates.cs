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

    /** colors used **/
    public Color keyOriginalColor = Color.white;
    public Color keyCorrectColor = Color.cyan;
    public Color keyErrorColor = Color.red;
    public Color keyNotPlayerModeColor = new Color(239, 156, 2);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        setColor();
        _isError = isError();
    }

    IEnumerator wait()
    {
        _isError = isError();
        yield return new WaitForSeconds(.5f);
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

    public void OnCollisionExit(Collision collision)
    {
        isProgrammedKeyPressed = false;
        keyProgrammedFinger = Fingering.NONE;
        keyProgrammedHand = Hand.NONE;
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
                }
            }
        }
        else if (name.Equals(this.name))
        {
            isProgrammedKeyPressed = true;
            keyProgrammedFinger = collision.gameObject.GetComponent<Tile>().finger;
            keyProgrammedHand = collision.gameObject.GetComponent<Tile>().hand;
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
        if (!isPlayerMode && isKeyPressed)
        {
            key.GetComponent<MeshRenderer>().material.color = keyNotPlayerModeColor;
        }

        else if (_isError)
        {
            key.GetComponent<MeshRenderer>().material.color = keyErrorColor;
        }

        else if (!_isError && isKeyPressed)
        {
            key.GetComponent<MeshRenderer>().material.color = keyCorrectColor;
        }

        else
        {
            key.GetComponent<MeshRenderer>().material.color = keyOriginalColor;
        }
    }
}
