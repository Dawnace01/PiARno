using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHistory : MonoBehaviour
{
    public GridObjectCollection listParent;
    public GameObject prefabElementList;
    public void generateHistory()
    {
        GameObject temp;
        temp = Instantiate(prefabElementList);
    }

    private void Start()
    {   
        //Only for test
        for(int i =0; i < 20; i++)
        {
            GameObject temp;
            temp = Instantiate(prefabElementList, listParent.transform);
            listParent.UpdateCollection();
        }
    }
}
