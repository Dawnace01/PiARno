using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideIt : MonoBehaviour
{
    public ParseSheet partition;
    public void Hide(GameObject toHide)
    {
        toHide.SetActive(false);
    }

    public void Show(GameObject toShow)
    {
        toShow.SetActive(true);
        partition.clearPlace();
    }
}
