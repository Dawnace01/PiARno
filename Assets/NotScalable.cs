using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotScalable : MonoBehaviour
{
    public GameObject target;
    public float scaleX, scaleY, scaleZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }
}
