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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("wait");
    }

    // Update is called once per frame
    void Update(){ }

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
}
