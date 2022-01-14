using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Microsoft;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Input;

public class FingerDetection : MonoBehaviour
{

    //[SerializeField]
    //TextMeshPro textToModify;

    //public GameObject sphereMarker;

    //GameObject thumbObject;
    //GameObject indexObject;
    //GameObject middleObject;
    //GameObject ringObject;
    //GameObject pinkyObject;

    //MixedRealityPose pose;

    //void Start()
    //{
    //    thumbObject = Instantiate(sphereMarker, this.transform);
    //    indexObject = Instantiate(sphereMarker, this.transform);
    //    middleObject = Instantiate(sphereMarker, this.transform);
    //    ringObject = Instantiate(sphereMarker, this.transform);
    //    pinkyObject = Instantiate(sphereMarker, this.transform);
    //}

    //void Update()
    //{
    //    if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out pose))
    //    {
    //        thumbObject.GetComponent<Renderer>().enabled = true;
    //        thumbObject.transform.position = pose.Position;
    //    }
    //}


    //public void setTextFinger()
    //{

    //    textToModify.SetText("Button Pressed");
    //}

    [SerializeField]
    GameObject[] buttons;
    [SerializeField]
    GameObject text;

    public GameObject sphereMarker;
    MixedRealityPose pose;
    GameObject thumbObject;
    GameObject indexObject;
    GameObject middleObject;
    GameObject ringObject;
    GameObject pinkyObject;

    private void Start()
    {
        thumbObject = Instantiate(sphereMarker, this.transform);
        indexObject = Instantiate(sphereMarker, this.transform);
        middleObject = Instantiate(sphereMarker, this.transform);
        ringObject = Instantiate(sphereMarker, this.transform);
        pinkyObject = Instantiate(sphereMarker, this.transform);
    }

    private void Update()
    {
/****** Right hand ******/
    // Thumb
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Right, out pose))
        {
            thumbObject.transform.position = pose.Position;
            Debug.Log("Pouce Droit : " + pose.Position);
            if (isCollision(pose.Position))
            {
                this.setText("Pouce droit");
            }
        }
    // Index
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Right, out pose))
        {
            indexObject.transform.position = pose.Position;
            Debug.Log("Index Droit : " + pose.Position);
            if (isCollision(pose.Position))
            {
                this.setText("Index droit");
            }
        }
    // Middle
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Right, out pose))
        {
            middleObject.transform.position = pose.Position;
            Debug.Log("Majeur Droit : " + pose.Position);
            if (isCollision(pose.Position))
            {
                this.setText("Majeur droit");
            }
        }
    // Ring
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Right, out pose))
        {
            ringObject.transform.position = pose.Position;
            Debug.Log("Annulaire Droit : " + pose.Position);
            if (isCollision(pose.Position))
            {
                this.setText("Annulaire droit");
            }
        }
    // Pinky
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Right, out pose))
        {
            pinkyObject.transform.position = pose.Position;
            Debug.Log("Auriculaire Droit : " + pose.Position);
            if (isCollision(pose.Position))
            {
                this.setText("Auriculaire droit");
            }
        }

/****** Left hand ******/
    // Thumb
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Left, out pose))
        {
            thumbObject.transform.position = pose.Position;
            if (isCollision(pose.Position))
            {
                this.setText("Pouce gauche");
            }
        }
    // Index
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Left, out pose))
        {
            indexObject.transform.position = pose.Position;
            if (isCollision(pose.Position))
            {
                this.setText("Index gauche");
            }
        }
    // Middle
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.MiddleTip, Handedness.Left, out pose))
        {
            middleObject.transform.position = pose.Position;
            if (isCollision(pose.Position))
            {
                this.setText("Majeur gauche");
            }
        }
    // Ring
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.RingTip, Handedness.Left, out pose))
        {
            ringObject.transform.position = pose.Position;
            if (isCollision(pose.Position))
            {
                this.setText("Annulaire gauche");
            }
        }
    // Pinky
        else if (HandJointUtils.TryGetJointPose(TrackedHandJoint.PinkyTip, Handedness.Left, out pose))
        {
            pinkyObject.transform.position = pose.Position;
            if (isCollision(pose.Position))
            {
                this.setText("Auriculaire gauche");
            }
        }

        else
        {
            this.setText("Appuyez");
        }
    }

    private void setText(string _text)
    {
        text.GetComponent<Text>().text = _text;
    }

    private Vector3 getPosition()
    {
        return buttons[0].GetComponent<RectTransform>().position;
    }

    private float getHalfWidthSize()
    {
        return buttons[0].GetComponent<RectTransform>().rect.width / 2;
    }

    private float getHalfHeightSize()
    {
        return buttons[0].GetComponent<RectTransform>().rect.height / 2;
    }

    private bool isCollision(Vector3 current)
    {
        Vector3 button = this.getPosition();
        float HWidth = getHalfWidthSize(), HHeight = getHalfHeightSize();
        return (
                (current.x >= button.x - HWidth) && (current.x <= button.x + HWidth) 
                && 
                (current.y >= button.y - HHeight) && (current.y <= button.y + HHeight)
            );
    }
}
