using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMoves : MonoBehaviour
{

    [SerializeField][Range(1f,20f)]
    float speed = 15f;
    Transform trans;
    [SerializeField]
    GameObject parent;

    bool update = false;

    private void Start()
    {
        trans = GetComponent<Transform>();
    }

    private void Update()
    {
        if (update)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        if(update && (trans.position.z - parent.GetComponent<Transform>().position.z + (trans.localScale.z/2)) <= 12.0)
        {
            update = false;
        }

        //if (transform.localScale.z < 0.1)
        //{
        //    update = false;
        //}
    }

    public void move()
    {
        update = true;
    }
}
